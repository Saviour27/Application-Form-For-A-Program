using Microsoft.Azure.Cosmos;
using Program_Application_Form.Abstractions;

namespace Program_Application_Form.Repositories;
public class Repository<T> : IRepository<T> where T : class
{
    private readonly Container _container;

    public Repository(CosmosClient cosmosClient, string databaseName, string containerName)
    {
        _container = cosmosClient.GetContainer(databaseName, containerName);
    }

    public async Task AddAsync(T entity)
    {
        await _container.CreateItemAsync(entity, new PartitionKey((entity as dynamic).Id));
    }

    public async Task<T> GetByIdAsync(string id)
    {
        try
        {
            var response = await _container.ReadItemAsync<T>(id, new PartitionKey(id));
            return response.Resource;
        }
        catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            return null;
        }
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        var query = _container.GetItemQueryIterator<T>();
        var results = new List<T>();

        while (query.HasMoreResults)
        {
            var response = await query.ReadNextAsync();
            results.AddRange(response);
        }

        return results;
    }

    public async Task UpdateAsync(T entity)
    {
        await _container.UpsertItemAsync(entity, new PartitionKey((entity as dynamic).Id));
    }

    public async Task DeleteAsync(string id)
    {
        await _container.DeleteItemAsync<T>(id, new PartitionKey(id));
    }
}