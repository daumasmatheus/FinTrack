using FinTrack.Api.Common.Api;
using FinTrack.Core.Handlers.Interfaces;
using FinTrack.Core.Models;
using FinTrack.Core.Requests.Transactions;
using FinTrack.Core.Responses.Base;

namespace FinTrack.Api.Endpoints.Transactions
{
    public class CreateTransactionEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
        => app.MapPost("/", HandleAsync)
            .WithName("Transactions: Create")
            .WithSummary("Cria uma nova Transação")
            .WithDescription("Cria uma nova Transação")
            .Produces<BaseResponse<Transaction?>>();

        private static async Task<IResult> HandleAsync(ITransactionHandler handler, CreateTransactionRequest request)
        {
            request.UserId = ApiConfiguration.UserId;
            var result = await handler.CreateAsync(request);

            return result.IsSuccess ? TypedResults.Created($"/{result.Data?.Id}", result) : TypedResults.BadRequest(result.Data);
        }
    }
}
