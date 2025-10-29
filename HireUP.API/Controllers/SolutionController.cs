using HireUP.Application.DTOs.Solution;
using HireUP.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace HireUP.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SolutionController : ControllerBase
    {
        private readonly SolutionApplicationService _solutionService;

        public SolutionController(SolutionApplicationService solutionService)
        {
            _solutionService = solutionService;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] SolutionRegisterDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _solutionService.CreateSolution(dto);
                return CreatedAtAction(nameof(Create), new { message = "Solução criada com sucesso" });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Erro ao criar solução", details = ex.Message });
            }
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var solutions = await _solutionService.GetAllSolutions();
                return Ok(solutions);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Erro ao buscar soluções", details = ex.Message });
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var solution = await _solutionService.GetSolutionById(id);

                if (solution == null)
                {
                    return NotFound(new { error = "Solução não encontrada" });
                }

                return Ok(solution);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Erro ao buscar solução", details = ex.Message });
            }
        }

        [HttpGet("problem/{problemId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetByProblemId(int problemId)
        {
            try
            {
                var solutions = await _solutionService.GetSolutionsByProblemId(problemId);
                return Ok(solutions);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Erro ao buscar soluções do problema", details = ex.Message });
            }
        }

        [HttpGet("employer/{employerId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetByEmployerId(int employerId)
        {
            try
            {
                var solutions = await _solutionService.GetSolutionsByEmployerId(employerId);
                return Ok(solutions);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Erro ao buscar soluções do empregador", details = ex.Message });
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(int id, [FromBody] SolutionUpdateDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _solutionService.UpdateSolution(id, dto);
                return Ok(new { message = "Solução atualizada com sucesso" });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Erro ao atualizar solução", details = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _solutionService.RemoveSolution(id);
                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Erro ao remover solução", details = ex.Message });
            }
        }
    }
}