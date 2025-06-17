using System;
using clifCore.Application;
using clifCore.Common;

namespace clif.CLI
{
    public class Program
    {
        private static readonly Clif clif = new();

        public static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine(clifCLI.Readme());
                return;
            }
                if (args.Length == 1 && !string.IsNullOrEmpty(args[0]))
                {
                // string file = @"/home/hamed-damavandi/Documents/clif/test.clf";
                string file = args[0];
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