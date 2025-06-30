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
            line = Link(line);
            line = Emphasis(line);
            line = highlight(line);
            line = rule(line);
            line = tasklist(line);
            line = unordered(line);
            line = ordered(line);
            //more... 
            line = Decode(line);
            line = code(line);
            line = getReady(false, line);
            currentBackground = currentForeground = null;
            return line;
        }

        private string? currentBackground;
        private string? currentForeground;

        private string currentColors() =>
            string.IsNullOrEmpty(currentBackground) ?
            TextFormats.Reset :
            currentBackground + currentForeground;

        private string getReady(bool mode, string line) =>
            mode ?
            line.Replace("[", "⤀").Replace("]", "⤙") :
            line.Replace("⤀", "[").Replace("⤙", "]");

        private string[] codes = new string[1];

        private string Encode(string line)
        {
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
            int c = 0;
            foreach (Match match in matches)
                line = regex.Replace(line, match => codes[c++], 1);
            return line;
        }

        private string Header(string line)
        {
            string[] patterns = [@"^###", @"^##", @"^#"];
            for (int i = 0; i < patterns.Length; i++)
            {
                string pattern = patterns[i];
                Regex regex = new(pattern, RegexOptions.Compiled);
                MatchCollection matches = regex.Matches(line);
                foreach (Match match in matches)
                {
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
                    return regex.Replace(line, match => $"{currentBackground}{currentForeground}", 1) +
                        TextFormats.Reset;
                }
            }
            return line;
        }

        private string Blockquote(string line)
        {
            string pattern = @"^>";
            Regex regex = new(pattern, RegexOptions.Compiled);
            MatchCollection matches = regex.Matches(line);
            foreach (Match match in matches)
            {
                currentBackground = Backgrounds.BrightBlack;
                currentForeground = Foregrounds.BrightWhite;
                return regex.Replace(line, match => $"{Backgrounds.Magenta} {currentBackground}{Foregrounds.Magenta}\"{currentForeground}", 1) +
                    $"{Foregrounds.Magenta}\"{TextFormats.Reset}";
            }
            return line;
        }

        private string Image(string line)
        {
            string pattern = @"\!\[(.*?)\]\((.*?)\)";
            Regex regex = new(pattern, RegexOptions.Compiled);
            MatchCollection matches = regex.Matches(line);
            foreach (Match match in matches)
            {
                line = regex.Replace(line, match => $"{Foregrounds.Magenta}{match.Groups[1].Value}{currentColors()}",1);
                string mode = !match.Groups[2].Value.Contains("http") ? "file://" : "";
                line += $"\n{toJP2A(match.Groups[2].Value)}" +
                    $"{Foregrounds.Magenta}{TextFormats.Bold}{match.Groups[1].Value}⤴ " +
                    $"{TextFormats.BoldOff}[🖼 ]({mode}{match.Groups[2].Value}){Backgrounds.Reset}{Foregrounds.Reset}";
            }
            return line;
        }

        private string Link(string line)
        {
            string pattern = @"\[(.*?)\]\((.*?)\)";
            Regex regex = new(pattern, RegexOptions.Compiled);
            MatchCollection matches = regex.Matches(line);
            foreach (Match match in matches)
                line = regex.Replace(line, match => $"{Foregrounds.BrightBlue}{Other.Link(match.Groups[2].Value, match.Groups[1].Value)}{currentColors()}",1);
            return line;
        }

        private string Emphasis(string line)
        {
            string pattern = string.Empty;

            pattern = @"\*\*\*(.*?)\*\*\*";
            line = Regex.Replace(line, pattern, match => $"{TextFormats.Bold}{TextFormats.Italic}{match.Groups[1].Value}{TextFormats.ItalicOff}{TextFormats.BoldOff}", RegexOptions.Compiled);

            pattern = @"___(.*?)___";
            line = Regex.Replace(line, pattern, match => $"{TextFormats.Bold}{TextFormats.Italic}{match.Groups[1].Value}{TextFormats.ItalicOff}{TextFormats.BoldOff}", RegexOptions.Compiled);

            pattern = @"\*\*(.*?)\*\*";
            line = Regex.Replace(line, pattern, match => $"{TextFormats.Bold}{match.Groups[1].Value}{TextFormats.BoldOff}", RegexOptions.Compiled);

            pattern = @"__(.*?)__";
            line = Regex.Replace(line, pattern, match => $"{TextFormats.Bold}{match.Groups[1].Value}{TextFormats.BoldOff}", RegexOptions.Compiled);

            pattern = @"\*(.*?)\*";
            line = Regex.Replace(line, pattern, match => $"{TextFormats.Italic}{match.Groups[1].Value}{TextFormats.ItalicOff}", RegexOptions.Compiled);

            pattern = @"_(.*?)_";
            line = Regex.Replace(line, pattern, match => $"{TextFormats.Italic}{match.Groups[1].Value}{TextFormats.ItalicOff}", RegexOptions.Compiled);

            pattern = @"~~~(.*?)~~~";
            line = Regex.Replace(line, pattern, match => $"{TextFormats.Dim}{match.Groups[1].Value}{TextFormats.DimOff}", RegexOptions.Compiled);

            pattern = @"~~(.*?)~~";
            line = Regex.Replace(line, pattern, match => $"{TextFormats.Strike}{match.Groups[1].Value}{TextFormats.StrikeOff}", RegexOptions.Compiled);

            pattern = @"~(.*?)~";
            line = Regex.Replace(line, pattern, match => $"{TextFormats.Underline}{match.Groups[1].Value}{TextFormats.UnderlineOff}", RegexOptions.Compiled);

            pattern = @"\%(.*?)\%";
            line = Regex.Replace(line, pattern, match => $"{TextFormats.Blink}{match.Groups[1].Value}{TextFormats.BlinkOff}", RegexOptions.Compiled);

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

        private string toJP2A(string image)
        {
            string jp2a = AsciiArt.ToJP2A(image).Result;
            return getReady(true, jp2a);
        }

        private string render(string line, string pattern, string start, string end)
        {
            return Regex.Replace(line,
                pattern,
                match => $"{start}{match.Groups[1].Value}{end}",
                RegexOptions.Compiled);
        }
    }
}