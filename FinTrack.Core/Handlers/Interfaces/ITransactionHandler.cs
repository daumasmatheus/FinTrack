using FinTrack.Core.Models;
using FinTrack.Core.Requests.Transactions;
using FinTrack.Core.Responses.Base;

namespace FinTrack.Core.Handlers.Interfaces
{
    public interface ITransactionHandler
    {
        Task<BaseResponse<Transaction?>> CreateAsync(CreateTransactionRequest request);
        Task<BaseResponse<Transaction?>> UpdateAsync(UpdateTransactionRequest request);
        Task<BaseResponse<Transaction?>> DeleteAsync(DeleteTransactionRequest request);
        Task<PagedBaseReponse<List<Transaction>?>> GetByPeriod(GetTransactionsByPeriodRequest request);
        Task<BaseResponse<Transaction?>> GetById(GetTransactionByIdRequest request);
    }
}
