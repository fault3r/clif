using System;
using clifCore.Domain.EscapeCodes;

namespace clifCore.Common
{
    public static class GradientText
    {
        public static string ToGradient(string input)
        {
            int count = input.Length;
            int block = input.Length / 5;
            int size = block;
            int color = 0;
            string output = Foregrounds.Clif[color];
            for (int i = 0; i < count; i++)
            {
                if (i == block)
                {
                    output += Foregrounds.Clif[color < Foregrounds.Clif.Length - 1 ? ++color : color];
                    block += size;
                }
                output += input[i];
            }
            output += Foregrounds.Reset;
            output = output.Replace("🫱🏻", "[").Replace("🫷🏻", "]");
            return output;
        }        
    }
}