using Microsoft.EntityFrameworkCore.Storage;

namespace URLShortener.Models;

public class Link
{
    public int Id { get; set; }
    public string? Original { get; set; }
    public string? Shortened { get; set; }
    public DateTime ShortenedAt { get; set; }
}