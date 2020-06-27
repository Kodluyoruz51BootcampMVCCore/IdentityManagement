using IdentityServer4.EntityFramework.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace IdentityManagement.Infrastructure.Persistence
{
    public class SeedData
    {
        public static void EnsureSeedData(IServiceProvider provider)
        {
            var configuration = provider.GetRequiredService<IConfiguration>();
            provider.GetRequiredService<AppIdentityDbContext>().Database.Migrate();
            provider.GetRequiredService<AppPersistedGrantDbContext>().Database.Migrate();
            provider.GetRequiredService<AppConfigurationDbContext>().Database.Migrate();

            var context = provider.GetRequiredService<AppConfigurationDbContext>();
            if (!context.Clients.Any())
            {
                var clients = new List<Client>();
                configuration.GetSection("IdentityServer:Clients").Bind(clients);
                context.Clients.AddRange(clients); //TODO: toentity?
                context.SaveChanges();
            }

            if (!context.ApiResources.Any())
            {
                var apiResources = new List<ApiResource>();
                configuration.GetSection("IdentityServer:ApiResources").Bind(apiResources);
                context.ApiResources.AddRange(apiResources);
                context.SaveChanges();
            }

            if (!context.IdentityResources.Any())
            {
                var identityResources = new List<IdentityResource>();
                configuration.GetSection("IdentityServer:IdentityResources").Bind(identityResources);
                context.IdentityResources.AddRange(identityResources);
                context.SaveChanges();
            }
        }
    }
}
