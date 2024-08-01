using aplicacao_servidora_dotnet;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwagger(builder.Configuration);

builder.Services.SetupAuthentication(builder.Configuration);

builder.Services.SetupAuthorization();

var app = builder.Build();

app.UseSwagger();

app.UseSwaggerUI(options =>
{
    var swaggerId = builder.Configuration.GetValue<string>("EntraID:Swagger:Id");

    options.OAuthClientId(swaggerId);
    options.OAuthUsePkce();
    options.OAuthScopeSeparator(" ");
});

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
