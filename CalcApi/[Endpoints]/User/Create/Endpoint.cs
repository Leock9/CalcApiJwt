using System.Net;
using CalcApi.Core.Application.UseCases.User.Create;
using FastEndpoints;

namespace CalcApi._Endpoints_.User.Create;

internal sealed class Endpoint : Endpoint<Request, Response>
{
    public ICreateUseCase CreateUseCase { get; set; } = null!;

    public override void Configure()
    {
        Post("/user");
        Options(o => o.WithName("CreateUserEndpoint"));
    }

    public override async Task HandleAsync(Request r, CancellationToken c)
    {
        try
        {
            var output = await CreateUseCase.HandleAsync(new CreateUseCaseInput(r.Username, r.Roles), c);
            await SendAsync(new Response { UserPassword = $"Senha exibida apenas uma vez: {output.UserPassword}" },
                cancellation: c);
        }
        catch (Exception e)
        {
            AddError(e.Message);
            await SendErrorsAsync((int)HttpStatusCode.BadRequest, c);
        }
    }
}