using HireUP.Domain.Entities;
using HireUP.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HireUP.Infra.Repositories
{
    public class EnterpriseRepository : IEnterpriseRepository
    {
        private readonly ApplicationDbContext _context;
        public EnterpriseRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Enterprise>> GetAllEnterprisesAsync()
        {
            return await _context.Enterprises.ToListAsync();
        }
        public async Task<Enterprise> GetEnterpriseByIdAsync(int id)
        {
            return await _context.Enterprises.FindAsync(id);
        }
        public async Task AddEnterpriseAsync(Enterprise enterprise)
        {
            await _context.Enterprises.AddAsync(enterprise);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(Enterprise enterprise)
        {
            _context.Enterprises.Update(enterprise);
            await _context.SaveChangesAsync();
        }
        public async Task RemoveAsync(Enterprise enterprise)
        {
            _context.Enterprises.Remove(enterprise);
            await _context.SaveChangesAsync();
        }
    }
}
