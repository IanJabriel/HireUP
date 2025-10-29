using HireUP.Application.DTOs.Event;
using HireUP.Domain.Entities;
using HireUP.Domain.Interfaces;

namespace HireUP.Application.Services
{
    public class EventApplicationService
    {
        private readonly IEventRepository _eventRepository;

        public EventApplicationService(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public async Task CreateEvent(EventRegisterDto dto)
        {
            if (dto.EndDate <= dto.StartDate)
            {
                throw new InvalidOperationException("Data de término deve ser posterior à data de início");
            }

            var eventEntity = new Event(
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