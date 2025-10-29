using System.ComponentModel.DataAnnotations;

namespace HireUP.Application.DTOs.Hackaton
{
    public class HackatonRegisterDto
    {
        [Required(ErrorMessage = "Título é obrigatório")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Título deve ter entre 3 e 50 caracteres")]
        public required string Title { get; set; }

        [Required(ErrorMessage = "Descrição é obrigatória")]
        [StringLength(2000, MinimumLength = 10, ErrorMessage = "Descrição deve ter entre 10 e 2000 caracteres")]
        public required string Description { get; set; }

        [Required(ErrorMessage = "Data de início é obrigatória")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "Data de término é obrigatória")]
        public DateTime EndDate { get; set; }

        [Required(ErrorMessage = "Indicação de privacidade é obrigatória")]
        public bool IsPrivate { get; set; }

        [StringLength(50, ErrorMessage = "Código de acesso não pode exceder 50 caracteres")]
        public string? CodeAcess { get; set; }
    }
}