
### BLACKBOX.AI

To make the repository generic and work with any type that has a `Guid` as its identifier, you can use a generic repository pattern. Here's how you can implement it in .NET:

### **1. Create a Base Domain Entity Interface**
First, define an interface that all domain entities must implement. This ensures they have an `Id` property of type `Guid`.

```csharp
public interface IEntity
{
    Guid Id { get; set; }
}
```

### **2. Define the Domain Model (Implement `IEntity`)**
Your domain entities (e.g., `User`, `Product`) should implement `IEntity`.

```csharp
public class User : IEntity
{
    public Guid Id { get; set; }  
    public string Name { get; set; }
    public string Email { get; set; }
}
```

### **3. Create a Base MongoDB Document Interface**
Ensure MongoDB documents have mappings for `ObjectId` and conversion methods.

```csharp
public interface IMongoDocument
{
    ObjectId ObjectId { get; set; }
}

public class UserDocument : IMongoDocument
{
    [BsonId]
    public ObjectId ObjectId { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }

    // Convert to Domain Entity
    public User ToDomain()
    {
        return new User
        {
            Id = Guid.Parse(ObjectId.ToString()),
            Name = Name,
            Email = Email
        };
    }

    // Convert from Domain Entity
    public static UserDocument FromDomain(User user)
    {
        return new UserDocument
        {
            ObjectId = ObjectId.Parse(user.Id.ToString()),
            Name = user.Name,
            Email = user.Email
        };
    }
}
```

### **4. Implement the Generic Repository**
Instead of having a separate repository for each entity, we create a single `GenericRepository<TEntity, TDocument>`.  

```csharp
using MongoDB.Driver;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;

public class GenericRepository<TEntity, TDocument> where TEntity : IEntity
                                                 where TDocument : IMongoDocument
{
    private readonly IMongoCollection<TDocument> _collection;

    public GenericRepository(IMongoDatabase database, string collectionName)
    {
        _collection = database.GetCollection<TDocument>(collectionName);
    }

    public async Task<TEntity> GetByIdAsync(Guid id)
    {
        var document = await _collection.Find(d => d.ObjectId == ObjectId.Parse(id.ToString())).FirstOrDefaultAsync();
        return ((dynamic)document)?.ToDomain();
    }

    public async Task CreateAsync(TEntity entity)
    {
        var document = ((dynamic)Activator.CreateInstance<TDocument>()).FromDomain(entity);
        await _collection.InsertOneAsync(document);
    }

    public async Task UpdateAsync(TEntity entity)
    {
        var document = ((dynamic)Activator.CreateInstance<TDocument>()).FromDomain(entity);
        await _collection.ReplaceOneAsync(d => d.ObjectId == ObjectId.Parse(entity.Id.ToString()), document);
    }

    public async Task DeleteAsync(Guid id)
    {
        await _collection.DeleteOneAsync(d => d.ObjectId == ObjectId.Parse(id.ToString()));
    }

    // Additional methods as needed (e.g., GetAll)
    public async Task<List<TEntity>> GetAllAsync()
    {
        var documents = await _collection.Find(_ => true).ToListAsync();
        var entities = new List<TEntity>();
        
        foreach (var doc in documents)
        {
            entities.Add(((dynamic)doc).ToDomain());
        }
        
        return entities;
    }
}
```

### **5. Register and Use the Repository**
Now, you can inject and use the repository in your application.

#### **Register in DI (e.g., ASP.NET Core Startup)**
```csharp
services.AddSingleton<IMongoDatabase>(provider =>
{
    var client = new MongoClient("mongodb://localhost:27017");
    return client.GetDatabase("MyDatabase");
});

services.AddTransient<GenericRepository<User, UserDocument>>(provider =>
{
    var database = provider.GetRequiredService<IMongoDatabase>();
    return new GenericRepository<User, UserDocument>(database, "users");
});

services.AddTransient<GenericRepository<Product, ProductDocument>>(provider =>
{
    var database = provider.GetRequiredService<IMongoDatabase>();
    return new GenericRepository<Product, ProductDocument>(database, "products");
});
```

#### **Usage in a Service or Controller**
```csharp
public class UserService
{
    private readonly GenericRepository<User, UserDocument> _userRepository;

    public UserService(GenericRepository<User, UserDocument> userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<User> GetUserByIdAsync(Guid id)
    {
        return await _userRepository.GetByIdAsync(id);
    }

    public async Task CreateUserAsync(User user)
    {
        await _userRepository.CreateAsync(user);
    }
}
```

### **Key Takeaways**
✅ **Domain Layer Clean:** Your domain models (`User`, `Product`) don’t depend on MongoDB.  
✅ **Generic Repository:** Eliminates duplicate code for CRUD operations.  
✅ **Dynamic Conversion:** Uses `dynamic` to invoke `ToDomain()` and `FromDomain()` methods.  
✅ **Future Extensibility:** Easy to add new entities (`TEntity`, `TDocument`) without changing the repository.  

This design ensures a **clean separation of concerns**, **less code duplication**, and **better testability**. 🚀
