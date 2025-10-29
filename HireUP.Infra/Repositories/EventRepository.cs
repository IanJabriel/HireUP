using HireUP.Domain.Entities;
using HireUP.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HireUP.Infra.Repositories
{
    public class EventRepository : IEventRepository
    {
        private readonly ApplicationDbContext _context;

        public EventRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Event>> GetAllEventsAsync()
        {
            return await _context.Events
                .Include(e => e.Attachments)
                .Include(e => e.EmployeesIds)
                .ToListAsync();
        }

        public async Task<Event> GetEventByIdAsync(int id)
        {
            return await _context.Events
                .Include(e => e.Attachments)
                .Include(e => e.EmployeesIds)
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task AddEventAsync(Event eventEntity)
        {
            await _context.Events.AddAsync(eventEntity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Event eventEntity)
        {
            _context.Events.Update(eventEntity);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveAsync(Event eventEntity)
        {
            _context.Events.Remove(eventEntity);
            await _context.SaveChangesAsync();
        }
    }
}
