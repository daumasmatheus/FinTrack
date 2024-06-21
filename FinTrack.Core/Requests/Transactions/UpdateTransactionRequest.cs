using FinTrack.Core.Enums;
using FinTrack.Core.Requests.Base;
using System.ComponentModel.DataAnnotations;

namespace FinTrack.Core.Requests.Transactions
{
    public class UpdateTransactionRequest : BaseRequest
    {
        public long Id { get; set; }

        [Required(ErrorMessage = "Título Inválido")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Valor Inválido")]
        public decimal Amount { get; set; }

        [Required(ErrorMessage = "Tipo Inválido")]
        public ETransactionType TransactionType { get; set; } = ETransactionType.Withdraw;

        [Required(ErrorMessage = "Categoria Inválido")]
        public long CategoryId { get; set; }

        [Required(ErrorMessage = "Data Inválida")]
        public DateTime? PaidOrReceivedAt { get; set; }
    }
}
