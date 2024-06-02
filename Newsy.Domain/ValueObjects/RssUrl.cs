using System.ComponentModel.DataAnnotations;

namespace Newsy.Domain.ValueObjects;
public record RssUrl
{
    private RssUrl(string value)
    {
        Value = value;
    }

    public string Value { get; }

    public static RssUrl Create(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            throw new ValidationException("Rss url is empty.");
        }

        return new RssUrl(value);
    }
}
