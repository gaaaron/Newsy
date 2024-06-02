using System.ComponentModel.DataAnnotations;

namespace Newsy.Domain.ValueObjects;

public record FacebookUrl
{
    private FacebookUrl(string value)
    {
        Value = value;
    }

    public string Value { get; }

    public static FacebookUrl Create(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            throw new ValidationException("Facebook url is empty.");
        }

        return new FacebookUrl(value);
    }
}
