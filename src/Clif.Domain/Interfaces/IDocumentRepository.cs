using System;
using Clif.Domain.DTOs;
using Clif.Domain.Entities;

namespace Clif.Domain.Interfaces
{
    public interface IDocumentRepository
    {
        RepositoryResult GetAll();

        RepositoryResult GetBy(FindFilter key, string value);

        RepositoryResult Add(Document document);

        RepositoryResult Update(int id, Document document);

        RepositoryResult Delete(int id);

        bool Exists(string title);

        enum FindFilter
        {
            Id,
            Title,
            Category,
            Find,
        }
    }
}