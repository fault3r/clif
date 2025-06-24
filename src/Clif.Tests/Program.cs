using System;
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
            var _database = new LiteDatabase("testdb.db");
            var _collection = _database.GetCollection<Document>("Documents");
            _collection.EnsureIndex(p => p.Title, unique: true);
            _collection.Insert(new Document { Title = "test" });
            var docs = _collection.FindAll();
            foreach (var doc in docs)
                Console.WriteLine($"Id: {doc.Id} | Title: {doc.Title}");
        }
    }
}