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
            // args = ["-m"];
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
                    case "--search":
                    case "-s":
                        Search(args);
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

        private void writeDocuments(IEnumerable<DocumentDto> documents)
        {
            foreach (var document in documents)
                Console.WriteLine($"Id: {document.Id}\nTitle: {document.Title}\nContent: {document.Content}\nModified: {document.Modified}\nCategory: {document.Category}\n");
        }

        private void List()
        {
            Console.WriteLine(MarkdownService.Render($"__{MarkdownService.Gradient("Documents List")}__"));
            var result = DocumentService.GetAll();
            if (result.Success)
            {
                writeDocuments(result.Documents);
                Console.WriteLine($"count: {result.Documents.Count()}");
            }
            Console.WriteLine($"clif: {result.Message}");
        }

        private void Title(string[] args)
        {
            if (args.Length > 1)
            {
                Console.WriteLine(MarkdownService.Render($"__{MarkdownService.Gradient("Document")}__"));
                string title = args[1];
                var result = DocumentService.GetByTitle(title);
                if (result.Success)
                    writeDocuments(result.Documents);
                Console.WriteLine($"clif: {result.Message}");
            }
            else
                Console.WriteLine("clif: invalid '--title' option command!");
        }

        private void Search(string[] args)
        {
            if (args.Length > 1)
            {
                Console.WriteLine(MarkdownService.Render($"__{MarkdownService.Gradient("Search Documents")}__"));
                string text = args[1];
                var result = DocumentService.FindByTitle(text);
                if (result.Success)
                {
                    writeDocuments(result.Documents);
                    Console.WriteLine($"count: {result.Documents.Count()}");
                }
                Console.WriteLine($"clif: {result.Message}");
            }
            else
                Console.WriteLine("clif: invalid '--search' option command!");
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
                Console.WriteLine(MarkdownService.Render($"__{MarkdownService.Gradient("Add Document")}__"));
                Console.Write("Content: ");
                var content = Console.ReadLine(); //fix
                Console.Write("Category: ");
                var category = Console.ReadLine();
                category = category?.Trim() == "" ? null : category;
                var result = DocumentService.Add(new NewDocumentDto(
                    title, content, category));
                Console.WriteLine($"clif: {result.Message}");
            }
            else
                Console.WriteLine("clif: invalid '--add' option command!");
        }

        private void Delete(string[] args)
        {
            if (args.Length > 1)
            {
                string title = args[1];
                if (!DocumentService.Exists(title))
                {
                    Console.WriteLine("clif: the document not found!");
                    return;
                }
                Console.WriteLine(MarkdownService.Render($"__{MarkdownService.Gradient("Delete Document")}__"));
                if (!(args.Length > 2 && (args[2] == "-y" || args[2] == "--yes")))
                {
                    Console.Write("clif: are you sure? (y/n): ");
                    if (Console.ReadKey().Key != ConsoleKey.Y)
                    {
                        Console.WriteLine("\nclif: aborted!");
                        return;
                    }
                }
                var result = DocumentService.Delete(title);
                Console.WriteLine($"\nclif: {result.Message}");
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
            Console.WriteLine(MarkdownService.Render($"__{MarkdownService.Gradient("Error")}__\n") +
                "clif: try '--help' for more information\n" +
                $"clif: invalid option! - '{arg}'");

        private string Readme =>
            MarkdownService.Render($"__{MarkdownService.Gradient("Clif.")}__ " +
                "~terminal~-base ==**Document Library**== in __Markdown__ format.\n") +
                "usage: clif [OPTION]...";

        private string Help =>
            MarkdownService.Render($"__{MarkdownService.Gradient("Help")}__\n" +
                "[Help Document Here]");

        private string Markdown =>
            MarkdownService.Render($"__{MarkdownService.Gradient("Markdown")}__") + "\n\n" +
                MarkdownService.Render("# Header 1") + "         # Header 1" +"\n" +
                MarkdownService.Render("## Header 2") + "         ## Header 2" +"\n" +
                MarkdownService.Render("### Header 3") + "         ### Header 3" + "\n\n" +
                MarkdownService.Render("Normal") + "           Normal" + "\n" +
                MarkdownService.Render("*Italic*") + "           *Italic* or _Italic_" +"\n" +
                MarkdownService.Render("**Bold**") + "             **Bold** or __Bold__" +"\n" +
                MarkdownService.Render("***Bold-Italic***") + "      ***Bold-Italic*** or ___Bold-Italic__n" +"\n" +
                MarkdownService.Render("~Underline~") + "        ~Underline~" +"\n" +
                MarkdownService.Render("~~Strike~~") + "           ~~Strike~~" +"\n" +
                MarkdownService.Render("~~~Dim~~~") + "              ~~~Dim~~~" +"\n" +
                MarkdownService.Render("%Blink%") + "            %Blink%" +"\n\n" +
                MarkdownService.Render("==Highlight==") + "        ==Highlight==" +"\n\n" +
                MarkdownService.Render("> Blockquote") + "    > Blockquote" + "\n\n" +
                MarkdownService.Render("`Code`") + "           `Code`" + "\n\n" +
                MarkdownService.Render("[Link](http://url.com)") + "             [Link](http://url.com)" +"\n\n" +
                MarkdownService.Render("![Image](clif.png)") + "        ![Image](clif.png)" +"\n\n" +
                MarkdownService.Render("### Task List") + "\n" +
                MarkdownService.Render("- [x] Item1") + "    - [x] Item1" +"\n" +
                MarkdownService.Render("- [ ] Item2") + "    - [ ] Item2" +"\n" +
                MarkdownService.Render("- [ ] Item3") + "    - [ ] Item3" + "\n\n" +
                MarkdownService.Render("# Unordered List") +"\n" +
                MarkdownService.Render("- Item1") + "    - Item1" + "\n" +
                MarkdownService.Render("- Item2") + "    - Item2" +"\n" +
                MarkdownService.Render("- Item3") + "    - Item3" +"\n\n" +
                MarkdownService.Render("## Ordered List") +"\n" +
                MarkdownService.Render("1. Item1") + "    1. Item1" +"\n" +
                MarkdownService.Render("2. Item2") + "    2. Item2" +"\n" +
                MarkdownService.Render("3. Item3") + "    3. Item3" +"\n\n" +
                "                 --- Horizontal-Rule\n" + MarkdownService.Render("---");
    }
}