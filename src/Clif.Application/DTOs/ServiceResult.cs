using System;

namespace Clif.Application.DTOs
{
    public class ServiceResult
    {
        public bool Success { get; set; }

        public string? Message { get; set; }

        public IEnumerable<DocumentDto>? Documents { get; set; }
    }
}