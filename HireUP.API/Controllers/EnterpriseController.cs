using HireUP.Application.DTOs.Enterprise;
using HireUP.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace HireUP.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EnterpriseController : ControllerBase
    {
        private readonly EnterpriseApplicationService _enterpriseService;

        public EnterpriseController(EnterpriseApplicationService enterpriseService)
        {
            _enterpriseService = enterpriseService;
        }

        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Register([FromBody] EnterpriseRegisterDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _enterpriseService.CreateEnterprise(dto);
                return CreatedAtAction(nameof(Register), new { message = "Empresa registrada com sucesso" });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Erro ao registrar empresa", details = ex.Message });
            }
        }

        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Login([FromBody] EnterpriseLoginDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var enterprise = await _enterpriseService.LoginEnterprise(dto);

                var response = new
                {
                    id = enterprise.Id,
                    name = enterprise.Name,
                    email = enterprise.Email,
                    phone = enterprise.Phone,
                    document = enterprise.Document,
                    field = enterprise.Field,
                    description = enterprise.Description,
                    geoLocationId = enterprise.GeoLocationId,
                    createdAt = enterprise.CreatedAt,
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
                var enterprises = await _enterpriseService.GetAllEnterprises();
                return Ok(enterprises);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Erro ao buscar empresas", details = ex.Message });
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var enterprise = await _enterpriseService.GetEnterpriseById(id);

                if (enterprise == null)
                {
                    return NotFound(new { error = "Empresa não encontrada" });
                }

                return Ok(enterprise);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Erro ao buscar empresa", details = ex.Message });
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(int id, [FromBody] EnterpriseUpdateDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _enterpriseService.UpdateEnterprise(id, dto);
                return Ok(new { message = "Empresa atualizada com sucesso" });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Erro ao atualizar empresa", details = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var enterprise = await _enterpriseService.GetEnterpriseById(id);

                if (enterprise == null)
                {
                    return NotFound(new { error = "Empresa não encontrada" });
                }

                await _enterpriseService.RemoveEnterprise(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Erro ao remover empresa", details = ex.Message });
            }
        }
    }
}