using clif;
using EscapeCodes;

namespace clif_cli
{
    public class Program
    {
        private static cliFa clif = new();


        public static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("clif README");
                Console.ResetColor();
                return;
            }
            if (args.Length == 1 && !string.IsNullOrEmpty(args[0]))
            {

                //arg must be a file
                string arg = args[0];
                if (File.Exists(arg))
                {
                    string[]? lines = File.ReadAllLines(arg);
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("file received.");
                    Console.ResetColor();
                    foreach (string line in lines)
                        Console.WriteLine("\n[B]" + line + "[E]");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("\u001b[1mfile not found!");
                    Console.ResetColor();
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("invalid command!");
                Console.ResetColor();
            }

        }
    }
}