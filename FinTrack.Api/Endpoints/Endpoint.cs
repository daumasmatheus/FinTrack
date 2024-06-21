using FinTrack.Api.Common.Api;
using FinTrack.Api.Endpoints.Categories;
using FinTrack.Api.Endpoints.Transactions;

namespace FinTrack.Api.Endpoints
{
    public static class Endpoint
    {
        public static void MapEndpoints(this WebApplication app)
        {
            var endpoints = app.MapGroup("");

            endpoints
                    .MapGroup("/")
                    .WithTags("Health Check")
                    .MapGet("/", () => new { message = "Ok" });

            endpoints
                .MapGroup("v1/categories")
                .WithTags("Categories")
                .MapEndpoint<CreateCategoryEndpoint>()
                .MapEndpoint<DeleteCategoryEndpoint>()
                .MapEndpoint<GetAllCategoriesEndpoint>()
                .MapEndpoint<GetCategoryByIdEndpoint>()
                .MapEndpoint<UpdateCategoryEndpoint>();

            endpoints
                .MapGroup("v1/transactions")
                .WithTags("Transactions")
                .MapEndpoint<CreateTransactionEndpoint>()
                .MapEndpoint<DeleteTransactionEndpoint>()
                .MapEndpoint<GetTransactionByIdEndpoint>()
                .MapEndpoint<GetTransactionsByPeriodEndpoint>()
                .MapEndpoint<UpdateTransactionEndpoint>();
        }

        private static IEndpointRouteBuilder MapEndpoint<TEndpoint>(this IEndpointRouteBuilder app) where TEndpoint : IEndpoint 
        {
            TEndpoint.Map(app);
            return app;
        }
    }
}
