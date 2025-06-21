using Clif.Application.DTOs;
using Clif.Application.Interfaces;
using Clif.Application.Services;
using Clif.Domain.Interfaces;
using Clif.Infrastructure.Configurations;
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
                .AddSingleton<LiteDbContext>(p => new LiteDbContext(
                    new LiteDbSettings
                    {
                        ConnectionString = "dbclif.db",
                        CollectionName = "Documents",
                    }))
                .AddSingleton<IDocumentRepository, DocumentRepository>()
                .AddSingleton<IDocumentService, DocumentService>()
                .AddSingleton<IClifMarkdown, ClifMarkdown>()
                .AddSingleton<IMarkdownService, MarkdownService>()
                .BuildServiceProvider();
        }

        public IDocumentService? DocumentService => _serviceProvider.GetService<IDocumentService>();

        public IMarkdownService? MarkdownService => _serviceProvider.GetService<IMarkdownService>();    

        public void Run(string[] args)
        {
            // args = ["-a", "hamed"];

            if (args.Length > 0)
            {
                switch (args[0])
                {
                    case "--markdown":
                    case "-m":
                        Console.WriteLine(Markdown);
                        break;
                    case "--help":
                    case "-h":
                        Console.WriteLine("Help Document");
                        break;
                    case "--file":
                    case "-f":
                        FileMarkdown(args);
                        break;
                    case "--list":
                    case "-l":
                        List();
                        break;
                    case "--add":
                    case "-a":
                        Add(args);
                        break;
                    default:
                        Invalid(args[0]);
                        break;
                }
            }
            else
                Console.WriteLine(Readme);
        }

        public void List()
        {
            Console.WriteLine("Documents List:\n");
            var documents = DocumentService?.GetAll().Documents;
            if (documents?.Count() > 0)
                foreach (var document in documents)
                    Console.WriteLine($"Id: {document.Id}\nTitle: {document.Title}\nContent: {document.Content}\nUpdated: {document.Updated}\nGroup: {document.Group}\n");
            else
                Console.WriteLine("no document found!");
        }

        public void Add(string[] args)
        {
            if (args.Length > 1)
            {
                if (DocumentService.Exists(args[1]))
                {
                    Console.WriteLine("the document already exists!");
                    return;
                }
                Console.WriteLine("Add a new document\n");
                Console.Write("Content: ");
                var content = Console.ReadLine();
                Console.Write("Group: ");
                var group = Console.ReadLine();
                var res = DocumentService?.Add(new NewDocumentDto(args[1], content, DateTime.UtcNow, group));
                Console.WriteLine(res?.Message ?? "null");
            }
            else
                Console.WriteLine("invalid command!");
        }

        public void FileMarkdown(string[] args)
        {
            if (args.Length > 1)
            {
                string file = args[1];
                if (File.Exists(file))
                {
                    string[] lines = File.ReadAllLines(file);
                    foreach (var line in lines)
                        Console.WriteLine(MarkdownService?.Render(line) ?? "(Error Loading Line)");
                }
                else
                    Console.WriteLine("file not exist!");
            }
            else
                Console.WriteLine("invalid command!");
        }

        public void Invalid(string arg) =>
            Console.WriteLine($"clif: invalid option - '{arg}'\n" +
            "try 'clif --help' for more information");

        public string Readme
        {
            get
            {
                return MarkdownService?.Render($"__{MarkdownService?.Gradient("Clif.")}__ " +
                    "~terminal~-base ==**Document Library**== in __Markdown__ format.\n") +
                    "usage: clif [OPTION]\n" +
                    "'clif --help' for more information";
            }
        }

        public string Markdown
        {
            get
            {
                return MarkdownService?.Render($"__{MarkdownService?.Gradient("clif. Markdown CheatSheet")}__\n\n") +
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
            }
        }
    }
}