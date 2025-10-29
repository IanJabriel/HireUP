using HireUP.Application.DTOs.Hackaton;
using HireUP.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace HireUP.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HackatonController : ControllerBase
    {
        private readonly HackatonApplicationService _hackatonService;

        public HackatonController(HackatonApplicationService hackatonService)
        {
            _hackatonService = hackatonService;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] HackatonRegisterDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _hackatonService.CreateHackaton(dto);
                return CreatedAtAction(nameof(Create), new { message = "Hackaton criado com sucesso" });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Erro ao criar hackaton", details = ex.Message });
            }
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var hackatons = await _hackatonService.GetAllHackatons();
                return Ok(hackatons);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Erro ao buscar hackatons", details = ex.Message });
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var hackaton = await _hackatonService.GetHackatonById(id);

                if (hackaton == null)
                {
                    return NotFound(new { error = "Hackaton não encontrado" });
                }

                return Ok(hackaton);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Erro ao buscar hackaton", details = ex.Message });
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(int id, [FromBody] HackatonUpdateDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _hackatonService.UpdateHackaton(id, dto);
                return Ok(new { message = "Hackaton atualizado com sucesso" });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Erro ao atualizar hackaton", details = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _hackatonService.RemoveHackaton(id);
                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Erro ao remover hackaton", details = ex.Message });
            }
        }
    }
}