using Microsoft.AspNetCore.Mvc;
using URLShortener.Data;
using URLShortener.Models;

namespace URLShortener.Controllers;

public class ShortenController : BaseController
{
    private readonly ShortenerContext _context;

    public ShortenController(ShortenerContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<Link> Shorten(string link)
    {
        var guid =  System.Guid.NewGuid().ToString()[24..];
        var fullURI = "https://localhost:7241/" + guid;

        var shortenedLink = new Link
        {
            Original = link,
            Shortened = fullURI,
            ShortenedAt = DateTime.Now
        };
        
        await _context.Links.AddAsync(shortenedLink);
        await _context.SaveChangesAsync();

        return shortenedLink;
    }
    
}