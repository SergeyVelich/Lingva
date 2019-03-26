﻿using IdentityServer4;
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
                new ApiResource("fiver_auth_api", "Lingva.WebAPI")
            };
        }

        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
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
                    ClientId = "fiver_auth_client",
                    ClientName = "Lingva.MVC",
                    ClientSecrets = { new Secret("secret".Sha256()) },

                    AllowedGrantTypes = GrantTypes.HybridAndClientCredentials,
                    AllowOfflineAccess = true,
                    RequireConsent = false,

                    RedirectUris = { "http://localhost:6002/signin-oidc" },
                    PostLogoutRedirectUris = { "http://localhost:6002/signout-callback-oidc" },

                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "fiver_auth_api"
                    },
                },
            };
        }

        //public static List<TestUser> GetUsers()
        //{
        //    return new List<TestUser>
        //    {
        //        new TestUser
        //        {
        //            SubjectId = "1",
        //            Username = "james",
        //            Password = "password",
        //            Claims = new List<Claim>
        //            {
        //                new Claim("name", "James Bond"),
        //                new Claim("website", "https://james.com")
        //            }
        //        },
        //        new TestUser
        //        {
        //            SubjectId = "2",
        //            Username = "spectre",
        //            Password = "password",
        //            Claims = new List<Claim>
        //            {
        //                new Claim("name", "Spectre"),
        //                new Claim("website", "https://spectre.com")
        //            }
        //        }
        //    };
        //}
    }
}
