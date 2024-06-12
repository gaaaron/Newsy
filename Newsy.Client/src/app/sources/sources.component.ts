import { Component } from '@angular/core';
import { Observable } from 'rxjs';
import { Source } from '../core/models/source';
import { SourceService } from '../core/services/source.service';
import { AsyncPipe } from '@angular/common';
import { AppToastService } from '../core/services/toast.service';
import { EditorService } from '../core/components/editor-popup/editor-service';
import { CreateOrdEditRssSource } from '../core/models/create_or_edit_rss_source';
import { EditorMeta } from '../core/components/editor-popup/data/editor-meta';

@Component({
  selector: 'app-sources',
  standalone: true,
  imports: [AsyncPipe],
  templateUrl: './sources.component.html'
})
export class SourcesComponent {
  sources$!: Observable<Source[]>;
  constructor(private sourceService: SourceService, private toast: AppToastService, private editorService: EditorService) {}

  ngOnInit(): void {
    this.sources$ = this.sourceService.getAll();
  }

  refresh(){
    this.sources$ = this.sourceService.getAll();
  }

  remove(id:string) {
    this.sourceService.delete(id).subscribe(result => {
      this.refresh();
    });
  }

  scrape(id:string) {
    this.sourceService.scrape(id).subscribe(result => {
      this.toast.show('Scrape finished', result);
    });
  }

  async createRssSource() : Promise<void> {
    let source = new CreateOrdEditRssSource(null, '', '');
    let result = await this.editorService.show(source, [
      new EditorMeta('name', 'Name', 'text'), 
      new EditorMeta('rssUrl', 'RSS Url', 'text')]);

    if (result) {
      this.sourceService.createOrdEditRss(source).subscribe(_ => this.refresh());
    }
  }

  async editRssSource(edited: Source) : Promise<void> {
    let source = new CreateOrdEditRssSource(edited.id, edited.name, edited.content);
    let result = await this.editorService.show(source, [
      new EditorMeta('name', 'Name', 'text'), 
      new EditorMeta('rssUrl', 'RSS Url', 'text')]);

    if (result) {
      this.sourceService.createOrdEditRss(source).subscribe(_ => this.refresh());
    }
  }
}
