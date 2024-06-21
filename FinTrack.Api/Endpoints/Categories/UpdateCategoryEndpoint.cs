using Azure;
using FinTrack.Api.Common.Api;
using FinTrack.Core.Handlers.Interfaces;
using FinTrack.Core.Models;
using FinTrack.Core.Requests.Categories;

namespace FinTrack.Api.Endpoints.Categories
{
    public class UpdateCategoryEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
        => app.MapPut("/{id}", HandleAsync)
              .WithName("Categories: Update")
              .WithSummary("Atualiza os dados de uma categoria")
              .WithDescription("Atualiza os dados de uma categoria")
              .Produces<Response<Category>?>();

        private static async Task<IResult> HandleAsync(ICategoryHandler handler, UpdateCategoryRequest request, long id)
        {
            request.UserId = ApiConfiguration.UserId;
            request.Id = id;

            var response = await handler.UpdateAsync(request);

            return response.IsSuccess ? TypedResults.Ok(response) : TypedResults.BadRequest();
        }
    }
}
