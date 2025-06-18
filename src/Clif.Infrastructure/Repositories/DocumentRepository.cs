using System;
using System.Runtime.CompilerServices;
using System.Windows.Markup;
using Clif.Domain.Entities;
using Clif.Domain.Interfaces;
using Clif.Infrastructure.Data.Contexts;
using Clif.Infrastructure.Data.Contexts.Documents;
using LiteDB;

namespace Clif.Infrastructure.Repositories
{
    public class DocumentRepository : IDocumentRepository
    {
        private readonly LiteDbContext _context;

        public DocumentRepository(LiteDbContext context)
        {
            _context = context;
        }

        private Document mapDocument(LiteDocument liteDocument)
        {
            var document = new Document
            {
                Id = Convert.ToInt32(liteDocument.Id.ToString()),
                Title = liteDocument.Title,
                Content = liteDocument.Content,
                Updated = liteDocument.Updated,
                Group = liteDocument.Group,
            };
            return document;

        }
        public IEnumerable<Document>? GetAll()
        {
            var documents = _context.Documents.FindAll()
                .Select(mapDocument);
            return documents;
        }

        public Document? GetById(int id)
        {
            var document = _context.Documents.FindById(new ObjectId(id.ToString()));
            return mapDocument(document);
        }

        public IEnumerable<Document>? Find(Fields key, string value)
        {
            IEnumerable<LiteDocument>? documents = [];
            switch (key)
            {
                case Fields.Title:
                    documents = _context.Documents.Find(p => p.Title == value);
                    break;
                case Fields.Group:
                    documents = _context.Documents.Find(p => p.Group == value);
                    break;
            }
            return documents.Select(mapDocument);
        }

        public Document Add(Document document)
        {
            var id = _context.Documents.Insert(new LiteDocument
            {
                Title = document.Title,
                Content = document.Content,
                Updated = document.Updated,
                Group = document.Group,
            });
            return GetById(id.AsInt32);
        }

        public enum Fields
        {
            Title,
            Group,
        }

    }
}