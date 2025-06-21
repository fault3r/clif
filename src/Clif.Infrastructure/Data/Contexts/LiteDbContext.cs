using System;
using Clif.Domain.Entities;
using Clif.Infrastructure.Configurations;
using Clif.Infrastructure.Data.Contexts.Documents;
using LiteDB;

namespace Clif.Infrastructure.Data.Contexts;

public class LiteDbContext
{
    private readonly ILiteDatabase _liteDatabase;

    private readonly ILiteCollection<LiteDocument> _documents;

    public LiteDbContext(LiteDbSettings settings)
    {
        _liteDatabase = new LiteDatabase(settings.ConnectionString);

        _documents = _liteDatabase.GetCollection<LiteDocument>(settings.CollectionName);
        _documents.EnsureIndex(p => p.Title, unique: true);
    }

    public ILiteCollection<LiteDocument> Documents => _documents;
}