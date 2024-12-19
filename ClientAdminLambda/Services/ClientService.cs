using ClientAdminLambda.Cache;
using ClientAdminLambda.Models;
using ClientAdminLambda.Repositories;
using Newtonsoft.Json;

namespace ClientAdminLambda.Services;

public class ClientService
{
    private readonly ClientRepository _repository;
    private readonly RedisCacheService _cacheService;

    public ClientService(ClientRepository repository, RedisCacheService cacheService)
    {
        _repository = repository;
        _cacheService = cacheService;
    }

    public async Task<Client?> GetClientAsync(string id)
    {
        // Check Redis cache first
        var cachedClient = await _cacheService.GetAsync(id);
        if (!string.IsNullOrEmpty(cachedClient))
        {
            return JsonConvert.DeserializeObject<Client>(cachedClient);
        }

        // If not in cache, fetch from DynamoDB
        var client = await _repository.GetClientAsync(id);
        if (client != null)
        {
            // Store in cache for future requests
            await _cacheService.SetAsync(id, JsonConvert.SerializeObject(client), TimeSpan.FromMinutes(30));
        }
        return client;
    }
    public async Task SaveClientAsync(Client client) => await _repository.SaveClientAsync(client);
    public async Task UpdateClientAsync(Client client) => await _repository.UpdateClientAsync(client);
    public async Task DeleteClientAsync(string id) => await _repository.DeleteClientAsync(id);
}
