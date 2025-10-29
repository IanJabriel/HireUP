using System.ComponentModel.DataAnnotations;  

namespace HireUP.Application.DTOs.Enterprise
{
    public class EnterpriseRegisterDto
    {
        [Required(ErrorMessage = "Nome é obrigatório")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Nome deve ter entre 3 e 100 caracteres")]
        public required string Name { get; set; }

        [Required(ErrorMessage = "Telefone é obrigatório")]
        [Phone(ErrorMessage = "Telefone em formato inválido")]
        public required string Phone { get; set; }

        [Required(ErrorMessage = "Email é obrigatório")]
        [EmailAddress(ErrorMessage = "Email em formato inválido")]
        public required string Email { get; set; }

        [Required(ErrorMessage = "Senha é obrigatória")]
        public required string Password { get; set; }

        [Required(ErrorMessage = "Documento é obrigatório")]
        [RegularExpression(@"^\d{14}$", ErrorMessage = "Documento deve ser CNPJ (14 dígitos)")]
        public required string Document { get; set; }

        [Required(ErrorMessage = "Localização é obrigatória")]
        public required string GeoLocationId { get; set; }

        [StringLength(1000, ErrorMessage = "Descrição não pode exceder 1000 caracteres")]
        public string? Description { get; set; }
    }
}
