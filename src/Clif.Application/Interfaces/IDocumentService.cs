using System;
using Clif.Application.DTOs;

namespace Clif.Application.Interfaces
{
    public interface IDocumentService
    {
        ServiceResult GetAll();

        ServiceResult GetByTitle(string title);
        
        ServiceResult FindByTitle(string title);

        ServiceResult Add(NewDocumentDto document);

        ServiceResult Update(string title, NewDocumentDto document);

        ServiceResult Delete(string title);

        bool Exists(string title);
    }
}