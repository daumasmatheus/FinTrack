using FinTrack.Api.Common.Api;
using FinTrack.Core.Handlers.Interfaces;
using FinTrack.Core.Models;
using FinTrack.Core.Requests.Transactions;
using FinTrack.Core.Responses.Base;

namespace FinTrack.Api.Endpoints.Transactions
{
    public class GetTransactionByIdEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/{id}", HandleAsync)
            .WithName("Transactions: GetById")
            .WithSummary("Obtem uma Transação pelo Id")
            .WithDescription("Remove uma Transação pelo Id")
            .Produces<BaseResponse<Transaction?>>();

        private static async Task<IResult> HandleAsync(ITransactionHandler handler, long id)
        {
            GetTransactionByIdRequest request = new()
            {
                Id = id,
                UserId = ApiConfiguration.UserId
            };
            var result = await handler.GetById(request);

            return result.IsSuccess ? TypedResults.Ok(result) : TypedResults.BadRequest();
        }
    }
}
