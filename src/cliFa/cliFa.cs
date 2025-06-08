using System;
using System.Text.RegularExpressions;
using EscapeCodes;

namespace clif
{
    public class cliFa
    {
        public string Render(string input)
        {
            string rendered = input;
            rendered = Header(rendered);
            rendered = Bold(rendered);
            rendered = Italic(rendered);
            return rendered;
        }


        public string Header(string input)
        {
            string pattern = @"^###";
            if (Regex.IsMatch(input, pattern))
                return Regex.Replace(input, pattern, Backgrounds.Red) + TextFormats.Reset;
            pattern = @"^##";
             if (Regex.IsMatch(input, pattern))
                return Regex.Replace(input, pattern, Backgrounds.Yellow) + TextFormats.Reset;
            pattern = @"^#";
            if (Regex.IsMatch(input, pattern))
                return Regex.Replace(input, pattern, Backgrounds.Green) + TextFormats.Reset;
            return input;      
        }

        public string Bold(string input)
        {
            string pattern = @"\*\*(.+?)\*\*";
            return Regex.Replace(input, pattern, match => TextFormats.Bold + match.Groups[1].Value + TextFormats.Normal);
        }

        public string Italic(string input)
        {
            string pattern = @"\*(.+?)\*";
            return Regex.Replace(input, pattern, match => TextFormats.Italic + match.Groups[1].Value + TextFormats.Normal);
        }
        
    }
}