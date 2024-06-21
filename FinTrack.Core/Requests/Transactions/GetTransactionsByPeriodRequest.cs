using FinTrack.Core.Requests.Base;

namespace FinTrack.Core.Requests.Transactions
{
    public class GetTransactionsByPeriodRequest : PagedBaseRequest
    {
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
