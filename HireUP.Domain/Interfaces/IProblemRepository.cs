using HireUP.Domain.Entities;

namespace HireUP.Domain.Interfaces
{
    public interface IProblemRepository
    {
        Task<IEnumerable<Problem>> GetAllProblemsAsync();
        Task<Problem> GetProblemByIdAsync(int id);
        Task<IEnumerable<Problem>> GetProblemsByEnterpriseIdAsync(int enterpriseId);
        Task AddProblemAsync(Problem problem);
        Task UpdateAsync(Problem problem);
        Task RemoveAsync(Problem problem);
    }
}