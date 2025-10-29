using System.ComponentModel.DataAnnotations;

namespace HireUP.Application.DTOs.Enterprise
{
    public class EnterpriseLoginDto
    {
        [Required(ErrorMessage = "O email é obrigatório")]
        [EmailAddress(ErrorMessage = "Email em formato inválido")]
        public string Email { get; set; }
        [Required(ErrorMessage = "A senha é obrigatória")]
        public string Password { get; set; }
    }
}
