using System;
using System.Text.RegularExpressions;
using EscapeCodes;

namespace clif
{
    public class cliFa
    {
        public string Render(string line)
        {            
            string rendered = line;
            rendered = Header(rendered);
            rendered = Styles(rendered);
            rendered = Blockquote(rendered);
            // rendered = Code(rendered);
            return rendered;
        }

        private string Header(string line)
        {
            string pattern = string.Empty;
            pattern = @"^###";    // #Header3
            if (Regex.IsMatch(line, pattern))
                return Regex.Replace(line, pattern, $"{Backgrounds.Red}{Foregrounds.Black}") + TextFormats.Reset;
            pattern = @"^##";    //Header2
            if (Regex.IsMatch(line, pattern))
                return Regex.Replace(line, pattern, $"{Backgrounds.Yellow}{Foregrounds.Black}") + TextFormats.Reset;
            pattern = @"^#";    //Header1
            if (Regex.IsMatch(line, pattern))
                return Regex.Replace(line, pattern, $"{Backgrounds.Green}{Foregrounds.Black}") + TextFormats.Reset;
            return line;
        }

        private string Styles(string line)
        {
            string pattern = string.Empty;
             pattern =  @"\*\*\*(.+?)\*\*\*";  
            if (Regex.IsMatch(line, pattern))
                line = Regex.Replace(line, pattern, match =>
                    $"{TextFormats.Bold}{TextFormats.Italic}{match.Groups[1].Value}{TextFormats.ItalicOff}{TextFormats.BoldOff}");
            pattern = @"\*\*(.+?)\*\*";    // **Bold**
            if (Regex.IsMatch(line, pattern))
                line = Regex.Replace(line, pattern, match =>
                    $"{TextFormats.Bold}{match.Groups[1].Value}{TextFormats.BoldOff}");
            pattern = @"\*(.+?)\*";    // *Italic*
            if (Regex.IsMatch(line, pattern))
                line = Regex.Replace(line, pattern, match =>
                    $"{TextFormats.Italic}{match.Groups[1].Value}{TextFormats.ItalicOff}");
            pattern = @"___(.*?)___";    // ___Strike___
            if (Regex.IsMatch(line, pattern))
                line = Regex.Replace(line, pattern, match =>
                    $"{TextFormats.Strike}{match.Groups[1].Value}{TextFormats.StrikeOff}");
            pattern = @"__(.*?)__";    // __Underline__
            if (Regex.IsMatch(line, pattern))
                line = Regex.Replace(line, pattern, match =>
                    $"{TextFormats.Underline}{match.Groups[1].Value}{TextFormats.UnderlineOff}");
            pattern = @"_(.*?)_";    // _Dim_
            if (Regex.IsMatch(line, pattern))
                line = Regex.Replace(line, pattern, match =>
                    $"{TextFormats.Dim}{match.Groups[1].Value}{TextFormats.DimOff}");
            pattern = @"%(.*?)%";    // %Blink%
            if (Regex.IsMatch(line, pattern))
                line = Regex.Replace(line, pattern, match =>
                    $"{TextFormats.Blink}{match.Groups[1].Value}{TextFormats.BlinkOff}");
            return line;
        }

        private string Blockquote(string line)
        {
            string pattern = @"^>";
            if (Regex.IsMatch(line, pattern))
                return Regex.Replace(line, pattern,
                    $"{Backgrounds.Cyan} {Backgrounds.Reset}{Backgrounds.White}{Foregrounds.Black}\"") + $"\"{TextFormats.Reset}";
            return line;
        }

        private string Code(string line)
        {
            string pattern = @"`([^`]*)`";
            if (Regex.IsMatch(line, pattern))
                line = Regex.Replace(line, pattern, "aa");
            return line;

        }

    }
}