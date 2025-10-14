using Microsoft.EntityFrameworkCore;  
using HireUP.Domain.Entities;
using HireUP.Domain.Interfaces;

namespace HireUP.Infra.Repositories
{
    public class EmployerRepository : IEmployerRepository
    {
        private readonly ApplicationDbContext _context;

        public EmployerRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Employer>> GetAllEmployersAsync()
        {
            return await _context.Employers.ToListAsync();
        }

        public async Task<Employer> GetEmployerByIdAsync(int id)
        {
            return await _context.Employers.FindAsync(id);
        }

        public async Task AddEmployerAsync(Employer employer)
        {
            await _context.Employers.AddAsync(employer);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveAsync(Employer employer)
        {
            _context.Employers.Remove(employer);
            await _context.SaveChangesAsync();
        }
    }
}
