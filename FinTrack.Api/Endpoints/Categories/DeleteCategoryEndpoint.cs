using Azure;
using FinTrack.Api.Common.Api;
using FinTrack.Core.Handlers.Interfaces;
using FinTrack.Core.Models;
using FinTrack.Core.Requests.Categories;

namespace FinTrack.Api.Endpoints.Categories
{
    public class DeleteCategoryEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
        => app.MapDelete("/{id}", HandleAsync)
              .WithName("Categories: Delete")
              .WithSummary("Remove uma categoria")
              .WithDescription("Remove uma categoria")
              .Produces<Response<Category>?>();

        private static async Task<IResult> HandleAsync(ICategoryHandler handler, long id)
        {
            DeleteCategoryRequest request = new()
            {
                Id = id,
                UserId = ApiConfiguration.UserId
            };

            var response = await handler.DeleteAsync(request);

            return response.IsSuccess ? TypedResults.Ok(response) : TypedResults.BadRequest();
        }
    }
}
