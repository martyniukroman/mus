using Duende.IdentityServer.Models;

namespace infrastructure.identity;

public class Clients
{
    public static IEnumerable<Client> Get()
    {
        return new List<Client>
        {
            new Client
            {
                ClientId = "musClient",
                ClientName = "musClient",
                AllowedGrantTypes = GrantTypes.ClientCredentials,
                ClientSecrets = new List<Secret> {new Secret("musClientSecret".Sha256())},
                AllowedScopes = new List<string> { "musApi.read" }
            }
        };
    }
}