using CalcApi.Core.Application.UseCases.User.Create;
using CalcApi.Core.Application.UseCases.User.Get;
using CalcApi.Core.Application.UseCases.User.Login;
using CalcApi.Infra.Gateways.PasswordHasher;
using CalcApi.Infra.Gateways.Redis;
using FastEndpoints;
using FastEndpoints.Security;
using FastEndpoints.Swagger;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddAuthenticationJwtBearer(s => s.SigningKey = "YourStrongSecretKeyHereWith256BitsLength!");
builder.Services.AddAuthorization();
builder.Services.AddFastEndpoints();

builder.Services.SwaggerDocument(o =>
{
    o.EnableJWTBearerAuth = false;
    o.DocumentSettings = s =>
    {
        s.Title = "Calc API";
        s.Version = "v1";
        s.Description = "API simples de calculadora protegida com JWT.";
    };
});

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        policy =>
        {
            policy.AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});

builder.Services.AddSingleton<IConnectionMultiplexer>(sp =>
{
    var configuration = builder.Configuration.GetConnectionString("RedisConnection") ?? "localhost:6379";
    return ConnectionMultiplexer.Connect(configuration);
});

builder.Services.AddScoped<IPasswordHasherGateway, PasswordHasherGateway>();
builder.Services.AddScoped<IUserGateway, UserGateway>();
builder.Services.AddScoped<ICreateUseCase, CreateUseCase>();
builder.Services.AddScoped<IGetUseCase, GetUseCase>();
builder.Services.AddScoped<ILoginUseCase, LoginUseCase>();


var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();
app.UseFastEndpoints(c =>
{
    c.Endpoints.Configurator = ep =>
    {
        ep.AllowAnonymous();
        ep.Summary(s =>
        {
            s.Responses[200] = "Sucesso.";
            s.Responses[400] = "Erro de validação.";
            s.Responses[401] = "Não autenticado.";
            s.Responses[403] = "Acesso negado.";
            s.Responses[404] = "Não encontrado.";
            s.Responses[500] = "Erro interno.";
        });
    };
});

app.UseSwaggerGen();
app.Run();