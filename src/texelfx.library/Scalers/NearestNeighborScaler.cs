using System;
using System.Drawing;
using System.Drawing.Drawing2D;
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

                    using (var graphics = Graphics.FromImage(img))
                    {
                        var responseItem = new ScalerResponseItem
                        {
                            OriginalDimensions = (img.Width, img.Height),
                            ScalledDimensions = (img.Width * 2, img.Height * 2)
                        };

                        graphics.InterpolationMode = InterpolationMode.NearestNeighbor;
                        graphics.DrawImage(img, 0, 0, img.Width * scaleMultiplier, img.Height * scaleMultiplier);

                        graphics.Save();

                        var mBitmap = new Bitmap(img.Width * scaleMultiplier, img.Height * scaleMultiplier, graphics);

                        using (var ms = new MemoryStream())
                        {
                            mBitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);

                            responseItem.ScaledBytes = ms.ToArray();

                            return responseItem;
                        }
                    }
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}