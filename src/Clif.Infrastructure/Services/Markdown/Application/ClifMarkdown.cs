using System;
using System.Text.RegularExpressions;
using Clif.Infrastructure.Services.Markdown.Infrastructure;
using static Clif.Infrastructure.Services.Markdown.Domain.EscapeCodes;

namespace Clif.Infrastructure.Services.Markdown.Application
{
    public class ClifMarkdown : IClifMarkdown
    {
        public string Render(string line)
        {
            line = encode(line);
            line = header(line);
            line = blockquote(line);
            line = image(line);
            line = link(line);
            line = emphasis(line);
            line = highlight(line);
            line = rule(line);
            //more...
            line = decode(line);
            line = code(line);
            line = ready(false, line);
            background = foreground = null;
            return line;
        }

        private string? background;
        private string? foreground;

        private string[] codes = new string[1];

        private string encode(string line)
        {
            Regex regex = new Regex(@"`(.*?)`", RegexOptions.Compiled);
            MatchCollection matches = regex.Matches(line);
            codes = new string[matches.Count];
            int i = 0;
            foreach (Match match in matches)
            {
                codes[i] = match.Value;
                line = line.Replace(codes[i], $"!?!code{i++}?!?");
            }
            return line;
        }

        private string decode(string line)
        {
            Regex regex = new Regex(@"\!\?\!(code\d+)\?\!\?", RegexOptions.Compiled);
            MatchCollection matches = regex.Matches(line);
            for (int i = 0; i < codes.Length; i++)
                line = line.Replace(matches[i].Value, codes[i]);
            return line;
        }

        private string ready(bool mode, string line)
        {
            if (mode)
                line = line.Replace("[", "⤀").Replace("]", "⤙");
            else
                line = line.Replace("⤀", "[").Replace("⤙", "]");
            return line;
        }

        private string render(string line, string pattern, string start, string end)
        {
            return Regex.Replace(line,
                pattern,
                match => $"{start}{match.Groups[1].Value}{end}",
                RegexOptions.Compiled);
        }

        private string colors()
        {
            return string.IsNullOrEmpty(background) ?
                TextFormats.Reset :
                background + foreground;
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
                            background = Backgrounds.BrightYellow;
                            foreground = Foregrounds.Black;
                            break;
                        case 1:
                            background = Backgrounds.BrightCyan;
                            foreground = Foregrounds.Black;
                            break;
                        case 2:
                            background = Backgrounds.BrightGreen;
                            foreground = Foregrounds.Black;
                            break;
                    }
                    break;
                }
            }
            return isHeader ? render(line, pattern, $"{background}{foreground}", "") + TextFormats.Reset : line;
        }

        private string blockquote(string line)
        {
            string pattern = @"^>";
            if (Regex.IsMatch(line, pattern, RegexOptions.Compiled))
            {
                background = Backgrounds.BrightBlack;
                foreground = Foregrounds.BrightWhite;
                return render(line, pattern, $"{Backgrounds.Magenta} {background}{Foregrounds.Magenta}\"{foreground}", "")
                    + $"{Foregrounds.Magenta}\"{TextFormats.Reset}";
            }
            return line;
        }

        private string image(string line)
        {
            string pattern = @"\!\[(.*?)\]\((.*?)\)";
            Regex regex = new Regex(pattern, RegexOptions.Compiled);
            MatchCollection matches = regex.Matches(line);
            string[] images = new string[matches.Count];
            for (int i = 0; i < images.Length; i++)
            {
                string mode = matches[i].Groups[2].Value.IndexOf("http") == -1 ? "file://" : "";
                line = Regex.Replace(line, pattern, match =>
                    $"{Foregrounds.Magenta}{match.Groups[1].Value}{colors()}");
                line += $"\n{toJP2A(matches[i].Groups[2].Value)}" +
                    $"{Foregrounds.Magenta}{TextFormats.Bold}{matches[i].Groups[1].Value}⤴ " +
                    $"{TextFormats.BoldOff}[🖼 ]({mode}{matches[i].Groups[2].Value})" +
                    colors();
            }
            return line;
        }

        private string toJP2A(string image)
        {
            string jp2a = AsciiArt.ToJP2A(image).Result;
            return ready(true, jp2a);
        }

        private string link(string line)
        {
            string pattern = @"\[(.*?)\]\((.*?)\)";
            if (Regex.IsMatch(line, pattern, RegexOptions.Compiled))
                line = Regex.Replace(line, pattern, match =>
                    $"{Foregrounds.BrightBlue}{Other.Link(match.Groups[2].Value, match.Groups[1].Value)}{colors()}");
            return line;
        }

        private string emphasis(string line)
        {
            string pattern = @"\*\*\*(.*?)\*\*\*";
            if (Regex.IsMatch(line, pattern, RegexOptions.Compiled))
                line = render(line, pattern, $"{TextFormats.Bold}{TextFormats.Italic}", $"{TextFormats.ItalicOff}{TextFormats.BoldOff}");
            pattern = @"___(.*?)___";
            if (Regex.IsMatch(line, pattern, RegexOptions.Compiled))
                line = render(line, pattern, $"{TextFormats.Bold}{TextFormats.Italic}", $"{TextFormats.ItalicOff}{TextFormats.BoldOff}");

            pattern = @"\*\*(.*?)\*\*";
            if (Regex.IsMatch(line, pattern, RegexOptions.Compiled))
                line = render(line, pattern, TextFormats.Bold, TextFormats.BoldOff);
            pattern = @"__(.*?)__";
            if (Regex.IsMatch(line, pattern, RegexOptions.Compiled))
                line = render(line, pattern, TextFormats.Bold, TextFormats.BoldOff);

            pattern = @"\*(.*?)\*";
            if (Regex.IsMatch(line, pattern, RegexOptions.Compiled))
                line = render(line, pattern, TextFormats.Italic, TextFormats.ItalicOff);
            pattern = @"_(.*?)_";
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

            pattern = @"\%(.*?)\%";
            if (Regex.IsMatch(line, pattern, RegexOptions.Compiled))
                line = render(line, pattern, TextFormats.Blink, TextFormats.BlinkOff);

            return line;
        }

        private string highlight(string line)
        {
            string pattern = @"==(.*?)==";
            if (Regex.IsMatch(line, pattern, RegexOptions.Compiled))
                line = render(line, pattern, $"{Backgrounds.BrightWhite}{Foregrounds.Black}", colors());
            return line;
        }

        private string rule(string line)
        {
            string pattern = @"^---+";
            if (Regex.IsMatch(line, pattern, RegexOptions.Compiled))
            {
                string hRule = "─────────────────────────────────────────────";
                return render(line, pattern, $"{Foregrounds.BrightWhite}{hRule}{Foregrounds.Reset}", "");
            }
            return line;
        }

        private string code(string line)
        {
            string pattern = @"`(.*?)`";
            if (Regex.IsMatch(line, pattern, RegexOptions.Compiled))
                return render(line, pattern,
                    $"{Backgrounds.BrightBlack}{Foregrounds.BrightWhite}`{Foregrounds.Black}{Backgrounds.BrightRed}",
                    $"{Backgrounds.BrightBlack}{Foregrounds.BrightWhite}`{colors()}");
            return line;
        }
    }
}