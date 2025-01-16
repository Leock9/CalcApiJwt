namespace CalcApi.Core.Application.UseCases.User.Create;

public record CreateUseCaseInput(string Username, IList<string> Roles);