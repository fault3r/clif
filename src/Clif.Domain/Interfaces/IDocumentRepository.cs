using System;
using Clif.Domain.DTOs;
using Clif.Domain.Entities;

namespace Clif.Domain.Interfaces
{
    public interface IDocumentRepository
    {
        RepositoryResult Find(FindFilter field, string search);

        RepositoryResult GetAll();

        RepositoryResult GetByTitle(string title);

        RepositoryResult Add(Document document);

        RepositoryResult Update(int id, Document document);

        RepositoryResult Delete(int id);

        bool Exists(string title);

        enum FindFilter
        {
            Id,
            Title,
        }
    }
}