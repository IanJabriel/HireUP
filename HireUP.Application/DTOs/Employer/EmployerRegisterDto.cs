using System.ComponentModel.DataAnnotations;

namespace HireUP.Application.DTOs.Employer
{
    public class EmployerRegisterDto
    {
        [Required(ErrorMessage = "Nome é obrigatório")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Nome deve ter entre 3 e 100 caracteres")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Telefone é obrigatório")]
        [Phone(ErrorMessage = "Telefone em formato inválido")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Email é obrigatório")]
        [EmailAddress(ErrorMessage = "Email em formato inválido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Senha é obrigatória")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Documento é obrigatório")]
        [RegularExpression(@"^\d{11}$|^\d{14}$", ErrorMessage = "Documento deve ser CPF (11 dígitos) ou CNPJ (14 dígitos)")]
        public string Document { get; set; }
    }
}
