using FastEndpoints;
using FluentValidation;

namespace CalcApi._Endpoints_.Auth;

internal sealed class Request
{
    public string Username { get; init; } = default!;
    public string Password { get; init; } = default!;
    
    internal sealed class Validator : Validator<Request>
    {
        public Validator()
        {
            RuleFor(x => x.Username).NotEmpty();
            RuleFor(x => x.Password).NotEmpty();
        }
    }
}

internal sealed class Response
{
    public string Token { get; init; } = default!;
}