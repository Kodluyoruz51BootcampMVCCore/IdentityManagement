using IdentityManagement.Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace IdentityManagement.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        //Fluent API Design -> Method Chaining
        public static IServiceCollection AddIdentityServerConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddIdentity<AppUser, AppRole>(options =>
            {
                options.User.RequireUniqueEmail = true;
                options.Password.RequiredLength = 0;
                options.Password.RequiredUniqueChars = 0;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireDigit = false;
                options.Password.RequireNonAlphanumeric = false;
                options.User.AllowedUserNameCharacters = "abcçdefghiıjklmnoöpqrsştuüvwxyzABCÇDEFGHIİJKLMNOÖPQRSŞTUÜVWXYZ0123456789-._@+'#!/^%{}*";
            }).AddEntityFrameworkStores<AppIdentityDbContext>().AddDefaultTokenProviders();

            services.AddIdentityServer()
                .AddDeveloperSigningCredential()
                .AddOperationalStore(options =>
                {
                    options.ConfigureDbContext = builder => builder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
                    options.EnableTokenCleanup = true;
                })
                .AddConfigurationStore(options => options.ConfigureDbContext = builder => builder.UseSqlServer(configuration.GetConnectionString("DefaultConnection")))
                .AddAspNetIdentity<AppUser>();

            return services;
        }

        /// <summary>
        /// Servislerin dependency injection eklentilerini yapan method.
        /// </summary>
        /// <typeparam name="T">Identity sınıfı</typeparam>
        /// <param name="services">Ana service nesnesi (startup.cs)</param>
        /// <returns>Service nesnesinin modifiye hali</returns>
        public static IServiceCollection AddServices<T>(this IServiceCollection services) where T : IdentityUser<int>, new()
        {
            //services.AddTransient<IProfileService, IdentityClaimsProfileService>();
            return services;
        }

        public static IServiceCollection AddDatabaseConfiguration(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<AppIdentityDbContext>(options => options.UseSqlServer(connectionString));
            services.AddDbContext<AppPersistedGrantDbContext>(options => options.UseSqlServer(connectionString));
            services.AddDbContext<AppConfigurationDbContext>(options => options.UseSqlServer(connectionString));

            return services;
        }
    }
}
