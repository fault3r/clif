using System;

namespace Clif.Infrastructure.Configurations
{
    public class LiteDbSettings
    {
        public required string ConnectionString { get; set; }

        public required string CollectionName { get; set; }
    }
}