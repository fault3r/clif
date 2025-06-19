using System;

namespace Clif.Application.DTOs
{
    public record AddDocumentDto(
        string Title, string? Content, DateTime Updated, string Group);
}