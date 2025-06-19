using System;
using Clif.Application.DTOs;
using Clif.Application.Interfaces;
using Clif.Application.Services;
using Clif.Domain.Interfaces;
using Clif.Infrastructure.Data.Contexts;
using Clif.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Clif
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var services = new ServiceCollection()
                .AddSingleton<LiteDbContext>(provider => new LiteDbContext("ClifDb.db", "Documents"))
                .AddSingleton<IDocumentRepository, DocumentRepository>()
                .AddSingleton<IDocumentService, DocumentService>()
                .BuildServiceProvider();
            var _documentService = services.GetService<IDocumentService>();
            var added = _documentService?.Add(new AddDocumentDto(
                "test title", "__test content__", DateTime.UtcNow, "Main"));
            Console.WriteLine("\n"+added?.Documents?.FirstOrDefault()?.Id.ToString() + " was added");
            var result = _documentService?.GetAll();
            foreach (var item in result.Documents)
            {
                Console.WriteLine(item.Title);
            }
        }
    }
}