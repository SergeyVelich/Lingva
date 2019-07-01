using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace AuthServer
{
    public static class Config
    {
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource("resourceapi", "Lingva.WebAPI")
            };
        }

        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Email(),
                new IdentityResources.Profile(),
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                // Hybrid Flow = OpenId Connect + OAuth
                // To use both Identity and Access Tokens
                new Client
                {
                    ClientId = "mvc_client",
                    ClientName = "Lingva.MVC",                    

                    AllowedGrantTypes = GrantTypes.HybridAndClientCredentials,
                    ClientSecrets = { new Secret("secret".Sha256()) },

                    RedirectUris = { "http://localhost:6002/signin-oidc" },
                    PostLogoutRedirectUris = { "http://localhost:6002/signout-callback-oidc" },
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        "resourceapi"
                    },

                    AllowOfflineAccess = true,
                    RequireConsent = false,
                },
                new Client
                {                   
                    ClientId = "angular_client",
                    ClientName = "Angular SPA",
                    AllowedGrantTypes = GrantTypes.Code,
                    RequirePkce = true,
                    RequireClientSecret = false,
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        "resourceapi"
                    },
                    RedirectUris = {"http://localhost:4200/auth-callback"},
                    PostLogoutRedirectUris = {"http://localhost:4200/"},
                    AllowedCorsOrigins = {"http://localhost:4200"},
                    AllowAccessTokensViaBrowser = true,
                    AccessTokenLifetime = 300,
                    IdentityTokenLifetime = 300,
                    RequireConsent = false
                }
            };
        }
    }
}
