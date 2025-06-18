using System;

namespace Clif.Domain.Entities
{
    public class Document
    {
        public int Id { get; set; }

        public required string Title { get; set; }

        public string? Content { get; set; }

        public DateTime Updated { get; set;}
    }
}