using System.Net;
using CalcApi.Core.Application.UseCases.User.Login;
using FastEndpoints;
using FastEndpoints.Security;

namespace CalcApi._Endpoints_.Auth;

internal sealed class Endpoint : Endpoint<Request>
{
    public ILoginUseCase LoginUseCase { get; set; }

    public override void Configure()
    {
        Post("/auth/login");
        AllowAnonymous();
        Options(o => o.WithName("LoginEndpoint"));
    }

    public override async Task HandleAsync(Request r, CancellationToken c)
    {
        try
        {
            var loginValid = await LoginUseCase.HandleAsync(new LoginUseCaseInput(r.Username, r.Password), c);
            if (!loginValid.IsAuthenticated)
            {
                await SendAsync(new Response(), 401, c);
                return;
            }

            // Nunca colocar HARDCODE apenas para fins do desafio
            var jwtToken = JwtBearer.CreateToken(
                o =>
                {
                    o.SigningKey = "YourStrongSecretKeyHereWith256BitsLength";
                    o.ExpireAt = DateTime.UtcNow.AddDays(1);
                    o.User.Claims.Add(("UserName", r.Username));
                    o.Issuer = "CalcApi";
                    o.Audience = "CalcApi";

                    foreach (var role in loginValid.Roles)
                    {
                        o.User.Claims.Add(("Role", role));
                    }
                });
            await SendAsync(
                new
                {
                    r.Username,
                    Token = $"Bearer {jwtToken}"
                }, cancellation: c);
        }
        catch (Exception e)
        {
            AddError(e.Message);
            await SendErrorsAsync((int)HttpStatusCode.BadRequest, c);
        }
    }
}