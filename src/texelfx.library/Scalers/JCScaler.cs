using texelfx.library.Objects;

namespace texelfx.library.Scalers
{
    public class JCScaler : BaseScaler
    {
        public override string Name => "JC Scaler";

        public override ScalerResponseItem Scale(int scaleMultiplier, byte[] originalBytes)
        {
            return new ScalerResponseItem
            {
                ScaledBytes = originalBytes
            };
        }
    }
}