using System;
using clifCore.Application;
using clifCore.Common;

namespace clif.CLI
{
    public class Program
    {
        private static readonly ClifCLI clif = new();

        public static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine(clif.Readme);
                return;
            }
            else
            {
                string arg = args[0];
                switch (arg)
                {
                    case "--markdown":
                    case "-m":
                        Console.WriteLine(clif.Markdown);
                        break;
                    case "--file":
                    case "-f":
                        string? file = args.Length > 1 ? args[1] : null;
                        if (!string.IsNullOrEmpty(file) && File.Exists(file))
                        {
                            Console.WriteLine("it is ready to open file.");
                        }
                        else
                            Console.WriteLine("clif: error loading file");
                        break;
                    case "--help":
                    case "-h":
                            Console.WriteLine(clif.Help);
                        break;
                    default:
                        Console.WriteLine($"clif: invalid option - '{arg}'\n" +
                            "try 'clif -h' for more information.");
                        break;
                }
            }
        }
    }
}