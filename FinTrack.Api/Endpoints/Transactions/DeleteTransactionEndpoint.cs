using FinTrack.Api.Common.Api;
using FinTrack.Core.Handlers.Interfaces;
using FinTrack.Core.Models;
using FinTrack.Core.Requests.Transactions;
using FinTrack.Core.Responses.Base;

namespace FinTrack.Api.Endpoints.Transactions
{
    public class DeleteTransactionEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
        => app.MapDelete("/{id}", HandleAsync)
            .WithName("Transactions: Delete")
            .WithSummary("Remove uma Transação")
            .WithDescription("Remove uma Transação")
            .Produces<BaseResponse<Transaction?>>();

        private static async Task<IResult> HandleAsync(ITransactionHandler handler, long id)
        {
            DeleteTransactionRequest request = new()
            {
                Id = id,
                UserId = ApiConfiguration.UserId
            };
            var result = await handler.DeleteAsync(request);

            return result.IsSuccess ? TypedResults.Ok(result) : TypedResults.BadRequest();
        }
    }
}
