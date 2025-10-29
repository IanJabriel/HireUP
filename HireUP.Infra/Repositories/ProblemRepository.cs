using HireUP.Domain.Entities;
using HireUP.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HireUP.Infra.Repositories
{
    public class ProblemRepository : IProblemRepository
    {
        private readonly ApplicationDbContext _context;

        public ProblemRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Problem>> GetAllProblemsAsync()
        {
            return await _context.Problems.ToListAsync();
        }

        public async Task<Problem> GetProblemByIdAsync(int id)
        {
            return await _context.Problems.FindAsync(id);
        }

        public async Task<IEnumerable<Problem>> GetProblemsByEnterpriseIdAsync(int enterpriseId)
        {
            return await _context.Problems
                .Where(p => p.EnterpriseId == enterpriseId)
                .ToListAsync();
        }

        public async Task AddProblemAsync(Problem problem)
        {
            await _context.Problems.AddAsync(problem);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Problem problem)
        {
            _context.Problems.Update(problem);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveAsync(Problem problem)
        {
            _context.Problems.Remove(problem);
            await _context.SaveChangesAsync();
        }
    }
}