using HireUP.Application.DTOs.Enterprise;
using HireUP.Domain.Entities;
using HireUP.Domain.Interfaces;

namespace HireUP.Application.Services
{
    public class EnterpriseApplicationService
    {
        private readonly IEnterpriseRepository _enterpriseRepository;
        private readonly IEventRepository _eventRepository;
        private readonly IHackatonRepository _hackatonRepository;
        private readonly IProblemRepository _problemRepository;

        public EnterpriseApplicationService(IEnterpriseRepository enterpriseRepository, IEventRepository eventRepository, IHackatonRepository hackatonRepository, IProblemRepository problemRepository)
        {
            _enterpriseRepository = enterpriseRepository;
            _eventRepository = eventRepository;
            _hackatonRepository = hackatonRepository;
            _problemRepository = problemRepository;
        }

        public async Task CreateEnterprise(EnterpriseRegisterDto dto)
        {
            var existingEnterprises = await _enterpriseRepository.GetAllEnterprisesAsync();
            
            if (existingEnterprises.Any(e => e.Email.Equals(dto.Email, StringComparison.OrdinalIgnoreCase)))
            {
                throw new InvalidOperationException("Email já cadastrado");
            }

            if (existingEnterprises.Any(e => e.Document == dto.Document))
            {
                throw new InvalidOperationException("CNPJ já cadastrado");
            }

            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(dto.Password);

            var enterprise = new Enterprise(
                name: dto.Name,
                phone: dto.Phone,
                email: dto.Email,
                password: hashedPassword,
                document: dto.Document,
                geoLocationId: dto.GeoLocationId,
                description: dto.Description
            );

            await _enterpriseRepository.AddEnterpriseAsync(enterprise);
        }

        public async Task UpdateEnterprise(int id, EnterpriseUpdateDto dto)
        {
            var enterprise = await _enterpriseRepository.GetEnterpriseByIdAsync(id);
            
            if (enterprise == null)
            {
                throw new InvalidOperationException("Empresa não encontrada");
            }

            if (!string.IsNullOrWhiteSpace(dto.Email))
            {
                var existingEnterprises = await _enterpriseRepository.GetAllEnterprisesAsync();
                if (existingEnterprises.Any(e => e.Email.Equals(dto.Email, StringComparison.OrdinalIgnoreCase) && e.Id != id))
                {
                    throw new InvalidOperationException("Email já cadastrado por outra empresa");
                }
            }

            var hashedPassword = !string.IsNullOrWhiteSpace(dto.Password) 
                ? BCrypt.Net.BCrypt.HashPassword(dto.Password) 
                : null;

            enterprise.Update(
                name: dto.Name,
                phone: dto.Phone,
                email: dto.Email,
                password: hashedPassword,
                geoLocationId: dto.GeoLocationId,
                description: dto.Description,
                field: dto.Field
            );

            await _enterpriseRepository.UpdateAsync(enterprise);
        }

        public async Task<Enterprise> LoginEnterprise(EnterpriseLoginDto dto)
        {
            var enterprises = await _enterpriseRepository.GetAllEnterprisesAsync();
            var enterprise = enterprises.FirstOrDefault(e => e.Email.Equals(dto.Email, StringComparison.OrdinalIgnoreCase));

            if (enterprise == null)
            {
                throw new UnauthorizedAccessException("Email ou senha inválidos");
            }

            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(dto.Password, enterprise.Password);

            if (!isPasswordValid)
            {
                throw new UnauthorizedAccessException("Email ou senha inválidos");
            }

            return enterprise;
        }

        public async Task<IEnumerable<Enterprise>> GetAllEnterprises()
        {
            return await _enterpriseRepository.GetAllEnterprisesAsync();
        }

        public async Task<Enterprise> GetEnterpriseById(int id)
        {
            return await _enterpriseRepository.GetEnterpriseByIdAsync(id);
        }

        public async Task RemoveEnterprise(int id)
        {
            var enterprise = await _enterpriseRepository.GetEnterpriseByIdAsync(id);
            if (enterprise == null)
            {
                throw new InvalidOperationException("Empresa não encontrada");
            }

            await _enterpriseRepository.RemoveAsync(enterprise);
        }

        public async Task<ResponseGetOnlyOwnPost> GetOnlyOwnPost(int id) 
        {
            var enterprise = await _enterpriseRepository.GetEnterpriseByIdAsync(id);

            if (enterprise == null)
            {
                throw new InvalidOperationException("Empresa não encontrada");
            }

            var events = await _eventRepository.GetEventByEnterpriseId(id);
            var hackaton = await _hackatonRepository.GetHackatonByEnterpriseId(id);
            var problem = await _problemRepository.GetProblemsByEnterpriseIdAsync(id);

            return new ResponseGetOnlyOwnPost
            {
                Event = events,
                Hackaton = hackaton,
                Problem = problem
            };
        }

        public class ResponseGetOnlyOwnPost
        {
            public List<Event> Event { get; set; } = [];
            public List<Hackaton> Hackaton { get; set; } = [];
            public IEnumerable<Problem> Problem { get; set; } = [];
        }
    }
}
