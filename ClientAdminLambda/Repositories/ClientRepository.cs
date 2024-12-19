using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using ClientAdminLambda.Models;

namespace ClientAdminLambda.Repositories;

public class ClientRepository
{
    private readonly DynamoDBContext _context;

    public ClientRepository(IAmazonDynamoDB dynamoDbClient)
    {
        _context = new DynamoDBContext(dynamoDbClient);
    }

    public async Task<Client?> GetClientAsync(string id) => await _context.LoadAsync<Client>(id);

    public async Task SaveClientAsync(Client client) => await _context.SaveAsync(client);

    public async Task UpdateClientAsync(Client client) => await _context.SaveAsync(client);

    public async Task DeleteClientAsync(string id) => await _context.DeleteAsync<Client>(id);
}
