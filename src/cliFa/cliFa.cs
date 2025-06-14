using System;
using System.Diagnostics;
using System.Runtime;
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
            line = blockquote(line);
            line = image(line);
            line = link(line);
            line = emphasis(line);
            line = highlight(line);
            line = rule(line);
            //more...
            line = decode(line);
            line = code(line);
            currentBackground = currentForeground = null;
            return line.Replace("🫱🏻", "[").Replace("🫷🏻", "]"); //;0
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

        private string image(string line)
        {
            string pattern = @"\!\[(.*?)\]\((.*?)\)";
            Regex regex = new Regex(pattern, RegexOptions.Compiled);
            MatchCollection matches = regex.Matches(line);
            string[] images = new string[matches.Count];
            for (int i = 0; i < images.Length; i++)
            {
                line = Regex.Replace(line, pattern, match =>
                    $"{Foregrounds.Magenta}{match.Groups[1].Value}{currentColors()}");
                line += $"\n{getImage(matches[i].Groups[2].Value).Result}" +
                    $"{Foregrounds.Magenta}{TextFormats.Bold}-{matches[i].Groups[1].Value}-{TextFormats.BoldOff}{currentColors()}";
            }
            return line;
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

        private string code(string line)
        {
            string pattern = @"`(.*?)`";
            if (Regex.IsMatch(line, pattern, RegexOptions.Compiled))
                return render(line, pattern,
                    $"{Backgrounds.BrightBlack}{Foregrounds.BrightWhite}`{Foregrounds.Black}{Backgrounds.BrightRed}",
                    $"{Backgrounds.BrightBlack}{Foregrounds.BrightWhite}`{currentColors()}");
            return line;
        }

        static async Task<string> getImage(string path)
        {
            string command = $"jp2a {path} --color -b --width=35";
            var psi = new ProcessStartInfo
            {
                FileName = "/bin/bash",
                Arguments = $"-c \"{command}\"",
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
            };
            Process ps = new()
            {
                StartInfo = psi,
            };
            ps.Start();
            await ps.WaitForExitAsync();
            string res = string.Empty;
            string rout = ps.StandardOutput.ReadToEnd();
            string rerror = ps.StandardError.ReadToEnd();
            if (!string.IsNullOrEmpty(rout))
                res += rout;
            else if (!string.IsNullOrEmpty(rerror))
                res += "-Error Loading Image-\n";
            return res.Replace("[", "🫱🏻").Replace("]", "🫷🏻");
        }
    }
}