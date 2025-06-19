using System;
using Clif.Application.DTOs;
using Clif.Application.Interfaces;
using Clif.Domain.Interfaces;
using Clif.Domain.Entities;
using Clif.Domain.DTOs;

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
                document.Id, document.Title, document.Content, document.Updated, document.Group);
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

        public ServiceResult GetById(string id)
        {
            return mapToServiceDto(_documentRepository.GetById(id));
        }

        public ServiceResult Add(AddDocumentDto document)
        {
            return mapToServiceDto(_documentRepository.Add(new Document
            {
                Id = "[new id]",
                Title = document.Title,
                Content = document.Content,
                Updated = document.Updated,
                Group = document.Group,
            }));
        }

        public ServiceResult Delete(string id)
        {
            return mapToServiceDto(_documentRepository.Delete(id));
        }
    }
}