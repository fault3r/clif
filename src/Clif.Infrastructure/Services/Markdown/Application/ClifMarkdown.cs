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
            line = Encode(line);
            line = Header(line);
            line = Blockquote(line);
            line = Image(line);
            line = link(line);
            line = emphasis(line);
            line = highlight(line);
            line = rule(line);
            line = tasklist(line);
            line = unordered(line);
            line = ordered(line);
            //more... 
            line = Decode(line);
            line = code(line);
            line = getReady(false, line);
            background = foreground = null;
            return line;
        }

        private string? background;
        private string? foreground;

        private string[] codes = new string[1];

        private MatchCollection getMatches(string line, string pattern)
        {
            Regex regex = new(pattern, RegexOptions.Compiled);
            return regex.Matches(line);
        }
        private string Encode(string line)
        {
            //this is ok, dont change it!
            string pattern = @"`(.*?)`";
            Regex regex = new(pattern, RegexOptions.Compiled);
            MatchCollection matches = regex.Matches(line);
            codes = new string[matches.Count];
            int c = 0;
            foreach (Match match in matches)
            {
                codes[c] = match.Value;
                line = regex.Replace(line, match => $"!?!code{c++}?!?", 1);
            }
            return line;
        }

        private string Decode(string line)
        {
            string pattern = @"\!\?\!(code\d+)\?\!\?";
            Regex regex = new(pattern, RegexOptions.Compiled);
            MatchCollection matches = regex.Matches(line);
            for (int i = 0; i < codes.Length; i++)
                line = line.Replace(matches[i].Value, codes[i]);
            return line;
        }

        private string getReady(bool mode, string line)
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

        private string currentColors() =>
                string.IsNullOrEmpty(background) ?
                TextFormats.Reset :
                background + foreground;

        private string Header(string line)
        {
            string[] patterns = [@"^###", @"^##", @"^#"];
            for (int i = 0; i < patterns.Length; i++)
            {
                string pattern = patterns[i];
                var matches = getMatches(line, pattern);
                foreach (Match match in matches)
                {
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
                    return line.Replace(match.Value, $"{background}{foreground}") +
                        TextFormats.Reset;
                }
            }
            return line;
        }

        private string Blockquote(string line)
        {
            string pattern = @"^>";
            var matches = getMatches(line, pattern);
            foreach (Match match in matches)
            {
                background = Backgrounds.BrightBlack;
                foreground = Foregrounds.BrightWhite;
                return line.Replace(match.Value, $"{Backgrounds.Magenta} {background}{Foregrounds.Magenta}\"{foreground}") +
                    $"{Foregrounds.Magenta}\"{TextFormats.Reset}";
            }
            return line;
        }

        private string Image(string line)
        {
            string pattern = @"\!\[(.*?)\]\((.*?)\)";
            var matches = getMatches(line, pattern);
            string[] images = new string[matches.Count];
            foreach (Match match in matches)
            {
                string mode = !match.Groups[2].Value.Contains("http") ? "file://" : "";
                line = Regex.Replace(line, pattern, match =>
                    $"{Foregrounds.Magenta}{match.Groups[1].Value}{currentColors()}");
                line += $"\n{toJP2A(match.Groups[2].Value)}" +
                    $"{Foregrounds.Magenta}{TextFormats.Bold}{match.Groups[1].Value}⤴ " +
                    $"{TextFormats.BoldOff}[🖼 ]({mode}{match.Groups[2].Value})" +
                    currentColors();
            }
            return line;
        }

        private string toJP2A(string image)
        {
            string jp2a = AsciiArt.ToJP2A(image).Result;
            return getReady(true, jp2a);
        }

        private string link(string line)
        {
            string pattern = @"\[(.*?)\]\((.*?)\)";
            if (Regex.IsMatch(line, pattern, RegexOptions.Compiled))
                line = Regex.Replace(line, pattern, match =>
                    $"{Foregrounds.BrightBlue}{Other.Link(match.Groups[2].Value, match.Groups[1].Value)}{currentColors()}");
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
                line = render(line, pattern, $"{Backgrounds.BrightWhite}{Foregrounds.Black}", currentColors());
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

        private string tasklist(string line)
        {
            string pattern = @"- \[[ x]\]";
            if (Regex.IsMatch(line, pattern, RegexOptions.Compiled))
                line = Regex.Replace(line, pattern, match =>
                    $"{Foregrounds.Green}   " +
                    (match.Groups[0].Value == "- [ ]" ? "✗ " : "✔ ") +
                    $"\t{Foregrounds.Reset}");
            return line;
        }

        private string unordered(string line)
        {
            string pattern = @"^-+";
            if (Regex.IsMatch(line, pattern, RegexOptions.Compiled))
            {
                string bullet = $"   {Foregrounds.Green}❂ {Foregrounds.Reset}";
                return render(line, pattern, $"{Foregrounds.BrightWhite}{bullet}\t{Foregrounds.Reset}", "");
            }
            return line;
        }

        private string ordered(string line)
        {
            string pattern = @"^\d+\.";
            if (Regex.IsMatch(line, pattern, RegexOptions.Compiled))
                line = Regex.Replace(line, pattern, match =>
                    $"{Foregrounds.Green}   {match.Groups[0].Value}\t{Foregrounds.Reset}");
            return line;
        }

        private string code(string line)
        {
            string pattern = @"`(.*?)`";
            if (Regex.IsMatch(line, pattern, RegexOptions.Compiled))
                return render(line, pattern,
                    $"{Backgrounds.BrightBlack}{Foregrounds.BrightWhite}`{Foregrounds.Black}{Backgrounds.BrightRed}",
                    $"{Backgrounds.BrightBlack}{Foregrounds.BrightWhite}`{currentColors()}");
            return line;
        }
    }
}