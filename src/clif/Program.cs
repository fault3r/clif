using clif;

namespace clif_cli
{
    public class Program
    {
        private static cliFa clif = new();


        public static void Main(string[] args)
        {
            Console.BackgroundColor = ConsoleColor.Cyan;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("app starts...\n");
            Console.ResetColor();
         
                string str = string.Empty;
                string? line;
                while (!string.IsNullOrEmpty(line = Console.ReadLine()))
                {
                    str += "[B]" + line +"[E]\n";
                }
                Console.WriteLine(str);
                
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("\napp stops...");
            Console.ResetColor();
        }
    }
}