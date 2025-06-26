using System;
using System.Collections.Immutable;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;
using Clif.Domain.DTOs;
using Clif.Domain.Entities;
using Clif.Domain.Interfaces;
using Clif.Infrastructure.Data.Contexts;
using Clif.Infrastructure.Data.Contexts.Documents;
using LiteDB;
using static Clif.Domain.Interfaces.IDocumentRepository;

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
            return new Document
            {
                Id = liteDocument.Id,
                Title = liteDocument.Title,
                Content = liteDocument.Content,
                Modified = liteDocument.Modified,
                Category = liteDocument.Category,
            };
        }

        public RepositoryResult GetAll()
        {
            try
            {
                var documents = _context.Documents.FindAll()
                    .OrderBy(p => p.Modified)
                    .Select(mapToDocument);
                if (documents.Any())
                    return new RepositoryResult
                    {
                        Success = true,
                        Message = "success.",
                        Documents = documents,
                    };
                else
                    return new RepositoryResult { Message = "no documents found!" };
            }
            catch
            {
                return new RepositoryResult { Message = "an unexpected error accured!" };
            }
        }

        public RepositoryResult GetBy(FindFilter key, string value)
        {
            try
            {
                var liteDocuments = key switch
                {
                    FindFilter.Id => _context.Documents.Find(p => p.Id == Convert.ToInt32(value)),
                    FindFilter.Title => _context.Documents.Find(p => p.Title.Equals(value, StringComparison.CurrentCultureIgnoreCase)),
                    FindFilter.Category => _context.Documents.Find(p => p.Category == value),
                    FindFilter.Find => _context.Documents.Find(p => p.Title.Contains(value)),
                    _ => [],
                };
                if (liteDocuments.Any())
                    return new RepositoryResult
                    {
                        Success = true,
                        Message = "success.",
                        Documents = liteDocuments.Select(mapToDocument),
                    };
                else
                    return new RepositoryResult { Message = "no documents found!" };
            }
            catch
            {
                return new RepositoryResult { Message = "an unexpected error accured!" };
            }
        }

        public RepositoryResult Add(Document document)
        {
            try
            {
                var id = _context.Documents.Insert(new LiteDocument
                {
                    Title = document.Title.Trim(),
                    Content = document.Content,
                    Modified = DateTime.UtcNow,
                    Category = document.Category,
                });
                return new RepositoryResult
                {
                    Success = true,
                    Message = "success.",
                    Documents = GetBy(FindFilter.Id, id.AsInt32.ToString()).Documents,
                };
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("insert duplicate key"))
                    return new RepositoryResult { Message = "the document already exists!" };
                return new RepositoryResult { Message = "an unexpected error accured!" };
            }
        }

        public RepositoryResult Update(int id, Document document)
        {
            try
            {
                var update = GetBy(FindFilter.Id, id.ToString()).Documents?.First();
                if (update != null)
                {
                    bool result = _context.Documents.Update(id, new LiteDocument
                    {
                        Title = document.Title.Trim(),
                        Content = document.Content,
                        Modified = DateTime.UtcNow,
                        Category = document.Category,
                    });
                    if (result)
                        return new RepositoryResult
                        {
                            Success = true,
                            Message = "success.",
                            Documents = GetBy(FindFilter.Id, id.ToString()).Documents,
                        };
                    else
                        return new RepositoryResult { Message = "cannot update the document!" };
                }
                else
                    return new RepositoryResult { Message = "the document not found!" };
            }
            catch
            {
                return new RepositoryResult { Message = "an unexpected error accured!" };
            }
        }

        public RepositoryResult Delete(int id)
        {
            try
            {
                var delete = GetBy(FindFilter.Id, id.ToString()).Documents?.First();
                if (delete != null)
                {
                    bool result = _context.Documents.Delete(id);
                    if (result)
                        return new RepositoryResult
                        {
                            Success = true,
                            Message = "success.",
                        };
                    else
                        return new RepositoryResult { Message = "cannot delete the document!" };
                }
                else
                    return new RepositoryResult { Message = "the document not found!" };
            }
            catch
            {
                return new RepositoryResult { Message = "an unexpected error accured!" };
            }
        }

        public bool Exists(string title) =>
            _context.Documents.Exists(p => p.Title.Equals(
                title.Trim(), StringComparison.CurrentCultureIgnoreCase));
    }
}