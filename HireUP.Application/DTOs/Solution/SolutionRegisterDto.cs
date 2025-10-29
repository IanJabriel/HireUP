using System.ComponentModel.DataAnnotations;

namespace HireUP.Application.DTOs.Solution
{
    public class SolutionRegisterDto
    {
        [Required(ErrorMessage = "ID do problema é obrigatório")]
        public int ProblemId { get; set; }

        [Required(ErrorMessage = "ID do empregador é obrigatório")]
        public int EmployeeId { get; set; }

        [Required(ErrorMessage = "Título é obrigatório")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Título deve ter entre 3 e 50 caracteres")]
        public required string Title { get; set; }

        [Required(ErrorMessage = "Título do anexo é obrigatório")]
        public required string AttachmentTitle { get; set; }

        [Required(ErrorMessage = "URL do anexo é obrigatória")]
        [Url(ErrorMessage = "URL em formato inválido")]
        public required string AttachmentUrl { get; set; }
    }
}