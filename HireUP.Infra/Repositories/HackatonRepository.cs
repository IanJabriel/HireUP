    using HireUP.Domain.Entities;
using HireUP.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HireUP.Infra.Repositories
{
    public class HackatonRepository : IHackatonRepository
    {
        private readonly ApplicationDbContext _context;

        public HackatonRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Hackaton>> GetAllHackatonsAsync()
        {
            return await _context.Hackatons
                .Include(h => h.Attachments)
                .Include(h => h.EmployeesIds)
                .ToListAsync();
        }

        public async Task<Hackaton> GetHackatonByIdAsync(int id)
        {
            return await _context.Hackatons
                .Include(h => h.Attachments)
                .Include(h => h.EmployeesIds)
                .FirstOrDefaultAsync(h => h.Id == id);
        }

        public async Task AddHackatonAsync(Hackaton hackaton)
        {
            await _context.Hackatons.AddAsync(hackaton);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Hackaton hackaton)
        {
            _context.Hackatons.Update(hackaton);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveAsync(Hackaton hackaton)
        {
            _context.Hackatons.Remove(hackaton);
            await _context.SaveChangesAsync();
        }
    }
}