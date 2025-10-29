using System.ComponentModel.DataAnnotations;

namespace HireUP.Application.DTOs.Solution
{
    public class SolutionUpdateDto
    {
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Título deve ter entre 3 e 50 caracteres")]
        public string? Title { get; set; }

        public string? AttachmentTitle { get; set; }

        [Url(ErrorMessage = "URL em formato inválido")]
        public string? AttachmentUrl { get; set; }
    }
}