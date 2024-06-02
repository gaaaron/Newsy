import { Routes } from '@angular/router';
import { DashboardComponent } from './dashboard/dashboard.component';
import { SourcesComponent } from './sources/sources.component';
import { TagsComponent } from './tags/tags.component';

export const routes: Routes = [{
    path: '',
    component: DashboardComponent,
    title: 'Dashboard page',
}, {
    path: 'sources',
    component: SourcesComponent,
    title: 'Sources page',
}, {
    path: 'tags',
    component: TagsComponent,
    title: 'Tags page',
}];
