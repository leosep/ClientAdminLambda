using Amazon.DynamoDBv2;
using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
using ClientAdminLambda.Cache;
using ClientAdminLambda.Models;
using ClientAdminLambda.Repositories;
using ClientAdminLambda.Services;
using Newtonsoft.Json;

[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace ClientAdminLambda;

public class Function
{
    private readonly ClientService _clientService;

    public Function()
    {
        var redisConnectionString = Environment.GetEnvironmentVariable("REDIS_CONNECTION_STRING");
        
        if (string.IsNullOrEmpty(redisConnectionString))
        {
            throw new ArgumentException("Redis connection string is not provided");
        }

        var dynamoDbClient = new AmazonDynamoDBClient();
        var repository = new ClientRepository(dynamoDbClient);
        var cacheService = new RedisCacheService(redisConnectionString);

        _clientService = new ClientService(repository, cacheService);
    }

    public async Task<APIGatewayProxyResponse> FunctionHandler(APIGatewayProxyRequest request, ILambdaContext context)
    {
        try
        {
            switch (request.HttpMethod)
            {
                case "GET":
                    var clientId = request.QueryStringParameters["id"];
                    var client = await _clientService.GetClientAsync(clientId);
                    return client is null
                        ? new APIGatewayProxyResponse { StatusCode = 404 }
                        : new APIGatewayProxyResponse { StatusCode = 200, Body = JsonConvert.SerializeObject(client) };

                case "POST":
                    var newClient = JsonConvert.DeserializeObject<Client>(request.Body);
                    await _clientService.SaveClientAsync(newClient);
                    return new APIGatewayProxyResponse { StatusCode = 201 };

                case "PUT":
                    var updateClient = JsonConvert.DeserializeObject<Client>(request.Body);
                    await _clientService.UpdateClientAsync(updateClient);
                    return new APIGatewayProxyResponse { StatusCode = 204 };

                case "DELETE":
                    var deleteClientId = request.QueryStringParameters["id"];
                    await _clientService.DeleteClientAsync(deleteClientId);
                    return new APIGatewayProxyResponse { StatusCode = 204 };

                default:
                    return new APIGatewayProxyResponse { StatusCode = 405 };
            }
        }
        catch (Exception ex)
        {
            context.Logger.LogError($"Error: {ex.Message}");
            return new APIGatewayProxyResponse { StatusCode = 500, Body = ex.Message };
        }
    }

}

