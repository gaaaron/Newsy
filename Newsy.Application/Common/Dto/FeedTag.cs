namespace Newsy.Application.Common.Dto;

public sealed record FeedTag(Guid TagId, string Name, FeedTagState State);
public enum FeedTagState
{
    None,
    Included,
    Excluded
}
