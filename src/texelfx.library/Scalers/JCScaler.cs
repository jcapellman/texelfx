using System.Numerics;

using texelfx.library.Common;
using texelfx.library.Objects;

using TensorFlowSharpCore;

namespace texelfx.library.Scalers
{
    public class JCScaler : BaseScaler
    {
        private static readonly Vector2 MODEL_IMAGE_SIZE = new Vector2(1920, 1080);

        public override string Name => "JC Scaler";

        private enum GraphInput
        {
            IMAGE_BYTES = 0
        }

        private enum GraphOutput
        {
            COMPRESSION_ARTIFACTS_DETECTED = 0
        }
        
        public override ScalerResponseItem Scale(int scaleMultiplier, byte[] originalBytes)
        {
            var graph = new TFGraph();

            // TODO Load Model

            using (var session = new TFSession(graph))
            {
                var tensor = ImageUtil.CreateTensorFromImageBytes(originalBytes, MODEL_IMAGE_SIZE);

                var runner = session.GetRunner();

                runner.AddInput(graph["input"][(int)GraphInput.IMAGE_BYTES], tensor);

                var output = runner.Run()[0];
                
                var hasCompressionArtifacts = ((bool[])output.GetValue())[(int)GraphOutput.COMPRESSION_ARTIFACTS_DETECTED];
            }

            return new ScalerResponseItem
            {
                ScaledBytes = originalBytes
            };
        }
    }
}