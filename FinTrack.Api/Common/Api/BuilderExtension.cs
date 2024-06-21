using FinTrack.Api.Data;
using FinTrack.Api.Handlers;
using FinTrack.Core;
using FinTrack.Core.Handlers.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FinTrack.Api.Common.Api
{
    public static class BuilderExtension
    {
        public static void AddConfiguration(this WebApplicationBuilder builder)
        {
            ApiConfiguration.ConnectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? string.Empty;
            Configuration.BackEndUrl = builder.Configuration.GetValue<string>("BackEndUrl") ?? string.Empty;
            Configuration.FrontEndUrl = builder.Configuration.GetValue<string>("FrontEndUrl") ?? string.Empty;
        }

        public static void AddDocumentation(this WebApplicationBuilder builder)
        {
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(x =>
            {
                x.CustomSchemaIds(n => n.FullName);
            });
        }

        public static void AddCrossOrigin(this WebApplicationBuilder builder)
        {
            builder.Services.AddCors(opts =>
                opts.AddPolicy(ApiConfiguration.CorsPolicyName,
                               policy => policy.WithOrigins([Configuration.BackEndUrl, Configuration.FrontEndUrl])
                                               .AllowAnyMethod()
                                               .AllowAnyHeader()
                                               .AllowCredentials()));
        }

        public static void AddServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddTransient<ITransactionHandler, TransactionHandler>();
            builder.Services.AddTransient<ICategoryHandler, CategoryHandler>();
        }

        public static void AddDataContexts(this WebApplicationBuilder builder)
            => builder.Services.AddDbContext<AppDbContext>(x => x.UseSqlServer(ApiConfiguration.ConnectionString));
    }
}
