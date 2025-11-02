using HireUP.Application.DTOs.Event;
using HireUP.Application.Services.Base;
using HireUP.Domain.Entities;
using HireUP.Domain.Interfaces;

namespace HireUP.Application.Services
{
    public class EventApplicationService : EmployerRegistrationService<Event>
    {
        private readonly IEventRepository _eventRepository;
        private readonly IEnterpriseRepository _enterpriseRepository;

        public EventApplicationService(
            IEventRepository eventRepository, 
            IEmployerRepository employerRepository,
            IEnterpriseRepository enterpriseRepository) 
            : base(employerRepository)
        {
            _eventRepository = eventRepository;
            _enterpriseRepository = enterpriseRepository;
        }

        public async Task CreateEvent(EventRegisterDto dto)
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

            var eventEntity = new Event(
                enterpriseId: dto.EnterpriseId,
                title: dto.Title,
                description: dto.Description,
                startDate: dto.StartDate,
                endDate: dto.EndDate,
                isPrivate: dto.IsPrivate,
                codeAcess: dto.CodeAcess
            );

            await _eventRepository.AddEventAsync(eventEntity);
        }

        public async Task UpdateEvent(int id, EventUpdateDto dto)
        {
            var eventEntity = await _eventRepository.GetEventByIdAsync(id);
            
            if (eventEntity == null)
            {
                throw new InvalidOperationException("Evento não encontrado");
            }

            eventEntity.Update(
                title: dto.Title,
                description: dto.Description,
                startDate: dto.StartDate,
                endDate: dto.EndDate,
                isPrivate: dto.IsPrivate,
                codeAcess: dto.CodeAcess
            );

            await _eventRepository.UpdateAsync(eventEntity);
        }

        public async Task RegisterEmployerInEvent(int eventId, int employerId)
        {
            await RegisterEmployer(
                eventId, 
                employerId, 
                _eventRepository.GetEventByIdAsync, 
                _eventRepository.UpdateAsync
            );
        }

        public async Task UnregisterEmployerFromEvent(int eventId, int employerId)
        {
            await UnregisterEmployer(
                eventId, 
                employerId, 
                _eventRepository.GetEventByIdAsync, 
                _eventRepository.UpdateAsync
            );
        }

        public async Task<IEnumerable<Event>> GetAllEvents()
        {
            return await _eventRepository.GetAllEventsAsync();
        }

        public async Task<Event> GetEventById(int id)
        {
            return await _eventRepository.GetEventByIdAsync(id);
        }

        public async Task RemoveEvent(int id)
        {
            var eventEntity = await _eventRepository.GetEventByIdAsync(id);
            if (eventEntity == null)
            {
                throw new InvalidOperationException("Evento não encontrado");
            }

            await _eventRepository.RemoveAsync(eventEntity);
        }
    }
}