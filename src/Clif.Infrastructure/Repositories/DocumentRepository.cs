using System;
using System.ComponentModel.DataAnnotations;
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
                return new RepositoryResult
                {
                    Success = true,
                    Message = "success.",
                    Documents = documents,
                };
            }
            catch
            {
                return new RepositoryResult { Message = "an unexpected error accured!" };
            }
        }

        public RepositoryResult Find(FindFilter key, string value)
        {
            try
            {
                var liteDocuments = key switch
                {
                    FindFilter.Id => _context.Documents.Find(p => p.Id == Convert.ToInt32(value)),
                    FindFilter.Title => _context.Documents.Find(p => p.Title.Equals(value.Trim(), StringComparison.CurrentCultureIgnoreCase)),
                    FindFilter.Category => _context.Documents.Find(p => p.Category == value.Trim()),
                    _ => [],
                };
                return new RepositoryResult
                {
                    Success = true,
                    Message = "success.",
                    Documents = liteDocuments.Select(mapToDocument),
                };
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
                    Documents = Find(FindFilter.Id, id.AsInt32.ToString()).Documents,
                };
            }
            catch (Exception ex)
            {
                if (ex.Message.IndexOf("insert duplicate key") > -1)
                    return new RepositoryResult { Message = "title field nust be unique!" };
                return new RepositoryResult { Message = "an unexpected error accured!" };
            }
        }

        public RepositoryResult Update(int id, Document document)
        {
            try
            {
                var update = Find(FindFilter.Id, id.ToString()).Documents?.First();
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
                            Documents = Find(FindFilter.Id, id.ToString()).Documents,
                        };
                    else
                        return new RepositoryResult { Message = "cannot update the document!" };
                }
                else
                    return new RepositoryResult { Message = "no document found to update!" };
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
                var delete = Find(FindFilter.Id, id.ToString()).Documents?.First();
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
                    return new RepositoryResult { Message = "no document found to delete!" };
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