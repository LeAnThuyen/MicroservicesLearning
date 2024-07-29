using System.Linq.Expressions;
using Inventory.Product.API.Entities.Abstractions;
using Inventory.Product.API.Extensions;
using MongoDB.Driver;

namespace Inventory.Product.API.Repositories.Abstraction;

public class MongoDbRepository<T> :IMongoDbRepositoryBase<T> where T:MongoEntity
{
    private  IMongoDatabase Database { get;}

    public MongoDbRepository(IMongoClient client, DatabaseSettings settings)
    {
        Database = client.GetDatabase(settings.DatabaseName);
    }

    public IMongoCollection<T> FindAll(ReadPreference? readPreference = null)
        => Database.WithReadPreference(readPreference ?? ReadPreference.Primary).GetCollection<T>(GetCollectionName());

    public Task CreateAsync(T entity) => Collection.InsertOneAsync(entity);
    
    protected virtual IMongoCollection<T> Collection => Database.GetCollection<T>(GetCollectionName());
    public Task UpdateAsync(T entity)
    {
        Expression<Func<T, string>> func = f => f.Id;
        var value=(string) entity.GetType().GetProperty(func.Body.ToString().Split(".")[1])?.GetValue(entity,null);
        var filter = Builders<T>.Filter.Eq(func, value);
        return Collection.ReplaceOneAsync(filter,entity);
    }

    public Task DeleteAsync(string id) => Collection.DeleteOneAsync(x => x.Id.Equals(id));
    
    private static string GetCollectionName()
    {
        return (typeof(T).GetCustomAttributes(typeof(BSonCollectionAttribute),true).FirstOrDefault() as BSonCollectionAttribute)?.CollectionName;
    }
}
