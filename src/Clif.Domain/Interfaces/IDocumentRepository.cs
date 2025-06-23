using System;
using Clif.Domain.DTOs;
using Clif.Domain.Entities;

namespace Clif.Domain.Interfaces
{
    public interface IDocumentRepository
    {
        RepositoryResult GetAll();

        RepositoryResult GetById(string id);

        RepositoryResult Add(Document document);

        RepositoryResult Update(string id, Document document);

        RepositoryResult Delete(string id);

        string? GetId(string title);

        bool Exists(string title);
    }
}