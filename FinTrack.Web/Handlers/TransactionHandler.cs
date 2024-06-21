using FinTrack.Core.Common;
using FinTrack.Core.Handlers.Interfaces;
using FinTrack.Core.Models;
using FinTrack.Core.Requests.Transactions;
using FinTrack.Core.Responses.Base;
using System.Net.Http.Json;

namespace FinTrack.Web.Handlers
{
    public class TransactionHandler(IHttpClientFactory httpClientFactory) : ITransactionHandler
    {
        private readonly HttpClient client = httpClientFactory.CreateClient(WebConfiguration.HttpClientName);

        public async Task<BaseResponse<Transaction?>> CreateAsync(CreateTransactionRequest request)
        {
            var result = await client.PostAsJsonAsync("v1/transactions", request);
            return await result.Content.ReadFromJsonAsync<BaseResponse<Transaction?>>() ?? new BaseResponse<Transaction?>(null, 400, "Falha ao criar nova transação");
        }

        public async Task<BaseResponse<Transaction?>> DeleteAsync(DeleteTransactionRequest request)
        {
            var result = await client.DeleteAsync($"v1/transactions/{request.Id}");
            return await result.Content.ReadFromJsonAsync<BaseResponse<Transaction?>>() ?? new BaseResponse<Transaction?>(null, 400, "Falha ao remover transação");
        }        

        public async Task<BaseResponse<Transaction?>> UpdateAsync(UpdateTransactionRequest request)
        {
            var result = await client.PutAsJsonAsync($"v1/transactions/{request.Id}", request);
            return await result.Content.ReadFromJsonAsync<BaseResponse<Transaction?>>() ?? new BaseResponse<Transaction?>(null, 400, "Falha ao atualizar transação");
        }

        public async Task<BaseResponse<Transaction?>> GetById(GetTransactionByIdRequest request)
            => await client.GetFromJsonAsync<BaseResponse<Transaction?>>($"v1/transactions/{request.Id}") ??
                    new BaseResponse<Transaction?>(null, 400, "Falha ao obter a transação");

        public async Task<PagedBaseReponse<List<Transaction>?>> GetByPeriod(GetTransactionsByPeriodRequest request)
        {
            const string format = "yyyy-MM-dd";
            var startDate = request.StartDate is not null ? request.StartDate.Value.ToString(format) : DateTime.Now.GetLastDay().ToString(format);
            var endDate = request.EndDate is not null ? request.EndDate.Value.ToString(format) : DateTime.Now.GetLastDay().ToString(format);

            return await client.GetFromJsonAsync<PagedBaseReponse<List<Transaction>?>>($"v1/transactions?startDate={startDate}&endDate={endDate}") ??
                    new PagedBaseReponse<List<Transaction>?>(null, 400, "Falha ao obter as transações");
        }
    }
}
