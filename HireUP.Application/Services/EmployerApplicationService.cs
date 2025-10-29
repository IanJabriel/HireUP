using HireUP.Application.DTOs.Employer;
using HireUP.Domain.Entities;
using HireUP.Domain.Interfaces;

namespace HireUP.Application.Services
{
    public class EmployerApplicationService
    {
        private readonly IEmployerRepository _employerRepository;

        public EmployerApplicationService(IEmployerRepository employerRepository)
        {
            _employerRepository = employerRepository;
        }

        public async Task CreateEmployer(EmployerRegisterDto dto)
        {
            var existingEmployers = await _employerRepository.GetAllEmployersAsync();
            if (existingEmployers.Any(e => e.Email == dto.Email))
            {
                throw new InvalidOperationException("Email já cadastrado");
            }

            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(dto.Password);

            var employer = new Employer(
                name: dto.Name,
                phone: dto.Phone,
                email: dto.Email,
                password: hashedPassword,
                document: dto.Document
            );

            await _employerRepository.AddEmployerAsync(employer);
        }

        public async Task UpdateEmployer(int id, EmployerUpdateDto dto)
        {
            var employer = await _employerRepository.GetEmployerByIdAsync(id);
            
            if (employer == null)
            {
                throw new InvalidOperationException("Empregador não encontrado");
            }

            if (!string.IsNullOrWhiteSpace(dto.Email))
            {
                var existingEmployers = await _employerRepository.GetAllEmployersAsync();
                if (existingEmployers.Any(e => e.Email.Equals(dto.Email, StringComparison.OrdinalIgnoreCase) && e.Id != id))
                {
                    throw new InvalidOperationException("Email já cadastrado por outro empregador");
                }
            }

            var hashedPassword = !string.IsNullOrWhiteSpace(dto.Password) 
                ? BCrypt.Net.BCrypt.HashPassword(dto.Password) 
                : null;

            employer.Update(
                name: dto.Name,
                phone: dto.Phone,
                email: dto.Email,
                password: hashedPassword,
                geoLocationId: dto.GeoLocationId,
                description: dto.Description,
                role: dto.Role
            );

            await _employerRepository.UpdateAsync(employer);
        }

        public async Task<Employer> LoginEmployer(EmployerLoginDto dto)
        {
            var employers = await _employerRepository.GetAllEmployersAsync();
            var employer = employers.FirstOrDefault(e => e.Email.Equals(dto.Email, StringComparison.OrdinalIgnoreCase));

            if (employer == null)
            {
                throw new UnauthorizedAccessException("Email ou senha inválidos");
            }

            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(dto.Password, employer.Password);

            if (!isPasswordValid)
            {
                throw new UnauthorizedAccessException("Email ou senha inválidos");
            }

            return employer;
        }

        public async Task<IEnumerable<Employer>> GetAllEmployers()
        {
            return await _employerRepository.GetAllEmployersAsync();
        }

        public async Task<Employer> GetEmployerById(int id)
        {
            return await _employerRepository.GetEmployerByIdAsync(id);
        }

        public async Task RemoveEmployer(int id)
        {
            var employer = await _employerRepository.GetEmployerByIdAsync(id);
            if (employer == null)
            {
                throw new InvalidOperationException("Empregador não encontrado");
            }

            await _employerRepository.RemoveAsync(employer);
        }
    }
}
