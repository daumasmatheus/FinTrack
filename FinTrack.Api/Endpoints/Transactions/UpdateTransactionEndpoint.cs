using FinTrack.Api.Common.Api;
using FinTrack.Core.Handlers.Interfaces;
using FinTrack.Core.Models;
using FinTrack.Core.Requests.Transactions;
using FinTrack.Core.Responses.Base;

namespace FinTrack.Api.Endpoints.Transactions
{
    public class UpdateTransactionEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
        => app.MapPut("/{id}", HandleAsync)
            .WithName("Transactions: Update")
            .WithSummary("Atualiza os dados de uma Transação")
            .WithDescription("Atualiza os dados de uma Transação")
            .Produces<BaseResponse<Transaction?>>();

        private static async Task<IResult> HandleAsync(ITransactionHandler handler, UpdateTransactionRequest request, long id)
        {
            request.UserId = ApiConfiguration.UserId;
            request.Id = id;

            var result = await handler.UpdateAsync(request);

            return result.IsSuccess ? TypedResults.Ok(result) : TypedResults.BadRequest();
        }
    }
}
