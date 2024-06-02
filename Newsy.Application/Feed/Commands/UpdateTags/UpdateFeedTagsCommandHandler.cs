using MediatR;
using Newsy.Application.Common;
using Newsy.Application.Common.Dto;
using Newsy.Domain.Abstractions;

namespace Newsy.Application.Feed.Commands.UpdateTags;

internal sealed class UpdateFeedTagsCommandHandler(
    INewsySystemRepository systemRepository, IUnitOfWork unitOfWork) :
    IRequestHandler<UpdateFeedTagsCommand>
{
    public async Task Handle(UpdateFeedTagsCommand request, CancellationToken cancellationToken)
    {
        var feed = systemRepository.GetFeedById(request.FeedId);
        Guard.NotFound(feed);

        var includeTags = request.FeedTags.Where(x => x.State == FeedTagState.Included).Select(x => x.TagId).ToList();
        var excludeTags = request.FeedTags.Where(x => x.State == FeedTagState.Excluded).Select(x => x.TagId).ToList();

        feed!.UpdateRules(includeTags, excludeTags);

        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
