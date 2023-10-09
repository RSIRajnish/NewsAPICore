using Microsoft.AspNetCore.Mvc;
using NewsAPICore.BLL.Services.IServices;

namespace NewsAPICore.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class NewsController : ControllerBase
{
    private readonly INewsService _newsService;

    public NewsController(INewsService newsService)
    {
        _newsService = newsService;
    }

    /// <summary>
    /// Get the Stories Id list
    /// </summary>
    /// <returns></returns>

    [HttpGet("GetStories")]
    public async Task<IActionResult> GetStories()
    {
        try
        {
            return Ok(await _newsService.GetStories());
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    /// <summary>
    /// Get the actual story list based on story id
    /// </summary>
    /// <param name="pageNo"></param>
    /// <param name="startPosition"></param>
    /// <param name="IsNewRequest"></param>
    /// <returns></returns>

    [HttpGet("GetStoriesItem")]
    public async Task<IActionResult> GetStoriesItem(int pageNo=0,int startPosition=0, int noOfRecords=200)
    {
        try
        {
            return Ok(await _newsService.GetStoriesItem(pageNo, startPosition, noOfRecords));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}