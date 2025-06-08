using clif;
using EscapeCodes;

namespace clif_cli
{
    public class Program
    {
        private static cliFa clif = new();


        public static void Main(string[] args)
        {

            string sample = "# this is a **bold** character *italic* and sample text";
            Console.WriteLine(clif.Render(sample));

            sample = "## this is a **bold** character *italic* and sample text";
            Console.WriteLine(clif.Render(sample));

            sample = "### this is a **bold** character *italic* and sample text";
            Console.WriteLine(clif.Render(sample));

            if (args.Length == 0)
            {
                Console.WriteLine("clif README");
                return;
            }
            if (args.Length == 1 && !string.IsNullOrEmpty(args[0]))
            {
                string file = args[0];
                if (File.Exists(file))
                {
                    string[]? lines = File.ReadAllLines(file);
                }
                else
                    Console.WriteLine("file not found!");
                
            }
            else
                Console.WriteLine("invalid command!");

        }
    }
}