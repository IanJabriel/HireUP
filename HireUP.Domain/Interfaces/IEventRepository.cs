using HireUP.Domain.Entities;

namespace HireUP.Domain.Interfaces
{
    public interface IEventRepository
    {
        Task<IEnumerable<Event>> GetAllEventsAsync();
        Task<Event> GetEventByIdAsync(int id);
        Task AddEventAsync(Event eventEntity);
        Task UpdateAsync(Event eventEntity);
        Task RemoveAsync(Event eventEntity);
        Task<List<Event>> GetEventByEmployerId(int employerId);
        Task<List<Event>> GetEventByEnterpriseId(int enterpriseId);
    }
}