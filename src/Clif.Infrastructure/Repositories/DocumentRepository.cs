using System;
using Clif.Domain.DTOs;
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

        private Document mapToDocument(LiteDocument liteDocument)
        {
            var document = new Document
            {
                Id = liteDocument.Id is null ? "[id]" : liteDocument.Id.ToString(),
                Title = liteDocument.Title,
                Content = liteDocument.Content,
                Updated = liteDocument.Updated,
                Group = liteDocument.Group,
            };
            return document;
        }

        public RepositoryResult GetAll()
        {
            try
            {
                var documents = _context.Documents.FindAll().OrderBy(p => p.Updated)
                    .Select(mapToDocument);
                return new RepositoryResult
                {
                    Success = true,
                    Message = "Success.",
                    Documents = documents,
                };
            }
            catch (Exception ex)
            {
                return new RepositoryResult { Message = ex.Message };
            }
        }

        public RepositoryResult GetById(string id)
        {
            try
            {
                var document = _context.Documents.FindById(new ObjectId(id));
                if (document is null)
                    return new RepositoryResult { Message = "Document not found!" };
                else
                    return new RepositoryResult
                    {
                        Success = true,
                        Message = "Success.",
                        Documents = [mapToDocument(document)],
                    };
            }
            catch (Exception ex)
            {
                return new RepositoryResult { Message = ex.Message };
            }
        }

        public RepositoryResult Add(Document document)
        {
            try
            {
                var id = _context.Documents.Insert(new LiteDocument
                {
                    Title = document.Title,
                    Content = document.Content,
                    Updated = document.Updated,
                    Group = document.Group,
                });
                if (id is null)
                    return new RepositoryResult { Message = "An unexcepted error accured!" };
                else
                    return new RepositoryResult
                    {
                        Success = true,
                        Message = "Success.",
                        Documents = GetById(id.RawValue.ToString()??"[id]").Documents,
                    };
            }
            catch (Exception ex)
            {
                return new RepositoryResult { Message = ex.Message };
            }
        }

        public RepositoryResult Delete(string id)
        {
            try
            {
                var result = _context.Documents.Delete(new ObjectId(id));
                if (!result)
                    return new RepositoryResult { Message = "Can not delete document!" };
                else
                    return new RepositoryResult
                    {
                        Success = true,
                        Message = "Success.",
                    };
            }
            catch (Exception ex)
            {
                return new RepositoryResult { Message = ex.Message };
            }
        }
    }
}