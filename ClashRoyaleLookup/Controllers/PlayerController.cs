using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class PlayerController
{
    private readonly string _apiKey;
    
    public PlayerController(IConfiguration configuration)
    {
        _apiKey = configuration["ClashRoyaleApiKey"];
    }
    
    [HttpGet("{tag}")]
    public async Task<IResult> Get(string tag)
    {
        try
        {
            var profile = await ClashRoyaleLookup.ClashLookup.SearchForUser(tag, _apiKey);
            return profile == null ? Results.NotFound(new { message = "User not found" }) : Results.Ok(profile);
        }
        catch (Exception e)
        {
            return Results.Problem(e.Message + "\n" + e.StackTrace);
        }
    }
}