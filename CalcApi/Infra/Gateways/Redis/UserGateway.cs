using System.Text.Json;
using CalcApi.Core.Entity;
using StackExchange.Redis;

namespace CalcApi.Infra.Gateways.Redis;

public interface IUserGateway
{
    Task AddUserAsync(User user);
    Task<User?> GetUserAsync(string username);
}

public class UserGateway(IConnectionMultiplexer redis) : IUserGateway
{
    public async Task AddUserAsync(User user)
    {
        var db = redis.GetDatabase();
        var serializedUser = JsonSerializer.Serialize(user);
        await db.StringSetAsync(user.Username, serializedUser);
    }

    public async Task<User?> GetUserAsync(string username)
    {
        var db = redis.GetDatabase();
        var serializedUser = await db.StringGetAsync(username);

        return string.IsNullOrEmpty(serializedUser) ? null : 
                JsonSerializer.Deserialize<User>(serializedUser);
    }
}