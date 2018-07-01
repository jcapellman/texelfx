namespace texelfx.trainer
{
    class Program
    {
        static void Main(string[] args)
        {
            var trainer = new TexelTrainer();

            trainer.Train(args[0], args[1]);
        }
    }
}