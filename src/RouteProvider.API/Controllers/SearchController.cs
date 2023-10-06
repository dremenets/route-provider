using Microsoft.AspNetCore.Mvc;

namespace RouteProvider.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class SearchController : ControllerBase
{
    private readonly ILogger<SearchController> _logger;

    public SearchController(ILogger<SearchController> logger)
    {
        _logger = logger;
    }

    [HttpPost]
    public Task<int> Post()
    {
        return Task.FromResult(1);
    }
}