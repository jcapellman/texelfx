using System;

using texelfx.Common;
using texelfx.Enums;

namespace texelfx
{
    class Program
    {
        static void WriteHelp()
        {
            Console.WriteLine("Usage: dotnet texelfx.dll <name of file to scale>");
            Console.WriteLine("Optional arguments:");
            Console.WriteLine("-width <desired width>");
            Console.WriteLine("-height <desired height>");
            Console.WriteLine("-output <name of output file>");
        }

        private static (int width, int height, string inputFileName, string outputFilename) parseArguments(string[] args)
        {
            var width = Constants.DEFAULT_SCALE_WIDTH;
            var height = Constants.DEFAULT_SCALE_HEIGHT;
            var outputFileName = string.Empty;
            var inputFileName = args[0];

            for (var x = 1; x < args.Length; x++)
            {
                if (args[x] == "-width")
                {
                    width = Convert.ToInt32(args[x + 1]);
                    continue;
                }

                if (args[x] == "-height")
                {
                    height = Convert.ToInt32(args[x + 1]);
                    continue;
                }

                if (args[x] == "-output")
                {
                    outputFileName = args[x + 1];
                    continue;
                }
            }

            return (width, height, inputFileName, outputFileName);
        }

        static void Main(string[] args)
        {
            Scaler scaler = new Scaler();

            if (args.Length == 0)
            {
                WriteHelp();

                return;
            }

            var arguments = parseArguments(args);

            var status = scaler.Scale(arguments.width, arguments.height, arguments.inputFileName, arguments.outputFilename);

            switch (status)
            {
                case StatusCodes.INPUT_FILE_DOES_NOT_EXIST:
                    Console.WriteLine($"{args[0]} does not exist");
                    break;
                case StatusCodes.SUCCESSFULLY_SCALED:
                    Console.WriteLine("Successfully scaled");
                    break;
                case StatusCodes.INTERNAL_ERROR:
                    Console.WriteLine("Internal Error Occurred");
                    break;
            }
        }
    }
}