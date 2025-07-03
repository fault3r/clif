using System;
using System.Text.RegularExpressions;

namespace Clif.Tests
{
    class Program
    {
        public static void Main(string[] args)
        {
            string line = "| some 1text1 here | some 2text here2 |some shit here too";
            string pattern = @"^\| ([^|]+) \| ([^|]+) \|";
            // line = "shot shit | --- | --- |some shit here too";
            // pattern = @"\| (-{3,}) \| (-{3,}) \|";
            Regex regex = new(pattern, RegexOptions.Compiled);
            MatchCollection matches = regex.Matches(line);
            foreach (Match match in matches)
                Console.WriteLine(match.Value);


            // first line is header
            // | --- | ---| line is a colorized line
            //others are normal
        
        }


    }
}