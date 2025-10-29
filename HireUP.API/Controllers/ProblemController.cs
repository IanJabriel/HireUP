using HireUP.Application.DTOs.Problem;
using HireUP.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace HireUP.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProblemController : ControllerBase
    {
        private readonly ProblemApplicationService _problemService;

        public ProblemController(ProblemApplicationService problemService)
        {
            _problemService = problemService;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] ProblemRegisterDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _problemService.CreateProblem(dto);
                return CreatedAtAction(nameof(Create), new { message = "Problema criado com sucesso" });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Erro ao criar problema", details = ex.Message });
            }
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var problems = await _problemService.GetAllProblems();
                return Ok(problems);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Erro ao buscar problemas", details = ex.Message });
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var problem = await _problemService.GetProblemById(id);

                if (problem == null)
                {
                    return NotFound(new { error = "Problema não encontrado" });
                }

                return Ok(problem);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Erro ao buscar problema", details = ex.Message });
            }
        }

        [HttpGet("enterprise/{enterpriseId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetByEnterpriseId(int enterpriseId)
        {
            try
            {
                var problems = await _problemService.GetProblemsByEnterpriseId(enterpriseId);
                return Ok(problems);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Erro ao buscar problemas da empresa", details = ex.Message });
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(int id, [FromBody] ProblemUpdateDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _problemService.UpdateProblem(id, dto);
                return Ok(new { message = "Problema atualizado com sucesso" });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Erro ao atualizar problema", details = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _problemService.RemoveProblem(id);
                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Erro ao remover problema", details = ex.Message });
            }
        }
    }
}