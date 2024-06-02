using MediatR;
using Microsoft.SyndicationFeed.Rss;
using Microsoft.SyndicationFeed;
using Newsy.Domain.Abstractions;
using System.Xml;
using Newsy.Application.Common;
using Newsy.Domain.Entities;

namespace Newsy.Application.Source.Commands.ScrapeRss;
internal class ScrapeRssCommandHandler(INewsySystemRepository newsySystemRepository, IUnitOfWork unitOfWork) : IRequestHandler<ScrapeRssCommand, string>
{
    public async Task<string> Handle(ScrapeRssCommand request, CancellationToken cancellationToken)
    {
        var rss = newsySystemRepository.GetRssSourceById(request.SourceId);
        Guard.NotFound(rss, request.SourceId);

        var scrapeData = await ScrapeRss(rss!);
        rss!.AddContent(scrapeData);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        if (scrapeData.Count > 0)
        {
            return $"{scrapeData.Count} news scraped.";
        }

        return "No new news.";
    }

    private static async Task<List<RssScrapeData>> ScrapeRss(RssSource rss)
    {
        var scrapeData = new List<RssScrapeData>();

        using XmlReader xmlReader = XmlReader.Create(rss.RssUrl.Value);
        var feedReader = new RssFeedReader(xmlReader);
        while (await feedReader.Read())
        {
            switch (feedReader.ElementType)
            {
                //// Read category
                //case SyndicationElementType.Category:
                //    ISyndicationCategory category = await feedReader.ReadCategory();
                //    break;

                //// Read Image
                //case SyndicationElementType.Image:
                //    ISyndicationImage image = await feedReader.ReadImage();
                //    break;

                // Read Item
                case SyndicationElementType.Item:
                    ISyndicationItem item = await feedReader.ReadItem();
                    if (item.Published < rss.LastScraped)
                        return scrapeData;

                    var link = item.Links.FirstOrDefault()?.Uri.ToString() ?? string.Empty;
                    scrapeData.Add(new RssScrapeData(item.Id, item.Title, item.Description ?? string.Empty, link, item.Published.DateTime));
                    break;

                    //// Read link
                    //case SyndicationElementType.Link:
                    //    ISyndicationLink link = await feedReader.ReadLink();
                    //    break;

                    //// Read Person
                    //case SyndicationElementType.Person:
                    //    ISyndicationPerson person = await feedReader.ReadPerson();
                    //    break;

                    //// Read content
                    //default:
                    //    ISyndicationContent content = await feedReader.ReadContent();
                    //    break;
            }
        }

        return scrapeData;
    }
}
