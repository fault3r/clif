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
            rendered = Header3(rendered);
            rendered = Header2(rendered);
            rendered = Header1(rendered);
            rendered = Bold(rendered);
            rendered = Italic(rendered);
            return rendered;
        }


        public string Header1(string input)
        {
            string pattern = @"^#";
            return Regex.Replace(input, pattern, Backgrounds.Red) + TextFormats.Reset;
        }

        public string Header2(string input)
        {
            string pattern = @"^##";
            return Regex.Replace(input, pattern, Backgrounds.Yellow) + TextFormats.Reset;
        }

        public string Header3(string input)
        {
            string pattern = @"^###";
            return Regex.Replace(input, pattern, Backgrounds.Green) + TextFormats.Reset;
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