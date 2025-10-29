using HireUP.Domain.Entities;

namespace HireUP.Domain.Interfaces
{
    public interface IEmployerRepository
    {
        Task<IEnumerable<Employer>> GetAllEmployersAsync();
        Task<Employer> GetEmployerByIdAsync(int id);
        Task AddEmployerAsync(Employer employer);
        Task UpdateAsync(Employer employer);
        Task RemoveAsync(Employer employer);
    }
}
