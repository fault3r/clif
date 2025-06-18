using System;
using LiteDB;

namespace Clif.Infrastructure.Data.Contexts.Documents
{
    public class LiteDocument
    {
        [BsonId]
        public ObjectId? Id { get; set; }

        public required string Title { get; set; }

        public string? Content { get; set; }

        public DateTime Updated { get; set; }

        public required string Group {get; set; }
        
    }
}