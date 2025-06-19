using System;
using Clif.Application.DTOs;

namespace Clif.Application.Interfaces
{
    public interface IDocumentService
    {
        ServiceResult GetAll();

        ServiceResult GetById(string id);

        ServiceResult Add(AddDocumentDto document);

        ServiceResult Delete(string id);
    }
}