using FastEndpoints;
using FluentValidation;

namespace CalcApi._Endpoints_.User.Get;

internal sealed class Request
{
    public string Username { get; init; }
    internal sealed class Validator : Validator<Request>
    {
        public Validator()
        {
            RuleFor(x => x.Username).NotEmpty();
        }
    }
}

internal sealed class Response
{
    public string Username { get; init; }
    public IList<string> Roles { get; init; }
}