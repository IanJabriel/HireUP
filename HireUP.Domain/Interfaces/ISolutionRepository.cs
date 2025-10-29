using HireUP.Domain.Entities;

namespace HireUP.Domain.Interfaces
{
    public interface ISolutionRepository
    {
        Task<IEnumerable<Solution>> GetAllSolutionsAsync();
        Task<Solution> GetSolutionByIdAsync(int id);
        Task<IEnumerable<Solution>> GetSolutionsByProblemIdAsync(int problemId);
        Task<IEnumerable<Solution>> GetSolutionsByEmployerIdAsync(int employerId);
        Task AddSolutionAsync(Solution solution);
        Task UpdateAsync(Solution solution);
        Task RemoveAsync(Solution solution);
    }
}