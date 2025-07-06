using MediaBrowser.Common.Configuration; // For IApplicationPaths
using MediaBrowser.Controller.Drawing;    // For IImageEnhancer
using MediaBrowser.Controller.Entities;
using MediaBrowser.Controller.Plugins;
using MediaBrowser.Model.Drawing;         // For ImageFormat
using MediaBrowser.Model.Entities;        // For ImageType
using MediaBrowser.Model.Logging;
using System;
using System.Drawing; // System.Drawing.Common - WARNING: Cross-platform issues
using System.Drawing.Drawing2D;
using System.Drawing.Imaging; // For ImageFormat from System.Drawing.Common
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace FlagMyCountry
{
    public class ServerEntryPoint : IServerEntryPoint, IImageEnhancer
    {
        private readonly ILogger _logger;
        private readonly FlagOverlayManager _flagOverlayManager;
        private readonly PluginConfiguration _configuration;
        private readonly IApplicationPaths _appPaths;

        public ServerEntryPoint(ILogManager logManager, FlagOverlayManager flagOverlayManager,
                                PluginConfiguration configuration, IApplicationPaths appPaths)
        {
            _logger = logManager.GetLogger(Plugin.PluginGuid.ToString());
            _flagOverlayManager = flagOverlayManager ?? throw new ArgumentNullException(nameof(flagOverlayManager));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _appPaths = appPaths ?? throw new ArgumentNullException(nameof(appPaths));

            _logger.Info("FlagMyCountry ServerEntryPoint initialized with IImageEnhancer.");
        }

        public void Run()
        {
            _logger.Info("FlagMyCountry Plugin Run method executed.");
            // Potential: Clean up old temp flag images from previous sessions on startup
            // CleanupOldTempFiles();
        }

        public void Dispose()
        {
            _logger.Info("FlagMyCountry Plugin Disposed.");
            // Potential: Ensure any outstanding operations are cancelled if possible.
        }

        // --- IImageEnhancer Implementation ---
        public string Name => "FlagMyCountry Overlay Enhancer";
        public int Order => 0; // Default order, runs among the first.

        public bool Supports(BaseItem item, ImageType imageType)
        {
            // Apply to Primary images. Could extend to Thumb, Backdrop, etc.
            // Also, only for relevant media types.
            if (imageType == ImageType.Primary) // Restrict to primary poster for now to limit scope
            {
                bool supportedType = item is Movie || item is Series || item is Episode || item is Video;
                if (supportedType)
                {
                    _logger.Debug($"Supports() called for item: {item.Name}, type: {imageType}. Will attempt enhancement.");
                }
                return supportedType;
            }
            return false;
        }

        public async Task<string> EnhanceImageAsync(BaseItem item, string imagePath, ImageType imageType, CancellationToken cancellationToken)
        {
            _logger.Debug($"EnhanceImageAsync for item: {item.Name}, ImageType: {imageType}, Path: {imagePath}");

            string flagResourceName = _flagOverlayManager.GetFlagResourceNameForMediaItem(item);
            if (string.IsNullOrEmpty(flagResourceName))
            {
                return imagePath; // No flag applicable
            }

            try
            {
                // Define path for temporary images
                string pluginTempDir = Path.Combine(_appPaths.CachePath, "plugins", Plugin.PluginGuid.ToString(), "tempflags");
                Directory.CreateDirectory(pluginTempDir);

                // Create a unique filename based on item ID, original path hash, and flag details to aid caching
                // and prevent reprocessing if not needed.
                string originalFileName = Path.GetFileNameWithoutExtension(imagePath);
                string cacheKey = $"{item.Id}_{originalFileName}_{flagResourceName}_{_configuration.Position}_{_configuration.FlagSize}".GetHashCode().ToString("x");
                string newImageFileName = $"{cacheKey}.png"; // Always save as PNG for consistency
                string newImagePath = Path.Combine(pluginTempDir, newImageFileName);

                // Check if already processed and exists (simple caching)
                if (File.Exists(newImagePath))
                {
                    // TODO: Add logic to check if original image or config has changed since cache.
                    // For now, if it exists, assume it's current.
                    _logger.Debug($"Returning cached enhanced image for {item.Name}: {newImagePath}");
                    return newImagePath;
                }

                using (Stream flagStream = _flagOverlayManager.GetFlagImageStream(flagResourceName))
                {
                    if (flagStream == null)
                    {
                        _logger.Warn($"Flag stream for resource '{flagResourceName}' is null. Cannot overlay.");
                        return imagePath;
                    }

                    // WARNING: System.Drawing.Common has severe limitations on non-Windows platforms.
                    // Production plugins should use SkiaSharp or ImageSharp for cross-platform compatibility.
                    using (Image originalImage = Image.FromFile(imagePath)) // Handles disposal
                    using (Image flagImageToDraw = Image.FromStream(flagStream)) // Handles disposal
                    {
                        cancellationToken.ThrowIfCancellationRequested();

                        int flagSize = _configuration.FlagSize;
                        // Ensure PixelFormat supports transparency if original does, or use a common one like ARGB.
                        PixelFormat pixelFormat = originalImage.PixelFormat;
                        if(pixelFormat == PixelFormat.DontCare || pixelFormat == PixelFormat.Undefined)
                            pixelFormat = PixelFormat.Format32bppArgb; // Default to ARGB for broad compatibility


                        using (Bitmap compositedImage = new Bitmap(originalImage.Width, originalImage.Height, pixelFormat))
                        using (Graphics graphics = Graphics.FromImage(compositedImage))
                        {
                            graphics.CompositingQuality = CompositingQuality.HighQuality;
                            graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                            graphics.SmoothingMode = SmoothingMode.AntiAlias; // Smoother edges
                            graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                            // Draw the original image first
                            graphics.DrawImage(originalImage, 0, 0, originalImage.Width, originalImage.Height);

                            // Calculate flag position with margin
                            int margin = Math.Max(2, (int)(flagSize * 0.05)); // 5% margin, at least 2px
                            int x = 0, y = 0;

                            switch (_configuration.Position)
                            {
                                case FlagPosition.TopLeft:      x = margin; y = margin; break;
                                case FlagPosition.TopRight:     x = originalImage.Width - flagSize - margin; y = margin; break;
                                case FlagPosition.BottomLeft:   x = margin; y = originalImage.Height - flagSize - margin; break;
                                case FlagPosition.BottomRight:  x = originalImage.Width - flagSize - margin; y = originalImage.Height - flagSize - margin; break;
                            }
                            // Ensure flag stays within bounds if image is very small
                            x = Math.Max(0, Math.Min(x, originalImage.Width - flagSize));
                            y = Math.Max(0, Math.Min(y, originalImage.Height - flagSize));

                            graphics.DrawImage(flagImageToDraw, new Rectangle(x, y, flagSize, flagSize));
                        }

                        cancellationToken.ThrowIfCancellationRequested();

                        compositedImage.Save(newImagePath, System.Drawing.Imaging.ImageFormat.Png);
                        _logger.Info($"Applied flag to '{item.Name}'. Original: '{imagePath}', New: '{newImagePath}'");
                        return newImagePath; // Return path to the newly created image
                    }
                }
            }
            catch (OperationCanceledException)
            {
                _logger.Info($"Image enhancement cancelled for {item.Name}.");
                throw; // Re-throw cancellation
            }
            catch (Exception ex)
            {
                _logger.Error($"Error enhancing image for {item.Name} (Path: {imagePath}): {ex.ToString()}");
                return imagePath; // Fallback to original image on error
            }
        }

        // private void CleanupOldTempFiles() { ... } // Optional: Implement cleanup logic
    }
}
