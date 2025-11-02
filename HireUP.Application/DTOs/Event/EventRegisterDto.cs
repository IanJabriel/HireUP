using System.ComponentModel.DataAnnotations;

namespace HireUP.Application.DTOs.Event
{
    public class EventRegisterDto
    {
        [Required(ErrorMessage = "ID da empresa é obrigatório")]
        public int EnterpriseId { get; set; }

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

        [StringLength(30, ErrorMessage = "Código de acesso não pode exceder 30 caracteres")]
        public string? CodeAcess { get; set; }
    }
}