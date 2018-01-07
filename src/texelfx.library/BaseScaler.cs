using System;

using texelfx.library.Objects;

namespace texelfx.library
{
    public abstract class BaseScaler
    {
        public abstract string Name { get; }

        protected static string GetOutputFileName(string inputFileName, string outputFileName) => !string.IsNullOrEmpty(outputFileName) ? outputFileName : $"{DateTime.Now.Ticks}_{inputFileName}";

        public abstract ScalerResponseItem Scale(int scaleMultiplier, byte[] originalBytes);        
    }
}