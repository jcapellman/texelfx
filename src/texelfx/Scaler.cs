using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

using texelfx.Enums;

namespace texelfx
{
    public class Scaler
    {
        private string getOutputFileName(string inputFileName, string outputFileName)
        {
            if (!string.IsNullOrEmpty(outputFileName))
            {
                return outputFileName;
            }

            return $"{DateTime.Now.Ticks}_{inputFileName}";
        }

        public StatusCodes Scale(int width, int height, string inputFileName, string outputFileName)
        {
            if (!File.Exists(inputFileName))
            {
                return StatusCodes.INPUT_FILE_DOES_NOT_EXIST;
            }

            try
            {
                using (var BitmapStream = File.Open(inputFileName, FileMode.Open))
                {
                    Image img = Image.FromStream(BitmapStream);

                    var mBitmap = new Bitmap(img);

                    mBitmap.Save(getOutputFileName(inputFileName, outputFileName), ImageFormat.Bmp);

                    return StatusCodes.SUCCESSFULLY_SCALED;
                }
            } catch (Exception)
            {
                return StatusCodes.INTERNAL_ERROR;
            }
        }
    }
}