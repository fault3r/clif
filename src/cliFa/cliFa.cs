using System;
using System.Text.RegularExpressions;
using EscapeCodes;

namespace clif
{
    public class cliFa
    {
        private string[] codes = new string[1];

        private string? currentBackground;
        private string? currentForeground;

        private string encode(string line)
        {
            Regex regex = new Regex(@"`(.*?)`", RegexOptions.Compiled);
            var matches = regex.Matches(line);
            codes = new string[matches.Count];
            int i = 0;
            foreach (Match match in matches)
            {
                codes[i] = match.Value;
                line = line.Replace(codes[i], $"[][code{i++}][]");
            }
            return line;
        }

        private string decode(string line)
        {
            Regex regex = new Regex(@"\[\]\[code\d+\]\[\]", RegexOptions.Compiled);
            var matches = regex.Matches(line);
            for (int i = 0; i < codes.Length; i++)
                line = line.Replace(matches[i].Value, codes[i]);
            return line;
        }

        private string render(string line, string pattern, string start, string end)
        {
            return Regex.Replace(line,
                    pattern,
                    match => $"{start}{match.Groups[1].Value}{end}",
                    RegexOptions.Compiled);
        }

        private string currentColors()
        {
            return string.IsNullOrEmpty(currentBackground) ? TextFormats.Reset : currentBackground + currentForeground;
        }

        public string Render(string line)
        {
            line = encode(line);
            line = header(line);
            line = emphasis(line);
            line = blockquote(line);
            line = rule(line);
            line = decode(line);
            line = code(line);
            currentBackground = currentForeground = null;
            return line;
        }

        private string header(string line)
        {
            bool isHeader = false;
            string pattern = string.Empty;
            string[] patterns = [@"^###", @"^##", @"^#"];
            for (int i = 0; i < patterns.Length; i++)
            {
                pattern = patterns[i];
                if (Regex.IsMatch(line, pattern, RegexOptions.Compiled))
                {
                    isHeader = true;
                    switch (i)
                    {
                        case 0:
                            currentBackground = Backgrounds.BrightYellow;
                            currentForeground = Foregrounds.Black;
                            break;
                        case 1:
                            currentBackground = Backgrounds.BrightCyan;
                            currentForeground = Foregrounds.Black;
                            break;
                        case 2:
                            currentBackground = Backgrounds.BrightGreen;
                            currentForeground = Foregrounds.Black;
                            break;
                    }
                    break;
                }
            }
            return isHeader ? render(line, pattern, $"{currentBackground}{currentForeground}", "") + TextFormats.Reset : line;
        }

        private string emphasis(string line)
        {
            string pattern = @"\*\*\*(.+?)\*\*\*";
            if (Regex.IsMatch(line, pattern, RegexOptions.Compiled))
                line = render(line, pattern, $"{TextFormats.Bold}{TextFormats.Italic}", $"{TextFormats.ItalicOff}{TextFormats.BoldOff}");
            pattern = @"___(.+?)___";
            if (Regex.IsMatch(line, pattern, RegexOptions.Compiled))
                line = render(line, pattern, $"{TextFormats.Bold}{TextFormats.Italic}", $"{TextFormats.ItalicOff}{TextFormats.BoldOff}");

            pattern = @"\*\*(.+?)\*\*";
            if (Regex.IsMatch(line, pattern, RegexOptions.Compiled))
                line = render(line, pattern, TextFormats.Bold, TextFormats.BoldOff);
            pattern = @"__(.+?)__";
            if (Regex.IsMatch(line, pattern, RegexOptions.Compiled))
                line = render(line, pattern, TextFormats.Bold, TextFormats.BoldOff);

            pattern = @"\*(.+?)\*";
            if (Regex.IsMatch(line, pattern, RegexOptions.Compiled))
                line = render(line, pattern, TextFormats.Italic, TextFormats.ItalicOff);
            pattern = @"_(.+?)_";
            if (Regex.IsMatch(line, pattern, RegexOptions.Compiled))
                line = render(line, pattern, TextFormats.Italic, TextFormats.ItalicOff);

            pattern = @"~~~(.*?)~~~";
            if (Regex.IsMatch(line, pattern, RegexOptions.Compiled))
                line = render(line, pattern, TextFormats.Dim, TextFormats.DimOff);

            pattern = @"~~(.*?)~~";
            if (Regex.IsMatch(line, pattern, RegexOptions.Compiled))
                line = render(line, pattern, TextFormats.Strike, TextFormats.StrikeOff);

            pattern = @"~(.*?)~";
            if (Regex.IsMatch(line, pattern, RegexOptions.Compiled))
                line = render(line, pattern, TextFormats.Underline, TextFormats.UnderlineOff);

            pattern = @"%(.*?)%";
            if (Regex.IsMatch(line, pattern, RegexOptions.Compiled))
                line = render(line, pattern, TextFormats.Blink, TextFormats.BlinkOff);

            return line;
        }

        private string blockquote(string line)
        {
            string pattern = @"^>";
            if (Regex.IsMatch(line, pattern, RegexOptions.Compiled))
            {
                currentBackground = Backgrounds.BrightBlack;
                currentForeground = Foregrounds.BrightWhite;
                return render(line, pattern, $"{Backgrounds.Magenta} {currentBackground}{currentForeground}\"", "")
                    + $"\"{TextFormats.Reset}";
            }
            return line;
        }

        private string code(string line)
        {
            string pattern = @"`(.*?)`";
            if (Regex.IsMatch(line, pattern, RegexOptions.Compiled))
                return render(line, pattern,
                    $"{Backgrounds.BrightBlack}{Foregrounds.BrightWhite}`{Backgrounds.BrightRed}",
                    $"{Backgrounds.BrightBlack}{Foregrounds.BrightWhite}`{currentColors()}");
            return line;
        }

        private string rule(string line)
        {
            string pattern = @"^---+";
            if (Regex.IsMatch(line, pattern, RegexOptions.Compiled))
            {
                string hRule = "────────────────────────────────────────────────────";
                return render(line, pattern, $"{Foregrounds.BrightWhite}{hRule}{Foregrounds.Reset}", "");
            }
            return line;
        }
    }
}