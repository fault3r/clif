using System;

namespace Clif.Application.DTOs
{
    public record DocumentDto(
        Int32 Id, string Title, string? Content, DateTime Modified, string Category);
}