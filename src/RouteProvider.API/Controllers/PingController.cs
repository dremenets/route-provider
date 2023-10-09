using Microsoft.AspNetCore.Mvc;
using RouteProvider.API.Services;

namespace RouteProvider.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class PingController : ControllerBase
{
    private readonly ISearchService _searchService;

    public PingController(ISearchService searchService)
    {
        _searchService = searchService;
    }

    [HttpGet]
    public async Task<ActionResult> Get() => await _searchService.Ping() ? StatusCode(200) : StatusCode(500);
}