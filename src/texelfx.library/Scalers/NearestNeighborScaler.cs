using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;

namespace texelfx.library.Scalers
{
    public class NearestNeighborScaler : BaseScaler
    {
        public override string Name => "Nearest Neighbor";

        public override byte[] Scale(int width, int height, byte[] originalBytes)
        {
            try
            {
                using (var bitmapStream = new MemoryStream(originalBytes))
                {
                    var img = Image.FromStream(bitmapStream);

                    using (var graphics = Graphics.FromImage(img))
                    {
                        graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                        graphics.DrawImage(img, 0, 0, width, height);

                        var mBitmap = new Bitmap(width, height, graphics);
                    }

                    return null;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}