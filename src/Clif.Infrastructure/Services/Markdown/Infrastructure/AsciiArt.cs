using System;
using System.Diagnostics;

namespace Clif.Infrastructure.Services.Markdown.Infrastructure
{
    public class AsciiArt
    {
         public static async Task<string> ToJP2A(string image)
        {
            string command = $"jp2a {image} --color --border --width=40 --fill";
            var ps = new Process()
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "/bin/bash",
                    Arguments = $"-c \"{command}\"",
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                },
            };            
            ps.Start();
            await ps.WaitForExitAsync();
            string result = string.Empty;
            string rout = ps.StandardOutput.ReadToEnd();
            string rerror = ps.StandardError.ReadToEnd();
            if (!string.IsNullOrEmpty(rout))
                result += rout;
            else if (!string.IsNullOrEmpty(rerror))
                result += "(Error Loading Image)\n";
            return result;
        }
    }
}