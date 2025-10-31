using Microsoft.AspNetCore.Mvc;
using Personnel.Api.ValidationDTO;
using Personnel.Application.Services.DTO;
using Personnel.Application.Services.Interfaces;
using Shared.Domain.Exceptions;

namespace Personnel.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PersonController : ControllerBase
{
    private readonly IPersonService _personService;
    private readonly IImageService _imageService;

    public PersonController(IPersonService personService, IImageService imageService)
    {
        _personService = personService;
        _imageService = imageService;
    }

    [HttpGet("{personId}")]
    public async Task<IActionResult> GetPerson([FromRoute] Guid personId, [FromQuery] bool trackChanges = false)
    {
        var response = await _personService.GetPersonAsync(personId);
        return Ok(response);
    }

    [HttpPost]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> CreatePerson([FromForm] CreatePersonRequest request, IFormFile? avatar)
    {
        if (avatar != null)
        {
            var validator = new ImageValidator();
            var validationResult = await validator.ValidateAsync(avatar);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors.Select(e => e.ErrorMessage));

            using var stream = avatar.OpenReadStream();
            var avatarUrl = await _imageService.UploadImageAsync(stream, avatar.FileName, avatar.ContentType);
            request.AvatarUrl = avatarUrl;
        }
        else
        {
            request.AvatarUrl = null;
        }

        var response = await _personService.CreatePersonAsync(request);
        return StatusCode(201, response);
    }

    [HttpPut]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> UpdatePerson([FromForm] UpdatePersonRequest request, IFormFile? avatar)
    {
        if (avatar != null)
        {
            var validator = new ImageValidator();
            var validationResult = await validator.ValidateAsync(avatar);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors.Select(e => e.ErrorMessage));

            using var stream = avatar.OpenReadStream();
            var avatarUrl = await _imageService.UploadImageAsync(stream, avatar.FileName, avatar.ContentType);
            request.AvatarUrl = avatarUrl;
        }
        else
        {
            request.AvatarUrl = null;
        }

        var response = await _personService.UpdatePersonAsync(request);
        return Ok(response);
    }

    [HttpDelete]
    public async Task<IActionResult> DeletePerson([FromRoute] Guid personId)
    {
        await _personService.DeletePersonAsync(personId);
        return NoContent();
    }

    [HttpGet("{personId}/avatar")]
    public async Task<IActionResult> GetAvatar([FromRoute] Guid personId)
    {
        var imagePerson = await _personService.GetImageNameAsync(personId);
        if (imagePerson == null)
            return NotFound("Аватарка не найдена.");

        var result = await _imageService.DownloadImageAsync(imagePerson);

        var (stream, fileName) = result.Value;

        var contentType = Path.GetExtension(fileName).ToLower() switch
        {
            ".jpg" or ".jpeg" => "image/jpeg",
            ".png" => "image/png",
            ".svg" => "image/svg+xml",
            _ => "application/octet-stream"
        };

        return File(stream, contentType, fileName);
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