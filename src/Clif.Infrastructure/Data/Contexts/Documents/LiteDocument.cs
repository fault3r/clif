using System;
using System.ComponentModel.DataAnnotations;
using LiteDB;

namespace Clif.Infrastructure.Data.Contexts.Documents
{
    public class LiteDocument
    {
        [BsonId(true)]
        public Int32 Id { get; set; }

        public required string Title { get; set; }

        public string? Content { get; set; }

        public DateTime Modified { get; set; }

        public required string Category {get; set; }
    }
}