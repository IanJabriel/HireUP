using HireUP.Domain.Entities;

namespace HireUP.Domain.Interfaces
{
    public interface IEnterpriseRepository
    {
        Task<IEnumerable<Enterprise>> GetAllEnterprisesAsync();
        Task<Enterprise> GetEnterpriseByIdAsync(int id);
        Task AddEnterpriseAsync(Enterprise enterprise);
        Task UpdateAsync(Enterprise enterprise);
        Task RemoveAsync(Enterprise enterprise);
    }
}
