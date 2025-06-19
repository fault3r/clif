using System;

namespace Clif.Domain.Entities
{
    public class Document
    {
        public required string? Id { get; set; }

        public required string Title { get; set; }

        public string? Content { get; set; }

        public DateTime Updated { get; set; }

        public required string Group {get; set; }
    }
}