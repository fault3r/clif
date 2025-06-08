using clif;
using EscapeCodes;

namespace clif_cli
{
    public class Program
    {
        private static cliFa clif = new();


        public static void Main(string[] args)
        {

            string sample = "# text this is a __**bold**__ character *italic* and sample ***bold and italic*** text";
            Console.WriteLine(clif.Render(sample));

            sample = "## this is a ___strike___ character __underline__ and sample _fault3r_ text";
            Console.WriteLine(clif.Render(sample));

            sample = "### this is a **bold** character *italic* and sample of %Hamed Damavandi% text";
            Console.WriteLine(clif.Render(sample));

            Console.WriteLine(TextFormats.Dim + "Dim " + TextFormats.DimOff + "test");
            Console.WriteLine(TextFormats.Underline + "Underlined text" + TextFormats.UnderlineOff + " test");

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