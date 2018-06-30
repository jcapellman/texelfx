using System;
using System.Threading.Tasks;

using texelfx.library.Objects;

namespace texelfx.library
{
    public abstract class BaseScaler
    {
        public abstract string Name { get; }

        protected static string GetOutputFileName(string inputFileName, string outputFileName) => !string.IsNullOrEmpty(outputFileName) ? outputFileName : $"{DateTime.Now.Ticks}_{inputFileName}";

        public abstract Task<ScalerResponseItem> ScaleAsync(int scaleMultiplier, byte[] originalBytes);        
    }
}