using Azure;
using FinTrack.Api.Common.Api;
using FinTrack.Core.Handlers.Interfaces;
using FinTrack.Core.Models;
using FinTrack.Core.Requests.Categories;

namespace FinTrack.Api.Endpoints.Categories
{
    public class GetCategoryByIdEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/{id}", HandleAsync)
              .WithName("Categories: GetById")
              .WithSummary("Retorna determinada categoria de um usuario pelo ID")
              .WithDescription("Retorna determinada categoria de um usuario pelo ID")
              .Produces<Response<Category>?>();

        private static async Task<IResult> HandleAsync(ICategoryHandler handler, long id)
        {
            GetCategoryByIdRequest request = new()
            {
                UserId = ApiConfiguration.UserId,
                Id = id
            };

            var response = await handler.GetById(request);

            return response.IsSuccess ? TypedResults.Ok(response) : TypedResults.BadRequest();
        }
    }
}
