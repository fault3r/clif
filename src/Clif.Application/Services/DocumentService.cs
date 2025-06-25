using System;
using Clif.Application.DTOs;
using Clif.Application.Interfaces;
using Clif.Domain.Interfaces;
using Clif.Domain.Entities;
using Clif.Domain.DTOs;
using static Clif.Domain.Interfaces.IDocumentRepository;

namespace Clif.Application.Services
{
    public class DocumentService : IDocumentService
    {
        private readonly IDocumentRepository _documentRepository;

        public DocumentService(IDocumentRepository documentRepository)
        {
            _documentRepository = documentRepository;
        }

        private DocumentDto mapToDocumentDto(Document document)
        {
            return new DocumentDto(
                document.Id, document.Title, document.Content, document.Modified, document.Category);
        }

        private ServiceResult mapToServiceDto(RepositoryResult repositoryResult)
        {
            return new ServiceResult
            {
                Success = repositoryResult.Success,
                Message = repositoryResult.Message,
                Documents = repositoryResult.Documents?.Select(mapToDocumentDto),
            };
        }

        public ServiceResult GetAll()
        {
            return mapToServiceDto(_documentRepository.GetAll());
        }

        public ServiceResult GetById(int id) =>
            mapToServiceDto(_documentRepository.Find(FindFilter.Id, id.ToString()));    

        public ServiceResult GetByTitle(string title) =>
            mapToServiceDto(_documentRepository.Find(FindFilter.Id, title));    

        public ServiceResult Add(NewDocumentDto document)
        {
            return mapToServiceDto(_documentRepository.Add(new Document
            {
                Title = document.Title,
                Content = document.Content,
                Category = document.Category ?? "white",
            }));
        }

        public ServiceResult Update(string title, NewDocumentDto document)
        {
            return mapToServiceDto(_documentRepository.Update(getId(title), new Document
            {
                Title = document.Title,
                Content = document.Content,
                Category = document.Category ?? "white",
            }));
        }

        public ServiceResult Delete(string title)
        {
            return mapToServiceDto(_documentRepository.Delete(getId(title)));
        }

        public bool Exists(string title) => _documentRepository.Exists(title);

        private int getId(string title)
        {
            var document = _documentRepository.Find(FindFilter.Title, title).Documents?.First();
            return document is null ? 0 : document.Id;
        }
    }
}