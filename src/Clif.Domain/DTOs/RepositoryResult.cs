using System;
using Clif.Domain.Entities;

namespace Clif.Domain.DTOs
{
    public class RepositoryResult
    {
        public bool Success { get; set; } = false;

        public string? Message { get; set; }

        public IEnumerable<Document>? Documents { get; set; }
    }
}