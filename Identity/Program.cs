using IdentityServer4.Models;

var builder = WebApplication.CreateBuilder(args);

var clients = new List<Client>()
{
    new Client
    {
        ClientId = "client",
        AllowedGrantTypes = GrantTypes.ClientCredentials,
        ClientSecrets = { new Secret("secret".Sha256()) },
        AllowedScopes = { "api1" }
    },

    new Client
    {
        ClientId = "client2",
        AllowedGrantTypes = GrantTypes.ClientCredentials,
        ClientSecrets = { new Secret("secret2".Sha256()) },
        AllowedScopes = { "api2" }
    }
};

var apiScopes = new List<ApiScope>
{
    new ApiScope("api1", "My API"),
    new ApiScope("api2", "My API2"),
};

builder.Services.AddIdentityServer()
    .AddInMemoryClients(clients)
    .AddInMemoryApiScopes(apiScopes)
    .AddDeveloperSigningCredential();

builder.Services.AddControllers();

var app = builder.Build();

//app.UseHttpsRedirection();
app.UseIdentityServer();

app.UseAuthorization();

app.MapControllers();

app.Run();
