using MediatR;
using Newsy.Domain.Abstractions;
using System.Runtime.CompilerServices;

namespace Newsy.Application.Tag.Queries.GetAll;

internal class GetAllTagsQueryHandler(IApplicationDbContext applicationDbContext) : IStreamRequestHandler<GetAllTagsQuery, TagDto>
{
    public async IAsyncEnumerable<TagDto> Handle(GetAllTagsQuery request, [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        var tags = applicationDbContext.Tags.AsAsyncEnumerable();
        await foreach (var tag in tags)
        {
            if (cancellationToken.IsCancellationRequested)
                yield break;

            yield return new TagDto(tag.Id, tag.Name, tag.GetType().Name);
        }
    }
}
