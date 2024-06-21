using FinTrack.Core.Requests.Base;
using System.ComponentModel.DataAnnotations;

namespace FinTrack.Core.Requests.Categories
{
    public class UpdateCategoryRequest : BaseRequest
    {
        public long Id { get; set; }

        [Required(ErrorMessage = "Titulo inválido")]
        [MaxLength(80, ErrorMessage = "Deve conter até 80 caracteres")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Descrição inválida")]
        public string? Description { get; set; }
    }
}
