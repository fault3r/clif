using System;
using clifCore.Common;

namespace clifCore.Application
{
    public class ClifCLI : Clif
    {
        public string Readme
        {
            get
            {
                return Render($"__{GradientText.ToGradient("Clif.")}__ a terminal-base __Note Library__ in *Markdown*.\n") +
                    "usage: clif [OPTION]";
            }
        }

        public string Markdown
        {
            get
            {
                return Render($"__{GradientText.ToGradient("clif. Markdown CheatSheet")}__\n\n") +
                    Render("#Header 1") + "         #Header 1\n" +
                    Render("##Header 2") + "         ##Header 2\n" +
                    Render("###Header 3") + "         ###Header 3\n\n" +
                    Render("Normal") + "           Normal\n" +
                    Render("*Italic*") + "           *Italic* or _Italic_\n" +
                    Render("**Bold**") + "             **Bold** or __Bold__\n" +
                    Render("***Bold-Italic***") + "      ***Bold-Italic*** or ___Bold-Italic___\n" +
                    Render("~Underline~") + "        ~Underline~\n" +
                    Render("~~Strike~~") + "           ~~Strike~~\n" +
                    Render("~~~Dim~~~") + "              ~~~Dim~~~\n" +
                    Render("%Blink%") + "            %Blink%\n\n" +
                    Render("==Highlight==") + "        ==Highlight==\n\n" +
                    Render(">Blockquote") + "    >Blockquote\n\n" +
                    Render("`Code`") + "           `Code`\n\n" +
                    Render("[Link](http://url.com)") + "             [Link](http://url.com)\n\n" +
                    Render("![Image](clif.png)") + "        ![Image](clif.png)\n\n" +
                    "                 --- Rule\n" + Render("---");
            }
        }
        
        public string Help
        {
            get
            {
                return Readme + "\n\n" +
                    Render("**[OPTION]**             **[DESCRIPTION]**\n")+
                        "-f [FILENAME]        open clif file\n" + 
                        "-m                   Markdown cheat sheet";
            }
        }
    }
}