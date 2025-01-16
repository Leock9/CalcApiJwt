using System.Net;
using CalcApi.Core.Application.UseCases.User.Get;
using FastEndpoints;

namespace CalcApi._Endpoints_.User.Get;

internal sealed class Endpoint : Endpoint<Request, Response>
{
    public IGetUseCase GetUseCase { get; set; } = null!;
    
    public override void Configure()
    {
        Get("/user");
        Options(o => o.WithName("GetUserEndpoint"));
    }

    public override async Task HandleAsync(Request r, CancellationToken c)
    {
        try
        {
            var output = await GetUseCase.HandleAsync(new GetUseCaseInput(r.Username), c);
            await SendAsync(new Response { Username = output.Username, Roles = output.Roles},
                cancellation: c);
        }
        catch (Exception e)
        {
            AddError(e.Message);
            await SendErrorsAsync((int)HttpStatusCode.BadRequest, c);
        }
    }
}