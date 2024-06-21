using FinTrack.Core.Requests.Base;

namespace FinTrack.Core.Requests.Transactions
{
    public class GetTransactionByIdRequest : BaseRequest
    {
        public long Id { get; set; }
    }
}
