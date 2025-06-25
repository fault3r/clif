using System;

namespace Clif.Application.DTOs
{
    public record NewDocumentDto(
        string Title, string? Content, string? Category);
}