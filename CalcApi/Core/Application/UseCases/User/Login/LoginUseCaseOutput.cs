namespace CalcApi.Core.Application.UseCases.User.Login;

public record LoginUseCaseOutput(bool IsAuthenticated, IList<string>Roles);