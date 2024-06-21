using FinTrack.Core.Requests.Base;

namespace FinTrack.Core.Requests.Transactions
{
    public class DeleteTransactionRequest : BaseRequest
    {
        public long Id { get; set; }
    }
}
