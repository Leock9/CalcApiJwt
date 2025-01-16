using CalcApi.Core.Application.UseCases.User.Get;
using CalcApi.Infra.Gateways.PasswordHasher;
using CalcApi.Infra.Gateways.Redis;

namespace CalcApi.Core.Application.UseCases.User.Create;

public interface ICreateUseCase : IUseCase<CreateUseCaseInput, CreateUseCaseOutput>
{
}

public class CreateUseCase(
    IUserGateway userGateway,
    IPasswordHasherGateway passwordHasherGateway,
    IGetUseCase getUseCase) : ICreateUseCase
{
    private IUserGateway UserGateway { get; } = userGateway;
    private IPasswordHasherGateway PasswordHasherGateway { get; } = passwordHasherGateway;

    public async Task<CreateUseCaseOutput> HandleAsync(CreateUseCaseInput input, CancellationToken cancellationToken)
    {
        try
        {
            if(await UserGateway.GetUserAsync(input.Username) is not null) 
                throw new Exception("User already exists");
            
            var password = Guid.NewGuid().ToString();
            var passwordHash = PasswordHasherGateway.HashPassword(password);

            var user = new Entity.User(input.Username, passwordHash, input.Roles);

            await userGateway.AddUserAsync(user);

            return new CreateUseCaseOutput
            {
                UserPassword = password
            };
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}