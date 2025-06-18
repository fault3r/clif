using System;
using Clif.Domain.Entities;
using Clif.Infrastructure.Data.Contexts.Documents;
using LiteDB;

namespace Clif.Infrastructure.Data.Contexts;

public class LiteDbContext
{
    private readonly ILiteDatabase _liteDatabase;

    private readonly string _database;
    private readonly string _collection;

    public LiteDbContext(string database, string collection)
    {
        _database = database;
        _collection = collection;
        _liteDatabase = new LiteDatabase(_database);
    }

    public ILiteCollection<LiteDocument> Documents => _liteDatabase.GetCollection<LiteDocument>(_collection);

    public string Database { get { return _database; } }
    public string Collection { get { return _collection; } }
}