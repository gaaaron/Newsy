export class CreateOrdEditRssSource {
    id: string | null;
    name: string;
    rssUrl: string;

    constructor(id: string|null, name: string, rssUrl: string) {
        this.id = id;
        this.name = name;
        this.rssUrl = rssUrl;
    }
}