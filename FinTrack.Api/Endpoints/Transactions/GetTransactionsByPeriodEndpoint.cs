using FinTrack.Api.Common.Api;
using FinTrack.Core;
using FinTrack.Core.Handlers.Interfaces;
using FinTrack.Core.Models;
using FinTrack.Core.Requests.Transactions;
using FinTrack.Core.Responses.Base;
using Microsoft.AspNetCore.Mvc;

namespace FinTrack.Api.Endpoints.Transactions
{
    public class GetTransactionsByPeriodEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/", HandleAsync)
            .WithName("Transactions: GetByPeriod")
            .WithSummary("Obtém Transaçoes de um determinado periodo")
            .WithDescription("Obtém Transaçoes de um determinado periodo")
            .Produces<PagedBaseReponse<List<Transaction?>>>();

        private static async Task<IResult> HandleAsync(ITransactionHandler handler, [FromQuery] DateTime? startDate = null, 
                                                                                    [FromQuery] DateTime? endDate = null,
                                                                                    [FromQuery] int pageNumber = Configuration.DefaultPageNumber,
                                                                                    [FromQuery] int pageSize = Configuration.DefaultPageSize)
        {
            GetTransactionsByPeriodRequest request = new()
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                StartDate = startDate,
                EndDate = endDate,
                UserId = ApiConfiguration.UserId
            };

            var response = await handler.GetByPeriod(request);

            return response.IsSuccess ? TypedResults.Ok(response) : TypedResults.BadRequest();
        }
    }
}
