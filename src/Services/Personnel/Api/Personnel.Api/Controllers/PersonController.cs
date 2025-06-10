using Microsoft.AspNetCore.Mvc;
using Personnel.Application.Services.DTO;
using Personnel.Application.Services.Interfaces;
using Shared.Domain.Exceptions;

namespace Personnel.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PersonController : ControllerBase
{
    private readonly IPersonService _personService;

    public PersonController(IPersonService personService)
    {
        _personService = personService;
    }

    [HttpGet("{personId}")]
    public async Task<IActionResult> GetPerson([FromRoute] Guid personId, [FromQuery] bool trackChanges = false)
    {
        var response = await _personService.GetPersonAsync(personId);
        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> CreatePerson([FromBody] CreatePersonRequest request)
    {
        var response = await _personService.CreatePersonAsync(request);
        return Ok(response);
    }

    [HttpPut]
    public async Task<IActionResult> UpdatePerson([FromBody] UpdatePersonRequest request)
    {
        var response = await _personService.UpdatePersonAsync(request);
        return Ok(response);
    }

    /// <summary>
    /// Добавление опыта работы к человеку
    /// </summary>
    [HttpPost("{personId}/work-experiences")]
    public async Task<IActionResult> AddWorkExperience([FromRoute] Guid personId, [FromBody] WorkExperienceDto dto)
    {
        try
        {
            await _personService.AddWorkExperienceAsync(personId, dto);
            return NoContent();
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { Errors = ex.Message });
        }
        catch (EntityNotFoundException ex)
        {
            return NotFound(new { Errors = ex.Message });
        }
    }

    /// <summary>
    /// Обновление опыта работы
    /// </summary>
    [HttpPut("{personId}/work-experiences/{workExperienceId}")]
    public async Task<IActionResult> UpdateWorkExperience(
        [FromRoute] Guid personId,
        [FromRoute] Guid workExperienceId,
        [FromBody] UpdateWorkExperienceRequest request)
    {
        if (request.Id != workExperienceId)
            return BadRequest("Идентификатор в теле запроса не совпадает с URL.");

        await _personService.UpdateWorkExperienceAsync(personId, request);
        return NoContent();
    }

    /// <summary>
    /// Удаление опыта работы
    /// </summary>
    [HttpDelete("{personId}/work-experiences/{workExperienceId}")]
    public async Task<IActionResult> DeleteWorkExperience([FromRoute] Guid personId, [FromRoute] Guid workExperienceId)
    {
        await _personService.DeleteWorkExperienceAsync(personId, workExperienceId);
        return NoContent();
    }
}