using System;

using texelfx.library.Enums;

namespace texelfx.library
{
    public abstract class BaseScaler
    {
        protected static string GetOutputFileName(string inputFileName, string outputFileName) => !string.IsNullOrEmpty(outputFileName) ? outputFileName : $"{DateTime.Now.Ticks}_{inputFileName}";

        public abstract StatusCodes Scale(int width, int height, string inputFileName, string outputFileName);        
    }
}