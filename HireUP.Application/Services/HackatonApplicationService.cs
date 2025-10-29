using HireUP.Application.DTOs.Hackaton;
using HireUP.Domain.Entities;
using HireUP.Domain.Interfaces;

namespace HireUP.Application.Services
{
    public class HackatonApplicationService
    {
        private readonly IHackatonRepository _hackatonRepository;

        public HackatonApplicationService(IHackatonRepository hackatonRepository)
        {
            _hackatonRepository = hackatonRepository;
        }

        public async Task CreateHackaton(HackatonRegisterDto dto)
        {
            if (dto.EndDate <= dto.StartDate)
            {
                throw new InvalidOperationException("Data de término deve ser posterior à data de início");
            }

            var hackaton = new Hackaton(
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