using ClashRoyaleLookup;
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
            var profile = await ClashLookup.SearchForUser(tag, _apiKey);
            return profile == null ? Results.NotFound(new { message = "User not found" }) : Results.Ok(profile);
        }
        catch (Exception e)
        {
            return Results.Problem(e.Message + "\n" + e.StackTrace);
        }
    }

    [HttpGet("badge/{badgeId}")]
    public async Task<IResult> Get(int badgeId)
    {
        try
        {
            var icon = await ClashLookup.GrabClanIcon(badgeId);

            if (icon != null)
                icon.IconUrl = $"https://royaleapi.github.io/cr-api-assets/badges/{icon.Name}.png";
            return icon == null ? Results.NotFound(new { message = "Icon not found " }) : Results.Ok(icon);
        }
        catch (Exception e)
        {
            return Results.Problem(e.Message + "\n" + e.StackTrace);
        }
    }
}