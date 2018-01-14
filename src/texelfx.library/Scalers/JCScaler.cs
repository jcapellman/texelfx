using texelfx.library.Common;
using texelfx.library.Objects;

using TensorFlowSharpCore;

namespace texelfx.library.Scalers
{
    public class JCScaler : BaseScaler
    {
        public override string Name => "JC Scaler";

        public override ScalerResponseItem Scale(int scaleMultiplier, byte[] originalBytes)
        {
            var graph = new TFGraph();

            // TODO Load Model

            using (var session = new TFSession(graph))
            {
                var tensor = ImageUtil.CreateTensorFromImageBytes(originalBytes);

                var runner = session.GetRunner();

                // TODO AddInput to the runner with the data from above

                var output = runner.Run();
                
                // TODO Parse output
            }

            return new ScalerResponseItem
            {
                ScaledBytes = originalBytes
            };
        }
    }
}