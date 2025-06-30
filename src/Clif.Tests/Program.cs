using System;
using System.Text.RegularExpressions;

namespace Clif.Tests
{
    class Program
    {
        public static void Main(string[] args)
        {
            string line = "this is a `test code` text and `test 2 text` finish";
            string pattern = @"`(.*?)`";
            Regex regex = new(pattern, RegexOptions.Compiled);
            MatchCollection matches = regex.Matches(line);
            foreach (Match match in matches)
                Console.WriteLine(match.Value);

        }


    }
}