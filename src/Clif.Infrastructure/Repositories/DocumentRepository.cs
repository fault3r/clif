using System;
using System.ComponentModel.DataAnnotations;
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
                Id = liteDocument.Id?.ToString() ?? null,
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
                    Message = "success.",
                    Documents = documents,
                };
            }
            catch (Exception ex)
            {
                return new RepositoryResult { Message = $"error: {ex.Message}" };
            }
        }

        public RepositoryResult GetById(string id)
        {
            try
            {
                var document = _context.Documents.FindById(new ObjectId(id));
                if (document is null)
                    return new RepositoryResult { Message = "document not found!" };
                else
                    return new RepositoryResult
                    {
                        Success = true,
                        Message = "success.",
                        Documents = [mapToDocument(document)],
                    };
            }
            catch (Exception ex)
            {
                return new RepositoryResult { Message = $"error: {ex.Message}" };
            }
        }

        public RepositoryResult Add(Document document)
        {
            try
            {
                var id = _context.Documents.Insert(new LiteDocument
                {
                    Id = null,
                    Title = document.Title.Trim(),
                    Content = document.Content,
                    Updated = document.Updated,
                    Group = document.Group,
                });
                if (id != null)
                {
                    return new RepositoryResult
                    {
                        Success = true,
                        Message = "success.",
                        Documents = GetById(id.AsObjectId.ToString()).Documents,
                    };
                }
                else
                    return new RepositoryResult { Message = "an unexpected error accured!" };

            }
            catch (Exception ex)
            {
                if (ex.Message.IndexOf("insert duplicate key") > -1)
                    return new RepositoryResult { Message = "title field must be unique, choose a different title!" };
                return new RepositoryResult { Message = $"error: {ex.Message}" };
            }
        }

        public RepositoryResult Update(string id, Document document)
        {
            try
            {
                var update = GetById(id).Documents?.First();
                if (update is null)
                    return new RepositoryResult { Message = "can not find the document!" };
                else
                {
                    _context.Documents.Update(new ObjectId(update.Id), new LiteDocument
                    {
                        Id = null,
                        Title = document.Title.Trim(),
                        Content = document.Content,
                        Updated = document.Updated,
                        Group = document.Group,
                    });
                    return new RepositoryResult
                    {
                        Success = true,
                        Message = "success.",
                        Documents = GetById(id).Documents,
                    };
                }
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
                    return new RepositoryResult { Message = "can not delete the document!" };
                else
                    return new RepositoryResult
                    {
                        Success = true,
                        Message = "success.",
                    };
            }
            catch (Exception ex)
            {
                return new RepositoryResult { Message = ex.Message };
            }
        }

        public bool Exists(string title) =>
            _context.Documents.Exists(p => p.Title.Equals(
                title.Trim(), StringComparison.CurrentCultureIgnoreCase));
    }
}