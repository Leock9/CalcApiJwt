namespace CalcApi.Core.Application.UseCases.User.Get;

public record GetUseCaseOutput(string Username, IList<string> Roles);