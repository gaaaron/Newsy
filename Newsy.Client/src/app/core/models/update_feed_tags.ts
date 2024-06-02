import { FeedTag } from "./feed_tag"

export class UpdateFeedTags {
    feedId: string;
    feedTags: FeedTag[];

    constructor(feedId: string, feedTags: FeedTag[]){
        this.feedId = feedId;
        this.feedTags = feedTags;
    }
}