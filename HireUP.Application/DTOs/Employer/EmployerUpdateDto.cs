using System.ComponentModel.DataAnnotations;

namespace HireUP.Application.DTOs.Employer
{
    public class EmployerUpdateDto
    {
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Nome deve ter entre 3 e 100 caracteres")]
        public string? Name { get; set; }

        [Phone(ErrorMessage = "Telefone em formato inválido")]
        public string? Phone { get; set; }

        [EmailAddress(ErrorMessage = "Email em formato inválido")]
        public string? Email { get; set; }

        public string? Password { get; set; }

        public string? GeoLocationId { get; set; }

        [StringLength(1000, ErrorMessage = "Descrição não pode exceder 1000 caracteres")]
        public string? Description { get; set; }

        public string? Role { get; set; }
    }
}