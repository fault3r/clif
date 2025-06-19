using Clif.Application.Interfaces;
using Clif.Application.Services;
using Clif.Domain.Interfaces;
using Clif.Infrastructure.Data.Contexts;
using Clif.Infrastructure.Repositories;
using Clif.Infrastructure.Services.Markdown.Application;
using Microsoft.Extensions.DependencyInjection;

namespace Clif.Application
{
    public class ClifCli
    {
        private readonly IServiceProvider _serviceProvider;

        public ClifCli()
        {
            _serviceProvider = new ServiceCollection()
                .AddSingleton<LiteDbContext>(p => new LiteDbContext("ClifDb.db", "Documents"))
                .AddSingleton<IDocumentRepository, DocumentRepository>()
                .AddSingleton<IDocumentService, DocumentService>()
                .AddSingleton<IClifMarkdown, ClifMarkdown>()
                .AddSingleton<IMarkdownService, MarkdownService>()
                .BuildServiceProvider();
        }

        public IDocumentService? DocumentService => _serviceProvider.GetService<IDocumentService>();

        public IMarkdownService? MarkdownService => _serviceProvider.GetService<IMarkdownService>();
                
        public string? Readme
        {            
            get
            {
                string readme = $"__{MarkdownService?.Gradient("Clif.")}__" +
                    " a terminal-base __Note Library__ in *Markdown*.\n" +
                    "usage: clif [OPTION]";
                return MarkdownService?.Render(readme);
            }
        }

        public string Markdown
        {
            get
            {
                string markdown = MarkdownService?.Render($"__{MarkdownService?.Gradient("clif. Markdown CheatSheet")}__\n\n") +
                    MarkdownService?.Render("#Header 1") + "         #Header 1\n" +
                    MarkdownService?.Render("##Header 2") + "         ##Header 2\n" +
                    MarkdownService?.Render("###Header 3") + "         ###Header 3\n\n" +
                    MarkdownService?.Render("Normal") + "           Normal\n" +
                    MarkdownService?.Render("*Italic*") + "           *Italic* or _Italic_\n" +
                    MarkdownService?.Render("**Bold**") + "             **Bold** or __Bold__\n" +
                    MarkdownService?.Render("***Bold-Italic***") + "      ***Bold-Italic*** or ___Bold-Italic___\n" +
                    MarkdownService?.Render("~Underline~") + "        ~Underline~\n" +
                    MarkdownService?.Render("~~Strike~~") + "           ~~Strike~~\n" +
                    MarkdownService?.Render("~~~Dim~~~") + "              ~~~Dim~~~\n" +
                    MarkdownService?.Render("%Blink%") + "            %Blink%\n\n" +
                    MarkdownService?.Render("==Highlight==") + "        ==Highlight==\n\n" +
                    MarkdownService?.Render(">Blockquote") + "    >Blockquote\n\n" +
                    MarkdownService?.Render("`Code`") + "           `Code`\n\n" +
                    MarkdownService?.Render("[Link](http://url.com)") + "             [Link](http://url.com)\n\n" +
                    MarkdownService?.Render("![Image](clif.png)") + "        ![Image](clif.png)\n\n" +
                    "                 --- Rule\n" + MarkdownService?.Render("---");
                return markdown;
            }
        }
    }
}