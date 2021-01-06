// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4.Models;
using System.Collections.Generic;
using System.Security.Claims;

namespace PaymentGateway.IdentityServer
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> IdentityResources =>
            new IdentityResource[]
            { 
                new IdentityResources.OpenId()
            };

        public static IEnumerable<ApiScope> ApiScopes =>
            new List<ApiScope>
            {
                new ApiScope("paymentgateway.api", "PaymentGateway API", new List<string> { "paymentgateway.merchant" }),
            };

        public static IEnumerable<Client> Clients =>
           new List<Client>
            {
                new Client
                {
                    AllowOfflineAccess= true,
                    ClientId = "merchant1",
                    ClientName = "merchant1",

                    // no interactive user, use the clientid/secret for authentication
                    AllowedGrantTypes = GrantTypes.ClientCredentials,

                    // secret for authentication
                    ClientSecrets =
                    {
                        new Secret("supersecurepassword".Sha256())
                    },
                    
                    // scopes that client has access to
                    AllowedScopes = { "paymentgateway.api" },

                    Claims = {
                            new ClientClaim(ClaimTypes.Role, "paymentgateway.merchant"), 
                            new ClientClaim(ClaimTypes.Name, "merchant1"),
                            new ClientClaim(ClaimTypes.SerialNumber, "1") //This will be matched to our storage PK
                            },
                    ClientClaimsPrefix = null


                }
           };
    }
}