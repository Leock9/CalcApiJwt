using CalcApi.Infra.Gateways.PasswordHasher;
using CalcApi.Infra.Gateways.Redis;

namespace CalcApi.Core.Application.UseCases.User.Login;

public interface ILoginUseCase : IUseCase<LoginUseCaseInput, LoginUseCaseOutput>
{
}
public class LoginUseCase(IUserGateway userGateway, IPasswordHasherGateway passwordHasherGateway)
    : ILoginUseCase
{
    public IUserGateway UserGateway { get; } = userGateway;
    public IPasswordHasherGateway PasswordHasherGateway { get; } = passwordHasherGateway;

    public async Task<LoginUseCaseOutput> HandleAsync(LoginUseCaseInput input, CancellationToken cancellationToken)
    {
        try
        {
            var user = await UserGateway.GetUserAsync(input.Username) ?? throw new Exception("User not found");
            return new LoginUseCaseOutput(PasswordHasherGateway.VerifyPassword(input.Password, user.Password), user.Roles);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}