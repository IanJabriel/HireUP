using System.ComponentModel.DataAnnotations;

namespace HireUP.Application.DTOs
{
    public class RegisterEmployerDto
    {
        [Required(ErrorMessage = "ID do empregador é obrigatório")]
        public int EmployerId { get; set; }
    }
}
