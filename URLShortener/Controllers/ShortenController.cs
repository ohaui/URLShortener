using Microsoft.AspNetCore.Mvc;
using URLShortener.Data;
using URLShortener.Models;

namespace URLShortener.Controllers;

public class ShortenController : BaseController
{
    private readonly ShortenerContext _context;
    private const string _WebsiteLink = "https://localhost:7241/";

    public ShortenController(ShortenerContext context)
    {
        _context = context;
    }
    
    public async Task<string> Shorten(string link)
    {
        var fullUri = CreateShortenLink(_WebsiteLink);

        var isLinkContains = _context.Links.FirstOrDefault(x => x.Original == link); //need to rename 🤣

        if (isLinkContains != default)
        {
            return isLinkContains.Shortened!;
        }
        
        var shortenedLink = new Link
        {
            Original = link,
            Shortened = fullUri,
            ShortenedAt = DateTime.Now
        };
        
        await _context.Links.AddAsync(shortenedLink);
        await _context.SaveChangesAsync();

        return shortenedLink.Shortened;
    }

    private string CreateShortenLink(string url)
    {
        var guid = Guid.NewGuid().ToString()[24..];
        var fullLink = url + guid;

        if (_context.Links.FirstOrDefault(x => x.Shortened == url + guid) != default)
        {
            CreateShortenLink(url);
        }

        return fullLink;

    }
    
}