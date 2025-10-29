using HireUP.Domain.Entities;
using HireUP.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HireUP.Infra.Repositories
{
    public class SolutionRepository : ISolutionRepository
    {
        private readonly ApplicationDbContext _context;

        public SolutionRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Solution>> GetAllSolutionsAsync()
        {
            return await _context.Solutions
                .Include(s => s.Attachment)
                .ToListAsync();
        }

        public async Task<Solution> GetSolutionByIdAsync(int id)
        {
            return await _context.Solutions
                .Include(s => s.Attachment)
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<IEnumerable<Solution>> GetSolutionsByProblemIdAsync(int problemId)
        {
            return await _context.Solutions
                .Include(s => s.Attachment)
                .Where(s => s.ProblemId == problemId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Solution>> GetSolutionsByEmployerIdAsync(int employerId)
        {
            return await _context.Solutions
                .Include(s => s.Attachment)
                .Where(s => s.EmployeeId == employerId)
                .ToListAsync();
        }

        public async Task AddSolutionAsync(Solution solution)
        {
            await _context.Solutions.AddAsync(solution);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Solution solution)
        {
            _context.Solutions.Update(solution);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveAsync(Solution solution)
        {
            _context.Solutions.Remove(solution);
            await _context.SaveChangesAsync();
        }
    }
}