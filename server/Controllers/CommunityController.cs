using Microsoft.AspNetCore.Mvc;
using Verbum.API.DTOs.Community;
using Verbum.API.Interfaces.Services;

namespace Verbum.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CommunityController : ControllerBase {
    private readonly ICommunityService _communityService;

    public CommunityController(ICommunityService communityService) {
        _communityService = communityService;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCommunityById(int id) {
        var result = await _communityService.GetCommunityById(id);
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetCommunities() {
        List<CommunityDto> result = await _communityService.GetCommunities();
        return Ok(result);
    }

    [HttpGet("{name}")]
    public async Task<IActionResult> GetCommunitiesByName(string name) {
        List<CommunityDto> result = await _communityService.GetCommunitiesByName(name);
        return Ok(result);
    }
}