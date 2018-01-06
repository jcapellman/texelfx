using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;

using texelfx.library.Enums;

namespace texelfx.library.Scalers
{
    public class NearestNeighborScaler : BaseScaler
    {
        public override StatusCodes Scale(int width, int height, string inputFileName, string outputFileName)
        {
            if (!File.Exists(inputFileName))
            {
                return StatusCodes.INPUT_FILE_DOES_NOT_EXIST;
            }

            try
            {
                using (var bitmapStream = File.Open(inputFileName, FileMode.Open))
                {
                    var img = Image.FromStream(bitmapStream);

                    using (var graphics = Graphics.FromImage(img))
                    {
                        graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                        graphics.DrawImage(img, 0, 0, width, height);

                        var mBitmap = new Bitmap(width, height, graphics);
                        mBitmap.Save(GetOutputFileName(inputFileName, outputFileName), ImageFormat.Png);
                    }

                    return StatusCodes.SUCCESSFULLY_SCALED;
                }
            }
            catch (Exception)
            {
                return StatusCodes.INTERNAL_ERROR;
            }
        }
    }
}