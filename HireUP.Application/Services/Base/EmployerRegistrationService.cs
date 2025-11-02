using HireUP.Domain.Entities.Base;
using HireUP.Domain.Interfaces;

namespace HireUP.Application.Services.Base
{
    public abstract class EmployerRegistrationService<TEntity> where TEntity : EmployerRegistrableEntity
    {
        protected readonly IEmployerRepository _employerRepository;

        protected EmployerRegistrationService(IEmployerRepository employerRepository)
        {
            _employerRepository = employerRepository;
        }

        public async Task RegisterEmployer(int entityId, int employerId, Func<int, Task<TEntity>> getEntityFunc, Func<TEntity, Task> updateEntityFunc)
        {
            var entity = await getEntityFunc(entityId);
            
            if (entity == null)
            {
                throw new InvalidOperationException($"{typeof(TEntity).Name} não encontrado");
            }

            var employer = await _employerRepository.GetEmployerByIdAsync(employerId);
            
            if (employer == null)
            {
                throw new InvalidOperationException("Empregador não encontrado");
            }

            entity.RegisterEmployer(employer);
            await updateEntityFunc(entity);
        }

        public async Task UnregisterEmployer(int entityId, int employerId, Func<int, Task<TEntity>> getEntityFunc, Func<TEntity, Task> updateEntityFunc)
        {
            var entity = await getEntityFunc(entityId);
            
            if (entity == null)
            {
                throw new InvalidOperationException($"{typeof(TEntity).Name} não encontrado");
            }

            entity.UnregisterEmployer(employerId);
            await updateEntityFunc(entity);
        }
    }
}
