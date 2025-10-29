using HireUP.Application.DTOs.Event;
using HireUP.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace HireUP.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventController : ControllerBase
    {
        private readonly EventApplicationService _eventService;

        public EventController(EventApplicationService eventService)
        {
            _eventService = eventService;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] EventRegisterDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _eventService.CreateEvent(dto);
                return CreatedAtAction(nameof(Create), new { message = "Evento criado com sucesso" });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Erro ao criar evento", details = ex.Message });
            }
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var events = await _eventService.GetAllEvents();
                return Ok(events);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Erro ao buscar eventos", details = ex.Message });
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var eventEntity = await _eventService.GetEventById(id);

                if (eventEntity == null)
                {
                    return NotFound(new { error = "Evento não encontrado" });
                }

                return Ok(eventEntity);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Erro ao buscar evento", details = ex.Message });
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(int id, [FromBody] EventUpdateDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _eventService.UpdateEvent(id, dto);
                return Ok(new { message = "Evento atualizado com sucesso" });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Erro ao atualizar evento", details = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _eventService.RemoveEvent(id);
                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Erro ao remover evento", details = ex.Message });
            }
        }
    }
}