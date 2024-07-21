using aplicacao_servidora_dotnet.Handlers;
using aplicacao_servidora_dotnet.Security;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;

}).AddJwtBearer(options =>
{
    var idDiretorio = "64308046-f79c-4131-bcc3-0b93be5c8528";
    var idCliente = "30a0e041-5f40-41db-8c2b-14df535302c2";

    options.Authority = $"https://login.microsoftonline.com/{idDiretorio}/v2.0";

    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidIssuer = $"https://sts.windows.net/{idDiretorio}/",
        ValidateAudience = true,
        ValidAudiences = [idCliente],
        ValidateLifetime = true,
    };
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("ConsumidorValido", policy =>
    {
        policy.AddRequirements(new RolePolicyRequirement("ApiAdmin, UserAdmin"));
    });
});

builder.Services.AddSingleton<IAuthorizationHandler, RolePolicyHandler>();

builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
