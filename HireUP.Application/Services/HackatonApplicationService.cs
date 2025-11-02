using HireUP.Application.DTOs.Hackaton;
using HireUP.Application.Services.Base;
using HireUP.Domain.Entities;
using HireUP.Domain.Interfaces;

namespace HireUP.Application.Services
{
    public class HackatonApplicationService : EmployerRegistrationService<Hackaton>
    {
        private readonly IHackatonRepository _hackatonRepository;
        private readonly IEnterpriseRepository _enterpriseRepository;

        public HackatonApplicationService(
            IHackatonRepository hackatonRepository, 
            IEmployerRepository employerRepository,
            IEnterpriseRepository enterpriseRepository)
            : base(employerRepository)
        {
            _hackatonRepository = hackatonRepository;
            _enterpriseRepository = enterpriseRepository;
        }

        public async Task CreateHackaton(HackatonRegisterDto dto)
        {
            // Validar se a empresa existe
            var enterprise = await _enterpriseRepository.GetEnterpriseByIdAsync(dto.EnterpriseId);
            if (enterprise == null)
            {
                throw new InvalidOperationException("Empresa não encontrada");
            }

            if (dto.EndDate <= dto.StartDate)
            {
                throw new InvalidOperationException("Data de término deve ser posterior à data de início");
            }

            var hackaton = new Hackaton(
                enterpriseId: dto.EnterpriseId,
                title: dto.Title,
                description: dto.Description,
                startDate: dto.StartDate,
                endDate: dto.EndDate,
                isPrivate: dto.IsPrivate,
                codeAcess: dto.CodeAcess
            );

            await _hackatonRepository.AddHackatonAsync(hackaton);
        }

        public async Task UpdateHackaton(int id, HackatonUpdateDto dto)
        {
            var hackaton = await _hackatonRepository.GetHackatonByIdAsync(id);
            
            if (hackaton == null)
            {
                throw new InvalidOperationException("Hackaton não encontrado");
            }

            hackaton.Update(
                title: dto.Title,
                description: dto.Description,
                startDate: dto.StartDate,
                endDate: dto.EndDate,
                isPrivate: dto.IsPrivate,
                codeAcess: dto.CodeAcess
            );

            await _hackatonRepository.UpdateAsync(hackaton);
        }

        public async Task RegisterEmployerInHackaton(int hackatonId, int employerId)
        {
            await RegisterEmployer(
                hackatonId, 
                employerId, 
                _hackatonRepository.GetHackatonByIdAsync, 
                _hackatonRepository.UpdateAsync
            );
        }

        public async Task UnregisterEmployerFromHackaton(int hackatonId, int employerId)
        {
            await UnregisterEmployer(
                hackatonId, 
                employerId, 
                _hackatonRepository.GetHackatonByIdAsync, 
                _hackatonRepository.UpdateAsync
            );
        }

        public async Task<IEnumerable<Hackaton>> GetAllHackatons()
        {
            return await _hackatonRepository.GetAllHackatonsAsync();
        }

        public async Task<Hackaton> GetHackatonById(int id)
        {
            return await _hackatonRepository.GetHackatonByIdAsync(id);
        }

        public async Task RemoveHackaton(int id)
        {
            var hackaton = await _hackatonRepository.GetHackatonByIdAsync(id);
            if (hackaton == null)
            {
                throw new InvalidOperationException("Hackaton não encontrado");
            }

            await _hackatonRepository.RemoveAsync(hackaton);
        }
    }
}