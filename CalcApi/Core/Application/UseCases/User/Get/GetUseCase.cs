using CalcApi.Infra.Gateways.Redis;

namespace CalcApi.Core.Application.UseCases.User.Get;

public interface IGetUseCase : IUseCase<GetUseCaseInput, GetUseCaseOutput>
{
}

public class GetUseCase(IUserGateway userGateway) : IGetUseCase
{
    public IUserGateway UserGateway { get; } = userGateway;

    public async Task<GetUseCaseOutput> HandleAsync(GetUseCaseInput input, CancellationToken cancellationToken)
    {
        try
        {
            var user = await userGateway.GetUserAsync(input.Username) ??
                       throw new Exception("User not found");

            return new GetUseCaseOutput(user.Username, user.Roles);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}