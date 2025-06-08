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
            rendered = Styles(rendered);
            return rendered;
        }

        private string Header(string input)
        {
            string pattern = string.Empty;
            pattern = @"^###"; //Header3
            if (Regex.IsMatch(input, pattern))
                return Regex.Replace(input, pattern, $"{Backgrounds.Red}{Foregrounds.Black}") + $"{Foregrounds.Reset}{Backgrounds.Reset}";
            pattern = @"^##"; //Header2
             if (Regex.IsMatch(input, pattern))
                return Regex.Replace(input, pattern, $"{Backgrounds.Yellow}{Foregrounds.Black}") + $"{Foregrounds.Reset}{Backgrounds.Reset}";
            pattern = @"^#"; //Header1
            if (Regex.IsMatch(input, pattern))
                return Regex.Replace(input, pattern, $"{Backgrounds.Green}{Foregrounds.Black}") + $"{Foregrounds.Reset}{Backgrounds.Reset}";
            return input;      
        }

        private string Styles(string input)
        {
            string pattern = string.Empty;
            pattern = @"\*\*\*(.+?)\*\*\*"; //Bold & Italic
            if (Regex.IsMatch(input, pattern))
                input = Regex.Replace(input, pattern, match =>
                    $"{TextFormats.Bold}{TextFormats.Italic}{match.Groups[1].Value}{TextFormats.ItalicOff}{TextFormats.BoldOff}");
            pattern = @"\*\*(.+?)\*\*"; //Bold
            if (Regex.IsMatch(input, pattern))
                input = Regex.Replace(input, pattern, match =>
                    $"{TextFormats.Bold}{match.Groups[1].Value}{TextFormats.BoldOff}");
            pattern = @"\*(.+?)\*"; //Italic
            if (Regex.IsMatch(input, pattern))
                input = Regex.Replace(input, pattern, match =>
                    $"{TextFormats.Italic}{match.Groups[1].Value}{TextFormats.ItalicOff}");
            pattern = @"___(.*?)___"; //Strike
            if (Regex.IsMatch(input, pattern))
                input = Regex.Replace(input, pattern, match =>
                    $"{TextFormats.Strike}{match.Groups[1].Value}{TextFormats.StrikeOff}");
            pattern = @"__(.*?)__"; //Underline
            if (Regex.IsMatch(input, pattern))
                input = Regex.Replace(input, pattern, match =>
                    $"{TextFormats.Underline}{match.Groups[1].Value}{TextFormats.UnderlineOff}");
            pattern = @"_(.*?)_"; //Dim
            if (Regex.IsMatch(input, pattern))
                input = Regex.Replace(input, pattern, match =>
                    $"{TextFormats.Dim}{match.Groups[1].Value}{TextFormats.DimOff}");
            pattern = @"%(.*?)%"; //Blink
            if (Regex.IsMatch(input, pattern))
                input = Regex.Replace(input, pattern, match =>
                    $"{TextFormats.Blink}{match.Groups[1].Value}{TextFormats.BlinkOff}");
            return input;
        }
        
    }
}