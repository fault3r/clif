using System;
using Clif.Infrastructure.Services.Markdown.Domain;
using static Clif.Infrastructure.Services.Markdown.Domain.EscapeCodes;

namespace Clif.Infrastructure.Services.Markdown.Infrastructure
{
    public static class GradientText
    {
        public static string ToGradient(string input)
        {
            int count = input.Length;
            int block = input.Length / 5;
            block = block == 0 ? ++block : block;
            int size = block;
            int color = 0;
            string output = EscapeCodes.Other.CColor[color];
            for (int i = 0; i < count; i++)
            {
                if (i == block)
                {
                    output += Other.CColor
                        [color < Other.CColor.Length - 1 ? ++color : color];
                    block += size;
                }
                output += input[i];
            }
            output += Foregrounds.Reset;
            return output;
        }
    }
}