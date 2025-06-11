using clif;
using EscapeCodes;

namespace clif_cli
{
    public class Program
    {
        private static cliFa clif = new();

        public static void Main(string[] args)
        {

            
            string output = string.Empty;
            if (args.Length == 0)
            {
                //     output = clif.Render("#clif Readme");
                //     Console.WriteLine(output);
                //     return;
                // }
                // if (args.Length == 1 && !string.IsNullOrEmpty(args[0]))
                // {
                //     string file = args[0];

                string file = @"/home/hamed-damavandi/Documents/clif/test.txt";
                if (File.Exists(file))
                {
                    string[]? lines = File.ReadAllLines(file);
                    foreach (string line in lines)
                        Console.WriteLine(clif.Render(line));
                }
                else
                    Console.WriteLine(clif.Render("###File not found!"));
            }
            else
                Console.WriteLine(clif.Render("##Invalid command!"));
        }
    }
}