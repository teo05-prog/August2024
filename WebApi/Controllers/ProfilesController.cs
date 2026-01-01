using Microsoft.AspNetCore.Mvc;
using WebApi.Entities;
using WebApi.Services;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProfilesController : ControllerBase
{
    private readonly IMatchService _matchService;

    public ProfilesController(IMatchService matchService)
    {
        _matchService = matchService;
    }
    
    [HttpPost]
    public ActionResult<Profile> CreateProfile([FromBody] Profile profile)
    {
        try
        {
            var createdProfile = _matchService.CreateProfile(profile);
            return CreatedAtAction(nameof(GetProfile), new { id = createdProfile.Id }, createdProfile);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }

    [HttpPost("{profileId}/likes/{likeProfileId}")]
    public IActionResult AddLike(int profileId, int likeProfileId)
    {
        try
        {
            _matchService.AddLike(profileId, likeProfileId);
            return NoContent();
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }
    
    [HttpGet]
    public ActionResult<List<Profile>> GetAllProfiles([FromQuery] string? gender = null, [FromQuery] int? minAge = null, [FromQuery] int? maxAge = null)
    {
        try
        {
            var profiles = _matchService.GetAllProfiles(gender, minAge, maxAge);
            return Ok(profiles);
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }
    
    [HttpGet("{profileId}/matches")]
    public ActionResult<List<Profile>> GetMatches(int profileId)
    {
        try
        {
            var matches = _matchService.GetMatches(profileId);
            return Ok(matches);
        }
        catch (ArgumentException ex)
        {
            return NotFound(new { error = ex.Message });
        }
    }

    [HttpGet("{id}")]
    public ActionResult<Profile> GetProfile(int id)
    {
        var profiles = _matchService.GetAllProfiles();
        var profile = profiles.FirstOrDefault(p => p.Id == id);
        
        if (profile == null)
            return NotFound(new { error = $"Profile with ID {id} not found." });

        return Ok(profile);
    }
}

