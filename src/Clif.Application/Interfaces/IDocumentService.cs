using System;
using Clif.Application.DTOs;

namespace Clif.Application.Interfaces
{
    public interface IDocumentService
    {
        ServiceResult GetAll();

        ServiceResult GetById(string id);

        ServiceResult Add(NewDocumentDto document);

        ServiceResult Update(string id, NewDocumentDto document);

        ServiceResult Delete(string id);
    }
}