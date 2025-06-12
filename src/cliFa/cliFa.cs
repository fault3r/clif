using System;
using System.Text.RegularExpressions;
using EscapeCodes;

namespace clif
{
    public class cliFa
    {
        private string[] codes = new string[1];

        private string Encode(string line)
        {
            Regex regex = new Regex(@"`(.*?)`", RegexOptions.Compiled);
            var matches = regex.Matches(line);
            codes = new string[matches.Count];
            int i = 0;
            foreach (Match match in matches)
            {
                codes[i] = match.Value;
                line = line.Replace(codes[i], $"[[[code{i++}]]]");
            }
            return line;
        }

        private string Decode(string line)
        {
            Regex regex = new Regex(@"\[\[\[code\d+\]\]\]", RegexOptions.Compiled);
            var matches = regex.Matches(line);
            for (int i = 0; i < codes.Length; i++)
                line = line.Replace(matches[i].Value, codes[i]);
            return line;
        }

        public string Render(string line)
        {
            line = Encode(line);
            line = Header(line);
            line = Styles(line);
            line = Blockquote(line);
            line = Decode(line);
            line = Code(line);
            return line;
        }

        private string Header(string line)
        {
            string pattern = string.Empty;
            pattern = @"^###";                   // #Header3
            if (Regex.IsMatch(line, pattern))
                return Regex.Replace(line, pattern, $"{Backgrounds.Red}{Foregrounds.Black}") + TextFormats.Reset;
            pattern = @"^##";                    //Header2
            if (Regex.IsMatch(line, pattern))
                return Regex.Replace(line, pattern, $"{Backgrounds.Yellow}{Foregrounds.Black}") + TextFormats.Reset;
            pattern = @"^#";                     //Header1
            if (Regex.IsMatch(line, pattern))
                return Regex.Replace(line, pattern, $"{Backgrounds.Green}{Foregrounds.Black}") + TextFormats.Reset;
            return line;
        }

        private string Styles(string line)
        {
            string pattern = string.Empty;
            pattern = @"\*\*\*(.+?)\*\*\*|___(.+?)___";      // **Bold & Italic**
            Regex regex = new Regex(pattern, RegexOptions.Compiled);
            MatchCollection matches = regex.Matches(line);
            foreach (Match match in matches)
                line = regex.Replace(line, $"{TextFormats.Bold}{TextFormats.Italic}{match.Groups[1].Value}{TextFormats.ItalicOff}{TextFormats.BoldOff}");
                
            pattern = @"\*\*(.+?)\*\*|__(.+?)__";          // **Bold**
            regex = new Regex(pattern, RegexOptions.Compiled);
            matches = regex.Matches(line);
            foreach (Match match in matches)
                line = regex.Replace(line, $"{TextFormats.Bold}{match.Groups[1].Value}{TextFormats.BoldOff}");

            pattern = @"\*(.+?)\*|_(.+?)_";          // **Italic**
            regex = new Regex(pattern, RegexOptions.Compiled);
            matches = regex.Matches(line);
            foreach (Match match in matches)
                line = regex.Replace(line, $"{TextFormats.Italic}{match.Groups[1].Value}{TextFormats.ItalicOff}");
                
            pattern = @"~~~(.*?)~~~";            // ~~Dim~~
            if (Regex.IsMatch(line, pattern))
                line = Regex.Replace(line, pattern, match =>
                    $"{TextFormats.Dim}{match.Groups[1].Value}{TextFormats.DimOff}");
            pattern = @"~~(.*?)~~";              // Strikethrough
            if (Regex.IsMatch(line, pattern))
                line = Regex.Replace(line, pattern, match =>
                    $"{TextFormats.Strike}{match.Groups[1].Value}{TextFormats.StrikeOff}");
            pattern = @"~(.*?)~";                // _Underline_
            if (Regex.IsMatch(line, pattern))
                line = Regex.Replace(line, pattern, match =>
                    $"{TextFormats.Underline}{match.Groups[1].Value}{TextFormats.UnderlineOff}");
            pattern = @"%(.*?)%";                // %Blink%
            if (Regex.IsMatch(line, pattern))
                line = Regex.Replace(line, pattern, match =>
                    $"{TextFormats.Blink}{match.Groups[1].Value}{TextFormats.BlinkOff}");
            return line;
        }

        private string Blockquote(string line)
        {
            string pattern = @"^>";              // > Blockquote
            if (Regex.IsMatch(line, pattern))
                return Regex.Replace(line, pattern,
                    $"{Backgrounds.Magenta} {Backgrounds.Reset}{Backgrounds.BrightBlack}{Foregrounds.White}\"") + $"\"{TextFormats.Reset}";
            return line;
        }

        private string Code(string line)
        {
            string pattern = @"`(.*?)`";
                line = Regex.Replace(line, pattern, match =>
                    $"{Backgrounds.BrightBlack} {Backgrounds.Reset}{Backgrounds.Cyan}"+
                        $"{Foregrounds.Black}{match.Groups[1].Value}{TextFormats.Reset}" +
                        $"{Backgrounds.BrightBlack} {Backgrounds.Reset}");
            return line;
        }
    }
}