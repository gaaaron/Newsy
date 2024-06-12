import { Component } from '@angular/core';
import { Tag } from '../core/models/tag';
import { TagService } from '../core/services/tag.service';
import { Observable } from 'rxjs';
import { AsyncPipe } from '@angular/common';
import { EditorService } from '../core/components/editor-popup/editor-service';
import { EditorMeta } from '../core/components/editor-popup/data/editor-meta';

@Component({
  selector: 'app-tags',
  standalone: true,
  imports: [AsyncPipe],
  templateUrl: './tags.component.html'
})
export class TagsComponent {
  tags$!: Observable<Tag[]>;
  constructor(private tagService: TagService, private editorService:EditorService) {}

  ngOnInit(): void {
    this.refresh();
  }

  refresh() : void {
    this.tags$ = this.tagService.getAll();
  }

  async edit(tag:Tag) : Promise<void> {
    let result = await this.editorService.show(tag, [new EditorMeta('name', 'Név', 'text')]);
  }

  async createContainsTag() : Promise<void> {
    let tag = { name: '', textToMatch: '' };
    let result = await this.editorService.show(tag, [
      new EditorMeta('name', 'Name', 'text'), 
      new EditorMeta('textToMatch', 'Text to match', 'text')]);

    if (result) {
      this.tagService.createContainsTag(tag).subscribe(_ => this.refresh());
    }
  }
}
