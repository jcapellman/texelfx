// Originally from https://github.com/migueldeicaza/TensorFlowSharp/blob/master/Examples/ExampleCommon/ImageUtil.cs
// Replaced the CreateTensorFromImageFile with CreateTensorFromBytes and replaced some of the syntax with C# 7.1 syntax

using System.Numerics;

using TensorFlowSharpCore;

namespace texelfx.library.Common
{
    public static class ImageUtil
    {
        /// <summary>
        /// Converts a PNG Bytes to a TFTensor
        /// </summary>
        /// <param name="fileBytes">PNG Bytes</param>
        /// <param name="dimension">Source dimension</param>
        /// <returns></returns>
        public static TFTensor CreateTensorFromImageBytes(byte[] fileBytes, Vector2 dimension)
        {
            var tensor = TFTensor.CreateString(fileBytes);

            ConstructGraphToNormalizeImage(out var graph, out var input, out var output, dimension);
            
            using (var session = new TFSession(graph))
            {
                var normalized = session.Run(
                         inputs: new[] { input },
                         inputValues: new[] { tensor },
                         outputs: new[] { output });

                return normalized[0];
            }
        }

        private static void ConstructGraphToNormalizeImage(out TFGraph graph, out TFOutput input, out TFOutput output, Vector2 dimension)
        {
            graph = new TFGraph();
            input = graph.Placeholder(TFDataType.String);

            output = graph.Cast(graph.Div(
                x: graph.Sub(
                    x: graph.ResizeBilinear(
                        images: graph.ExpandDims(
                            input: graph.Cast(
                                graph.DecodePng(contents: input, channels: 3), DstT: TFDataType.Float),
                            dim: graph.Const(0, "make_batch")),
                        size: graph.Const(new[] { dimension.X, dimension.Y }, "size")),
                    y: graph.Const(117, "mean")),
                y: graph.Const(1, "scale")), TFDataType.Float);
        }
    }
}