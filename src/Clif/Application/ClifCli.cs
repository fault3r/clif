using System.Reflection.Metadata.Ecma335;
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
                        ConnectionString = "dbClif.db",
                        CollectionName = "Documents",
                    }))
                .AddSingleton<IDocumentRepository, DocumentRepository>()
                .AddSingleton<IDocumentService, DocumentService>()
                .AddSingleton<IClifMarkdown, ClifMarkdown>()
                .AddSingleton<IMarkdownService, MarkdownService>()
                .BuildServiceProvider();
        }

        private IDocumentService DocumentService =>
            _serviceProvider.GetService<IDocumentService>() ??
            throw new Exception("clif: document service error!");

        private IMarkdownService MarkdownService =>
            _serviceProvider.GetService<IMarkdownService>() ??
            throw new Exception("clif: markdown service error!");

        public void Run(string[] args)
        {
            // args = ["-a", "testing"];
            if (args.Length > 0)
            {
                string arg = args[0];
                switch (arg)
                {
                    case "--help":
                    case "-h":
                        Console.WriteLine(Help);
                        break;
                    case "--markdown":
                    case "-m":
                        Console.WriteLine(Markdown);
                        break;
                    case "--file":
                    case "-f":
                        FileMarkdown(args);
                        break;
                    case "--list":
                    case "-l":
                        List();
                        break;
                    case "--title":
                    case "-t":
                        Title(args);
                        break;
                    case "--add":
                    case "-a":
                        Add(args);
                        break;
                    case "--delete":
                    case "-d":
                        Delete(args);
                        break;
                    default:
                        Invalid(arg);
                        break;
                }
            }
            else
                Console.WriteLine(Readme);
        }

        private void List()
        {
            Console.WriteLine(MarkdownService.Render($"__{MarkdownService.Gradient("Documents List")}__"));
            var documents = DocumentService.GetAll().Documents;
            if (documents?.Count() > 0)
                foreach (var document in documents)
                    Console.WriteLine($"Title: {document.Title}\nContent: {document.Content}\nUpdated: {document.Updated}\nGroup: {document.Group}\n");
            else
                Console.WriteLine("the document not found!");
        }

        private void Title(string[] args)
        {
            if (args.Length > 1)
            {
                Console.WriteLine(MarkdownService.Render($"__{MarkdownService.Gradient("Document")}__"));
                string title = args[1];
                var id = DocumentService.GetById(DocumentService.GetId(title) ?? ReturnTypeEncoder);
                if (id != null)
                {
                    var document = DocumentService.GetById(id).Documents?.First();
                    Console.WriteLine($"Title: {document.Title}\nContent: {document.Content}\nUpdated: {document.Updated}\nGroup: {document.Group}\n");
                }
                else
                    Console.WriteLine("the document not found!");
            }
            else
                Console.WriteLine("clif: invalid '--title' option command!");
        }

        private void Add(string[] args)
        {
            if (args.Length > 1)
            {
                string title = args[1];
                if (DocumentService.Exists(title))
                {
                    Console.WriteLine("clif: the document already exists!");
                    return;
                }
                Console.WriteLine(MarkdownService.Render($"__{MarkdownService.Gradient("New Document")}__"));
                Console.Write("Content: ");
                var content = Console.ReadLine(); //fix
                Console.Write("Group: ");
                var group = Console.ReadLine();
                group = group?.Trim() == "" ? null : group;
                var result = DocumentService.Add(new NewDocumentDto(
                    title, content, DateTime.UtcNow, group ?? "main"));
                Console.WriteLine(result.Message);
            }
            else
                Console.WriteLine("clif: invalid '--add' option command!");
        }

        private void Delete(string[] args)
        {
            if (args.Length > 1)
            {
                string title = args[1];
                bool yes = false;
                if (args.Length > 2 && (args[2] == "-y" || args[2] == "--yes"))
                    yes = true;
                if (!DocumentService.Exists(title))
                {
                    Console.WriteLine("clif: the document not found!");
                    return;
                }
                Console.WriteLine(MarkdownService.Render($"__{MarkdownService.Gradient("Delete Document")}__"));
                if (!yes)
                {
                    Console.Write("are you sure? (y/n): ");
                    if (Console.ReadKey().Key != ConsoleKey.Y)
                    {
                        Console.WriteLine("\naborted!");
                        return;
                    }
                }                
                var id = DocumentService.GetId(title);
                if (id is null)
                    Console.WriteLine("\nclif: an unexpected error accured!");
                else
                {
                    var result = DocumentService.Delete(id);
                    Console.WriteLine((yes ? "" : "\n") + result.Message);
                }
            }
            else
                Console.WriteLine("clif: invalid '--delete' option command!");
        }

        private void FileMarkdown(string[] args)
        {
            if (args.Length > 1)
            {
                string file = args[1];
                if (File.Exists(file))
                {
                    string[] lines = File.ReadAllLines(file);
                    foreach (var line in lines)
                        Console.WriteLine(MarkdownService.Render(line));
                }
                else
                    Console.WriteLine("clif: file not exist!");
            }
            else
                Console.WriteLine("clif: invalid '--file' option command!");
        }

        private void Invalid(string arg) =>
            Console.WriteLine($"clif: invalid option! - '{arg}'\n" +
                "try 'clif --help' for more information");

        private string Readme =>
            MarkdownService.Render($"__{MarkdownService.Gradient("Clif.")}__ " +
                "~terminal~-base ==**Document Library**== in __Markdown__ format.\n") +
                "usage: clif [OPTION]...";

        private string Help =>
            MarkdownService.Render($"__{MarkdownService.Gradient("Help")}__\n" +
                "[Help Document Here]");

        private string Markdown =>
            MarkdownService.Render($"__{MarkdownService.Gradient("Markdown")}__\n\n") +
                MarkdownService.Render("#Header 1") + "         #Header 1\n" +
                MarkdownService.Render("##Header 2") + "         ##Header 2\n" +
                MarkdownService.Render("###Header 3") + "         ###Header 3\n\n" +
                MarkdownService.Render("Normal") + "           Normal\n" +
                MarkdownService.Render("*Italic*") + "           *Italic* or _Italic_\n" +
                MarkdownService.Render("**Bold**") + "             **Bold** or __Bold__\n" +
                MarkdownService.Render("***Bold-Italic***") + "      ***Bold-Italic*** or ___Bold-Italic___\n" +
                MarkdownService.Render("~Underline~") + "        ~Underline~\n" +
                MarkdownService.Render("~~Strike~~") + "           ~~Strike~~\n" +
                MarkdownService.Render("~~~Dim~~~") + "              ~~~Dim~~~\n" +
                MarkdownService.Render("%Blink%") + "            %Blink%\n\n" +
                MarkdownService.Render("==Highlight==") + "        ==Highlight==\n\n" +
                MarkdownService.Render(">Blockquote") + "    >Blockquote\n\n" +
                MarkdownService.Render("`Code`") + "           `Code`\n\n" +
                MarkdownService.Render("[Link](http://url.com)") + "             [Link](http://url.com)\n\n" +
                MarkdownService.Render("![Image](clif.png)") + "        ![Image](clif.png)\n\n" +
                "                 --- Horizontal-Rule\n" + MarkdownService.Render("---");
    }
}