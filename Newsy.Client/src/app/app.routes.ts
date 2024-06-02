import { Routes } from '@angular/router';
import { SourcesComponent } from './sources/sources.component';
import { TagsComponent } from './tags/tags.component';
import { FeedComponent } from './feed/feed.component';

export const routes: Routes = [
    {
        path: '',
        component: SourcesComponent,
        title: 'Sources',
    },
    {
        path: 'tags',
        component: TagsComponent,
        title: 'Tags',
    },
    {
        path: 'feed/:id',
        component: FeedComponent,
        title: 'Feed',
    },
];
