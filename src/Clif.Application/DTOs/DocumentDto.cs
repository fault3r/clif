using System;

namespace Clif.Application.DTOs
{
    public record DocumentDto(
        string? Id, string Title, string? Content, DateTime Updated, string Group);
}