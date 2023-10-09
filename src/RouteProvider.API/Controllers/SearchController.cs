using Microsoft.AspNetCore.Mvc;
using RouteProvider.API.Services;

namespace RouteProvider.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class SearchController : ControllerBase
{
    private readonly ISearchService _searchService;

    public SearchController(ISearchService searchService)
    {
        _searchService = searchService;
    }

    [HttpPost]
    public Task<ActionResult> Post()
    {
        throw new NotImplementedException();
    }
}