using FinTrack.Api.Common.Api;
using FinTrack.Core;
using FinTrack.Core.Handlers.Interfaces;
using FinTrack.Core.Models;
using FinTrack.Core.Requests.Categories;
using FinTrack.Core.Responses.Base;
using Microsoft.AspNetCore.Mvc;

namespace FinTrack.Api.Endpoints.Categories
{
    public class GetAllCategoriesEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/", HandleAsync)
              .WithName("Categories: GetAll")
              .WithSummary("Retorna todas categorias de um usuario")
              .WithDescription("Retorna todas categorias de um usuario")
              .Produces<PagedBaseReponse<List<Category>?>>();

        private static async Task<IResult> HandleAsync(ICategoryHandler handler, [FromQuery]int pageNumber = Configuration.DefaultPageNumber, 
                                                                                 [FromQuery]int pageSize = Configuration.DefaultPageSize)
        {
            GetAllCategoriesRequest request = new()
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                UserId = ApiConfiguration.UserId
            };

            var response = await handler.GetAllAsync(request);

            return response.IsSuccess ? TypedResults.Ok(response) : TypedResults.BadRequest();
        }
    }
}
