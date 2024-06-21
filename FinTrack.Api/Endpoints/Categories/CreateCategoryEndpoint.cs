using Azure;
using FinTrack.Api.Common.Api;
using FinTrack.Core.Handlers.Interfaces;
using FinTrack.Core.Models;
using FinTrack.Core.Requests.Categories;

namespace FinTrack.Api.Endpoints.Categories
{
    public class CreateCategoryEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
            => app.MapPost("/", HandleAsync)
                  .WithName("Categories: Create")
                  .WithSummary("Cria uma nova categoria")
                  .WithDescription("Cria uma nova categoria")
                  .Produces<Response<Category>?>();

        private static async Task<IResult> HandleAsync(ICategoryHandler handler, CreateCategoryRequest request)
        {
            request.UserId = ApiConfiguration.UserId;
            var response = await handler.CreateAsync(request);

            return response.IsSuccess ? TypedResults.Created($"v1/categories/{response.Data?.Id}", response) : TypedResults.BadRequest();
        }
    }
}
