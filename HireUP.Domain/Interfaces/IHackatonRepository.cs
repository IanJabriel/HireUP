using HireUP.Domain.Entities;

namespace HireUP.Domain.Interfaces
{
    public interface IHackatonRepository
    {
        Task<IEnumerable<Hackaton>> GetAllHackatonsAsync();
        Task<Hackaton> GetHackatonByIdAsync(int id);
        Task AddHackatonAsync(Hackaton hackaton);
        Task UpdateAsync(Hackaton hackaton);
        Task RemoveAsync(Hackaton hackaton);
    }
}