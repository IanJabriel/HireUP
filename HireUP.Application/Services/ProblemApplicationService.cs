using HireUP.Application.DTOs.Problem;
using HireUP.Domain.Entities;
using HireUP.Domain.Interfaces;

namespace HireUP.Application.Services
{
    public class ProblemApplicationService
    {
        private readonly IProblemRepository _problemRepository;
        private readonly IEnterpriseRepository _enterpriseRepository;

        public ProblemApplicationService(
            IProblemRepository problemRepository,
            IEnterpriseRepository enterpriseRepository)
        {
            _problemRepository = problemRepository;
            _enterpriseRepository = enterpriseRepository;
        }

        public async Task CreateProblem(ProblemRegisterDto dto)
        {
            var enterprise = await _enterpriseRepository.GetEnterpriseByIdAsync(dto.EnterpriseId);
            if (enterprise == null)
            {
                throw new InvalidOperationException("Empresa não encontrada");
            }

            var problem = new Problem(
                enterpriseId: dto.EnterpriseId,
                title: dto.Title,
                description: dto.Description
            );

            await _problemRepository.AddProblemAsync(problem);
        }

        public async Task UpdateProblem(int id, ProblemUpdateDto dto)
        {
            var problem = await _problemRepository.GetProblemByIdAsync(id);
            
            if (problem == null)
            {
                throw new InvalidOperationException("Problema não encontrado");
            }

            problem.Update(
                title: dto.Title,
                description: dto.Description
            );

            await _problemRepository.UpdateAsync(problem);
        }

        public async Task<IEnumerable<Problem>> GetAllProblems()
        {
            return await _problemRepository.GetAllProblemsAsync();
        }

        public async Task<Problem> GetProblemById(int id)
        {
            return await _problemRepository.GetProblemByIdAsync(id);
        }

        public async Task<IEnumerable<Problem>> GetProblemsByEnterpriseId(int enterpriseId)
        {
            return await _problemRepository.GetProblemsByEnterpriseIdAsync(enterpriseId);
        }

        public async Task RemoveProblem(int id)
        {
            var problem = await _problemRepository.GetProblemByIdAsync(id);
            if (problem == null)
            {
                throw new InvalidOperationException("Problema não encontrado");
            }

            await _problemRepository.RemoveAsync(problem);
        }
    }
}