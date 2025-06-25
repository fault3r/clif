using System;
using System.Runtime.CompilerServices;
using Clif.Domain.Entities;
using Clif.Infrastructure.Configurations;
using Clif.Infrastructure.Data.Contexts;
using Clif.Infrastructure.Repositories;
using LiteDB;

namespace Clif.Tests
{

    public class Document
    {
        [BsonId]
        public int Id { get; set; }

        public required string Title { get; set; }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            // var _database = new LiteDatabase("testdb.db");
            // var _collection = _database.GetCollection<Document>("Documents");
            // _collection.EnsureIndex(p => p.Title, unique: true);
            // _collection.Insert(new Document { Title = "test" });
            // var docs = _collection.FindAll();
            // foreach (var doc in docs)
            //     Console.WriteLine($"Id: {doc.Id} | Title: {doc.Title}");

            DocumentRepository repo = new(new LiteDbContext(new LiteDbSettings
            {
                ConnectionString = "testdb.db",
                CollectionName = "Documents",
            }));
            var res = repo.Update(new Domain.Entities.Document
            {
                Title = "test two",
                Content = "aaaa",
                Category = "dddddd",
            });
            var docs = repo.GetAll();
            foreach (var doc in docs.Documents)
                Console.WriteLine(doc.Id + " " + doc.Title);
            Console.WriteLine(docs.Message);
            // var res2 = repo.Delete(res.Documents.First().Id);
        }
    }
}