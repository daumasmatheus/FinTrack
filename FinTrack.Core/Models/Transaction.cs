using FinTrack.Core.Enums;

namespace FinTrack.Core.Models
{
    public class Transaction
    {
        public long Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? PaidOrReceivedAt { get; set; }
        public ETransactionType TransactionType { get; set; } = ETransactionType.Withdraw;
        public long CategoryId { get; set; }
        public string UserId { get; set; } = string.Empty;

        public Category Category { get; set; } = null!;
    }
}
