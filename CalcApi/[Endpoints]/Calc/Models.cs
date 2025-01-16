using FastEndpoints;
using FluentValidation;

namespace CalcApi._Endpoints_.Calc;

internal sealed class Request
{
    [FromHeader]
    public string Token { get; init; } = default!;
    public double Number1 { get; init; }
    public double Number2 { get; init; }
    public string Operation { get; init; } = default!;
    
    internal sealed class Validator : Validator<Request>
    {
        public Validator()
        {
            RuleFor(x => x.Number1).NotEmpty();
            RuleFor(x => x.Number2).NotEmpty();
            RuleFor(x => x.Operation)
                .NotEmpty()
                .Must(BeAValidOperation).WithMessage("Invalid operation. Allowed values: add, subtract, multiply, divide.");
        }
        
        private static bool BeAValidOperation(string operation)
        {
            var validOperations = new[] { "add", "subtract", "multiply", "divide" };
            return validOperations.Contains(operation?.ToLower());
        }
    }
}

internal sealed class Response
{
    public double Result { get; init; }
}