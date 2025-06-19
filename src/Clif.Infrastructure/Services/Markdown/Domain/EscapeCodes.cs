using System;

namespace Clif.Infrastructure.Services.Markdown.Domain
{
    public static class EscapeCodes
    {
        public static class TextFormats
        {
            public const string Reset = "\x1B⤀0m";
            public const string Bold = "\x1B⤀1m";
            public const string BoldOff = "\x1B⤀22m";
            public const string Dim = "\x1B⤀2m";
            public const string DimOff = "\x1B⤀22m";
            public const string Italic = "\x1B⤀3m";
            public const string ItalicOff = "\x1B⤀23m";
            public const string Underline = "\x1B⤀4m";
            public const string UnderlineOff = "\x1B⤀24m";
            public const string Blink = "\x1B⤀5m";
            public const string BlinkOff = "\x1B⤀25m";
            public const string Reverse = "\x1B⤀7m";
            public const string ReverseOff = "\x1B⤀27m";
            public const string Hidden = "\x1B⤀8m";
            public const string HiddenOff = "\x1B⤀28m";
            public const string Strike = "\x1B⤀9m";
            public const string StrikeOff = "\x1B⤀29m";
        }

        public static class Foregrounds
        {
            public const string Reset = "\x1B⤀39m";
            public const string Black = "\x1B⤀30m";
            public const string Red = "\x1B⤀31m";
            public const string Green = "\x1B⤀32m";
            public const string Yellow = "\x1B⤀33m";
            public const string Blue = "\x1B⤀34m";
            public const string Magenta = "\x1B⤀35m";
            public const string Cyan = "\x1B⤀36m";
            public const string White = "\x1B⤀37m";
            public const string BrightBlack = "\x1B⤀90m";
            public const string BrightRed = "\x1B⤀91m";
            public const string BrightGreen = "\x1B⤀92m";
            public const string BrightYellow = "\x1B⤀93m";
            public const string BrightBlue = "\x1B⤀94m";
            public const string BrightMagenta = "\x1B⤀95m";
            public const string BrightCyan = "\x1B⤀96m";
            public const string BrightWhite = "\x1B⤀97m";
        }

        public static class Backgrounds
        {
            public const string Reset = "\x1B⤀49m";
            public const string Black = "\x1B⤀40m";
            public const string Red = "\x1B⤀41m";
            public const string Green = "\x1B⤀42m";
            public const string Yellow = "\x1B⤀43m";
            public const string Blue = "\x1B⤀44m";
            public const string Magenta = "\x1B⤀45m";
            public const string Cyan = "\x1B⤀46m";
            public const string White = "\x1B⤀47m";
            public const string BrightBlack = "\x1B⤀100m";
            public const string BrightRed = "\x1B⤀101m";
            public const string BrightGreen = "\x1B⤀102m";
            public const string BrightYellow = "\x1B⤀103m";
            public const string BrightBlue = "\x1B⤀104m";
            public const string BrightMagenta = "\x1B⤀105m";
            public const string BrightCyan = "\x1B⤀106m";
            public const string BrightWhite = "\x1B⤀107m";
        }

        public static class Clear
        {
            public const string EndScreen = "\x1B⤀J";
            public const string BeginScreen = "\x1B⤀1J";
            public const string Screen = "\u001b⤀2J";
            public const string EndOLine = "\x1B⤀K";
            public const string BeginLine = "\x1B⤀1K";
        }

        public static class Cursor
        {
            public const string Up = "\x1B⤀A";
            public const string Right = "\x1B⤀B";
            public const string Down = "\x1B⤀C";
            public const string Left = "\x1B⤀D";
            public static string Position(int row, int column) => $"\x1B⤀{row};{column}H";
            public const string Save = "\x1B⤀s";
            public const string Restore = "\x1B⤀u";
            public const string Hide = "\x1B⤀?25l";
            public const string Show = "\x1B⤀?25h";
        }

        public static class Other
        {
            public static string Link(string url, string alt) => $"\x1B⤙8;;{url}\x1B\\{alt}\x1B⤙8;;\x1B\\";

            public static string[] CColor => ["\x1B⤀38;5;48m", "\x1B⤀38;5;49m", "\x1B⤀38;5;51m", "\x1B⤀38;5;39m", "\x1B⤀38;5;27m"];
        }
    }
}