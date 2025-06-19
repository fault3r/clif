using System;
using Clif.Application;
using Clif.Application.Interfaces;

namespace Clif
{
    class Program
    {
        private static ClifCli? _clifCli = new();

        public static void Main(string[] args)
        {
            IDocumentService? clif = _clifCli?.DocumentService;
            Console.Clear();

            // Console.WriteLine(clif?.Delete("6853fc2d59447f040f294c67").Message);

            // string id = "685401b9d7fb2401ddba9033";
            // clif?.Update(id, new NewDocumentDto(
            //     "Hamed Damavandi", "Hello, It's me.", DateTime.UtcNow, "Test"));

            // Console.WriteLine(clif?.GetById(id).Documents?.First().Title+ "\n");
            
            Console.WriteLine(_clifCli?.Readme);
            Console.WriteLine(_clifCli?.Markdown);

            // Console.WriteLine(_clifCli?.MarkdownService?.Render("#Hello World"));

            //var added = clif.Add(new AddDocumentDto($"Title {new Random().Next(100, 200).ToString()}", "content", DateTime.UtcNow, "Main"));
            //Console.WriteLine(added.Documents?.First().Id + " <- added to the document list.");

            var docs = clif?.GetAll().Documents;
            if (docs != null)
            {
                Console.WriteLine("List of all documents:");
                foreach (var doc in docs)
                {
                    string item = $"\nId: {doc.Id}\nTitle: {doc.Title}\nContent: {doc.Content}\nUpdated: {doc.Updated}\nGroup: {doc.Group}";
                    Console.WriteLine(item);
                }
            }
        }
    }
}