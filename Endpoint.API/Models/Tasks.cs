using System.ComponentModel.DataAnnotations;

namespace Endpoint.API.Models
{
    public class Tasks
    {
        public int Id { get; set; }

        [Required(ErrorMessage= "Title is mandatory")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "The title must be between 3 and 50 characters")]
        public string? Titulo { get; set; }

        [StringLength(500, ErrorMessage = "The description must have a maximum of 300 characters")]
        public string? Descricao { get; set; }

        [Required(ErrorMessage = "Expiration date is mandatory")]
        [DataType(DataType.Date)]
        public DateTime DataVencimento { get; set; }

        [Required(ErrorMessage = "Task status is required.")]
        public bool Concluida { get; set; }

        [Required(ErrorMessage = "Priority is mandatory.")]
        [RegularExpression("^(High|Medium|Low)$", ErrorMessage = "The priority must be \"High\" \"Medium\" or \"Low\".")]
        public string? Prioridade { get; set; }
    }
}
