using System.Net;
using CalcApi.Helper;
using FastEndpoints;
using Microsoft.IdentityModel.Tokens;

namespace CalcApi._Endpoints_.Calc;

internal sealed class Endpoint : Endpoint<Request, Response>
{
    public override void Configure()
    {
        Post("/calculator");
        Options(o => o.WithName("CalculatorEndpoint"));
    }

    public override async Task HandleAsync(Request r, CancellationToken c)
    {
        try
        {
            var jwt = HttpContext.Request.Headers["Token"].ToString().Replace("Bearer ", "");

            if (jwt.IsNullOrEmpty())
            {
                await SendAsync(new Response(), 401, c);
                return;
            }

            if (!JwtHelper.ValidClaims(jwt, r.Operation))
            {
                await SendAsync(new Response(), 403, c);
                return;
            }

            var result = r.Operation.ToLower() switch
            {
                "add" => r.Number1 + r.Number2,
                "subtract" => r.Number1 - r.Number2,
                "multiply" => r.Number1 * r.Number2,
                "divide" => r.Number2 != 0
                    ? r.Number1 / r.Number2
                    : throw new ArgumentException("Cannot divide by zero"),
                _ => throw new ArgumentException("Invalid operation")
            };

            await SendAsync(new Response { Result = result }, cancellation: c);
        }
        catch (Exception e)
        {
            AddError(e.Message);
            await SendErrorsAsync((int)HttpStatusCode.BadRequest, c);
        }
    }
}