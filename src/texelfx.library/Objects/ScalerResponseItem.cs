using System;

namespace texelfx.library.Objects
{
    public class ScalerResponseItem
    {
        public byte[] ScaledBytes { get; set; }

        public (int width, int height) OriginalDimensions { get; set; }

        public (int width, int height) ScaledDimensions { get; set; }

        public Exception ExceptionCaught { get; private set; }

        public bool HasError => ExceptionCaught != null;

        public ScalerResponseItem(Exception exception)
        {
            ExceptionCaught = exception;
        }

        public ScalerResponseItem() { }
    }
}