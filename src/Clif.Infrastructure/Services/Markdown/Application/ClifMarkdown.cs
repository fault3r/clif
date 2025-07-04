using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;
using System.Xml.Serialization;
using Clif.Infrastructure.Services.Markdown.Infrastructure;
using static Clif.Infrastructure.Services.Markdown.Domain.EscapeCodes;

namespace Clif.Infrastructure.Services.Markdown.Application
{
    public class ClifMarkdown : IClifMarkdown
    {
        public string Render(string line)
        {
            line = Table(line);
            line = _Encode(line);
            line = Header(line); 
            line = Blockquote(line); 
            line = Tasklist(line);
            line = Image(line);
            line = Link(line);
            line = Emphasis(line);
            line = Highlight(line); 
            line = Rule(line);
            line = Unordered(line);
            line = Ordered(line);
            // More... 
            line = _Decode(line);
            line = Code(line);
            line = _Ready(false, line);
            currentBackground = currentForeground = null;
            return line;
        }

        private string? currentBackground;
        private string? currentForeground;

        private string currentColors() =>
            string.IsNullOrEmpty(currentBackground) ?
            TextFormats.Reset :
            currentBackground + currentForeground;

        private string _Ready(bool mode, string line)
        {
            if (insertRow)
            {
                line += "\n";
                insertRow = false;
            }
            return mode ?
            line.Replace("[", "⤀").Replace("]", "⤛") :
            line.Replace("⤀", "[").Replace("⤛", "]");
        }

        private string[] codes = new string[1];

        string[] headers = [];
        int headersCount = 0;
        private bool inTable = false;
        private bool insertRow = false;

        private string _Encode(string line)
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

        private string _Decode(string line)
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
            string[] patterns = [@"^### ", @"^## ", @"^# "];
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
            string pattern = @"^> ";
            Regex regex = new(pattern, RegexOptions.Compiled);
            MatchCollection matches = regex.Matches(line);
            foreach (Match match in matches)
            {
                currentBackground = Backgrounds.BrightBlack;
                currentForeground = Foregrounds.BrightWhite;
                return regex.Replace(
                    line,
                    match => $"{Backgrounds.Magenta} {currentBackground}{Foregrounds.Magenta}\"{currentForeground}",
                    1) +
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
                line = regex.Replace(
                    line,
                    match => $"{Foregrounds.Magenta}{match.Groups[1].Value}{currentColors()}",
                    1);
                string mode = !match.Groups[2].Value.Contains("http") ? "file://" : "";
                line += $"\n{AsciiArt.ToJP2A(match.Groups[2].Value).Result}" +
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
                line = regex.Replace(
                    line,
                    match => $"{Foregrounds.BrightBlue}{Other.Link(match.Groups[2].Value, match.Groups[1].Value)}{currentColors()}",
                    1);
            return line;
        }

        private string Emphasis(string line)
        {
            string pattern = @"\*\*\*(.*?)\*\*\*";
            line = Regex.Replace(
                line,
                pattern,
                match => $"{TextFormats.Bold}{TextFormats.Italic}{match.Groups[1].Value}{TextFormats.ItalicOff}{TextFormats.BoldOff}",
                RegexOptions.Compiled);
            pattern = @"___(.*?)___";
            line = Regex.Replace(
                line,
                pattern,
                match => $"{TextFormats.Bold}{TextFormats.Italic}{match.Groups[1].Value}{TextFormats.ItalicOff}{TextFormats.BoldOff}",
                RegexOptions.Compiled);
            pattern = @"\*\*(.*?)\*\*";
            line = Regex.Replace(
                line,
                pattern,
                match => $"{TextFormats.Bold}{match.Groups[1].Value}{TextFormats.BoldOff}",
                RegexOptions.Compiled);
            pattern = @"__(.*?)__";
            line = Regex.Replace(
                line,
                pattern,
                match => $"{TextFormats.Bold}{match.Groups[1].Value}{TextFormats.BoldOff}",
                RegexOptions.Compiled);
            pattern = @"\*(.*?)\*";
            line = Regex.Replace(
                line,
                pattern,
                match => $"{TextFormats.Italic}{match.Groups[1].Value}{TextFormats.ItalicOff}",
                RegexOptions.Compiled);
            pattern = @"_(.*?)_";
            line = Regex.Replace(
                line,
                pattern,
                match => $"{TextFormats.Italic}{match.Groups[1].Value}{TextFormats.ItalicOff}",
                RegexOptions.Compiled);
            pattern = @"~~~(.*?)~~~";
            line = Regex.Replace(
                line,
                pattern,
                match => $"{TextFormats.Dim}{match.Groups[1].Value}{TextFormats.DimOff}",
                RegexOptions.Compiled);
            pattern = @"~~(.*?)~~";
            line = Regex.Replace(
                line,
                pattern,
                match => $"{TextFormats.Strike}{match.Groups[1].Value}{TextFormats.StrikeOff}",
                RegexOptions.Compiled);
            pattern = @"~(.*?)~";
            line = Regex.Replace(
                line,
                pattern,
                match => $"{TextFormats.Underline}{match.Groups[1].Value}{TextFormats.UnderlineOff}",
                RegexOptions.Compiled);
            pattern = @"\%(.*?)\%";
            line = Regex.Replace(
                line,
                pattern,
                match => $"{TextFormats.Blink}{match.Groups[1].Value}{TextFormats.BlinkOff}",
                RegexOptions.Compiled);
            return line;
        }

        private string Highlight(string line)
        {
            string pattern = @"==(.*?)==";
            line = Regex.Replace(
                line,
                pattern,
                match => $"{Backgrounds.BrightWhite}{Foregrounds.Black}{match.Groups[1].Value}{currentColors()}",
                RegexOptions.Compiled);
            return line;
        }

        private string Rule(string line)
        {
            string pattern = @"^---+";
            string rule = "─────────────────────────────────────────────";
            line = Regex.Replace(
                line,
                pattern,
                match => $"{Foregrounds.BrightWhite}{rule}{Foregrounds.Reset}{match.Groups[1].Value}",
                RegexOptions.Compiled);
            return line;
        }

        private string Tasklist(string line)
        {
            string pattern = @"^- \[[ x]\] ";
            line = Regex.Replace(
                line,
                pattern,
                match => $"   {Foregrounds.BrightYellow}{(match.Value == "- [ ] " ? "✗ " : "✔ ")}\t{Foregrounds.Reset}",
                RegexOptions.Compiled);
            return line;
        }

        private string Unordered(string line)
        {
            string pattern = @"^- ";
            string bullet = $"{Foregrounds.BrightGreen}❂ {Foregrounds.Reset}";
            line = Regex.Replace(
                line,
                pattern,
                match => $"   {bullet}\t{Foregrounds.Reset}{match.Groups[1].Value}",
                RegexOptions.Compiled);
            return line;
        }

        private string Ordered(string line)
        {
            string pattern = @"^\d+\. ";
            line = Regex.Replace(
                line,
                pattern,
                match => $"   {Foregrounds.BrightCyan}{match.Groups[0].Value}{Foregrounds.Reset}\t{match.Groups[1].Value}",
                RegexOptions.Compiled);
            return line;
        }

        private string Table(string line)
        {
            string pattern = @"\|([^|]+)";
            Regex regex = new(pattern, RegexOptions.Compiled);
            MatchCollection matches = regex.Matches(line);
            if (matches.Count > 0)
            {
                string headerPattern = @"^\|(\s*-{3,}\s*\|)+";
                var headerMatches = Regex.Matches(line, headerPattern);
                if(headerMatches.Count>0)
                {
                    if (inTable)
                    {
                        headersCount = headerMatches[0].Value.Count(c => c == '|') - 1;
                        line= GradientText.ToGradient("✦✧✦✧✦✧✦✧✦✧✦✧✦✧✦✧✦✧✦✧✦✧✦✧✦✧✦✧✦✧✦✧✦✧✦✧✦✧✦✧✦✧✦✧✦");
                    }
                }
                else
                {
                    if (!inTable)
                    {
                        headersCount = matches.Count;
                        headers = new string[headersCount];
                        int c = 0;
                        foreach (Match match in matches)
                            headers[c++] = match.Groups[1].Value.Trim();
                        inTable = true;
                        line = GradientText.ToGradient("………………………………………………………………………………………………………………………");  
                    }
                    else
                    {
                        string row = string.Empty;
                        int count = matches.Count > headersCount ? headersCount : matches.Count;
                        for (int i = 0; i < count; i++)
                        {
                            if (i < headers.Length)
                                row += GradientText.ToGradient(headers[i].PadLeft(20));
                            else
                                row += GradientText.ToGradient($"[Column{i + 1}]".PadLeft(20));
                            row += $" {TextFormats.Bold}𑈺{TextFormats.BoldOff} {matches[i].Groups[1].Value.Trim()}\n";
                        }
                        line = row.Substring(0, row.Length - 1);
                        insertRow = true;
                    }
                }
            }
            else
            {
                if (inTable)
                {
                    inTable = false;
                    string hRule = "─────────────────────────────────────────────";
                    line = $"{Cursor.Up}{GradientText.ToGradient(hRule)}\n{Render(line)}";
                }
            }
            return line;
        }

        private string Code(string line)
        {
            string pattern = @"`(.*?)`";
            line = Regex.Replace(
                line,
                pattern,
                match => $"{Backgrounds.BrightBlack}{Foregrounds.BrightWhite}`{Foregrounds.Black}{Backgrounds.BrightRed}" +
                    $"{match.Groups[1].Value}{Backgrounds.BrightBlack}{Foregrounds.BrightWhite}`{currentColors()}",
                RegexOptions.Compiled);
            return line;
        }
    }
}