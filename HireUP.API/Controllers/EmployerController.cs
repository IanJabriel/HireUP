using HireUP.Application.DTOs.Employer;
using HireUP.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace HireUP.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployerController : ControllerBase
    {
        private readonly EmployerApplicationService _employerService;

        public EmployerController(EmployerApplicationService employerService)
        {
            _employerService = employerService;
        }

        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Register([FromBody] EmployerRegisterDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _employerService.CreateEmployer(dto);
                return CreatedAtAction(nameof(Register), new { message = "Empregador registrado com sucesso" });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Erro ao registrar empregador", details = ex.Message });
            }
        }

        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Login([FromBody] EmployerLoginDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var employer = await _employerService.LoginEmployer(dto);

                var response = new
                {
                    id = employer.Id,
                    name = employer.Name,
                    email = employer.Email,
                    phone = employer.Phone,
                    document = employer.Document,
                    role = employer.Role,
                    description = employer.Description,
                    createdAt = employer.CreatedAt,
                    message = "Login realizado com sucesso"
                };

                return Ok(response);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Erro ao realizar login", details = ex.Message });
            }
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var employers = await _employerService.GetAllEmployers();
                return Ok(employers);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Erro ao buscar empregadores", details = ex.Message });
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var employer = await _employerService.GetEmployerById(id);

                if (employer == null)
                {
                    return NotFound(new { error = "Empregador não encontrado" });
                }

                return Ok(employer);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Erro ao buscar empregador", details = ex.Message });
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(int id, [FromBody] EmployerUpdateDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _employerService.UpdateEmployer(id, dto);
                return Ok(new { message = "Empregador atualizado com sucesso" });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Erro ao atualizar empregador", details = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var employer = await _employerService.GetEmployerById(id);

                if (employer == null)
                {
                    return NotFound(new { error = "Empregador não encontrado" });
                }

                await _employerService.RemoveEmployer(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Erro ao remover empregador", details = ex.Message });
            }
        }
    }
}