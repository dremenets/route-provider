using Microsoft.AspNetCore.Mvc;
using RouteProvider.API.Model.Requests;
using RouteProvider.API.Model.Responses;
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
    public async Task<ActionResult<SearchResponse?>> Post([FromBody] SearchRequest request)
    {
        var result = await _searchService.GetRoute(request);
        if (result == null)
            return StatusCode(500);

        return Ok(result);
    }
}