using System;

namespace texelfx.library
{
    public abstract class BaseScaler
    {
        public abstract string Name { get; }

        protected static string GetOutputFileName(string inputFileName, string outputFileName) => !string.IsNullOrEmpty(outputFileName) ? outputFileName : $"{DateTime.Now.Ticks}_{inputFileName}";

        public abstract byte[] Scale(int width, int height, byte[] originalBytes);        
    }
}