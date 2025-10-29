using System.ComponentModel.DataAnnotations;

namespace HireUP.Application.DTOs.Hackaton
{
    public class HackatonUpdateDto
    {
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Título deve ter entre 3 e 50 caracteres")]
        public string? Title { get; set; }

        [StringLength(2000, MinimumLength = 10, ErrorMessage = "Descrição deve ter entre 10 e 2000 caracteres")]
        public string? Description { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public bool? IsPrivate { get; set; }

        [StringLength(50, ErrorMessage = "Código de acesso não pode exceder 50 caracteres")]
        public string? CodeAcess { get; set; }
    }
}