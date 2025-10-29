using HireUP.Application.DTOs.Solution;
using HireUP.Domain.Entities;
using HireUP.Domain.Interfaces;

namespace HireUP.Application.Services
{
    public class SolutionApplicationService
    {
        private readonly ISolutionRepository _solutionRepository;
        private readonly IProblemRepository _problemRepository;
        private readonly IEmployerRepository _employerRepository;

        public SolutionApplicationService(
            ISolutionRepository solutionRepository,
            IProblemRepository problemRepository,
            IEmployerRepository employerRepository)
        {
            _solutionRepository = solutionRepository;
            _problemRepository = problemRepository;
            _employerRepository = employerRepository;
        }

        public async Task CreateSolution(SolutionRegisterDto dto)
        {
            var problem = await _problemRepository.GetProblemByIdAsync(dto.ProblemId);
            if (problem == null)
            {
                throw new InvalidOperationException("Problema não encontrado");
            }

            var employer = await _employerRepository.GetEmployerByIdAsync(dto.EmployeeId);
            if (employer == null)
            {
                throw new InvalidOperationException("Empregador não encontrado");
            }

            var attachment = Attachment.Create(dto.AttachmentTitle, dto.AttachmentUrl);

            var solution = new Solution(
                problemId: dto.ProblemId,
                employeeId: dto.EmployeeId,
                title: dto.Title,
                attachment: attachment
            );

            await _solutionRepository.AddSolutionAsync(solution);
        }

        public async Task UpdateSolution(int id, SolutionUpdateDto dto)
        {
            var solution = await _solutionRepository.GetSolutionByIdAsync(id);
            
            if (solution == null)
            {
                throw new InvalidOperationException("Solução não encontrada");
            }

            Attachment? newAttachment = null;
            if (!string.IsNullOrWhiteSpace(dto.AttachmentTitle) && !string.IsNullOrWhiteSpace(dto.AttachmentUrl))
            {
                newAttachment = Attachment.Create(dto.AttachmentTitle, dto.AttachmentUrl);
            }

            solution.Update(
                title: dto.Title,
                attachment: newAttachment
            );

            await _solutionRepository.UpdateAsync(solution);
        }

        public async Task<IEnumerable<Solution>> GetAllSolutions()
        {
            return await _solutionRepository.GetAllSolutionsAsync();
        }

        public async Task<Solution> GetSolutionById(int id)
        {
            return await _solutionRepository.GetSolutionByIdAsync(id);
        }

        public async Task<IEnumerable<Solution>> GetSolutionsByProblemId(int problemId)
        {
            return await _solutionRepository.GetSolutionsByProblemIdAsync(problemId);
        }

        public async Task<IEnumerable<Solution>> GetSolutionsByEmployerId(int employerId)
        {
            return await _solutionRepository.GetSolutionsByEmployerIdAsync(employerId);
        }

        public async Task RemoveSolution(int id)
        {
            var solution = await _solutionRepository.GetSolutionByIdAsync(id);
            if (solution == null)
            {
                throw new InvalidOperationException("Solução não encontrada");
            }

            await _solutionRepository.RemoveAsync(solution);
        }
    }
}