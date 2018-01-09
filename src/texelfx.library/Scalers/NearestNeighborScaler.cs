using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;

using texelfx.library.Objects;

namespace texelfx.library.Scalers
{
    public class NearestNeighborScaler : BaseScaler
    {
        public override string Name => "Nearest Neighbor";

        public override ScalerResponseItem Scale(int scaleMultiplier, byte[] originalBytes)
        {
            try
            {
                using (var bitmapStream = new MemoryStream(originalBytes))
                {
                    var img = Image.FromStream(bitmapStream);

                    var responseItem = new ScalerResponseItem
                    {
                        OriginalDimensions = (img.Width, img.Height),
                        ScaledDimensions = (img.Width * scaleMultiplier, img.Height * scaleMultiplier)
                    };

                    var scaledResult = new Bitmap(responseItem.ScaledDimensions.width, responseItem.ScaledDimensions.height);
                    scaledResult.SetResolution(img.HorizontalResolution, img.VerticalResolution);
                    
                    using (var graphics = Graphics.FromImage(scaledResult))
                    {
                        graphics.CompositingQuality = CompositingQuality.HighQuality;
                        graphics.InterpolationMode = InterpolationMode.NearestNeighbor;
                        graphics.SmoothingMode = SmoothingMode.HighQuality;
                        graphics.DrawImage(img, 0, 0, responseItem.ScaledDimensions.width, responseItem.ScaledDimensions.height);
                        
                        using (var ms = new MemoryStream())
                        {
                            scaledResult.Save(ms, ImageFormat.Png);

                            responseItem.ScaledBytes = ms.ToArray();

                            return responseItem;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return new ScalerResponseItem(ex);
            }
        }
    }
}