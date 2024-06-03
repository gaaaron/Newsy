import { Component } from '@angular/core';
import { Tag } from '../core/models/tag';
import { TagService } from '../core/services/tag.service';
import { Observable } from 'rxjs';
import { AsyncPipe } from '@angular/common';
import { TagCreatorPopupComponent } from './tag-creator-popup/tag-creator-popup.component';

@Component({
  selector: 'app-tags',
  standalone: true,
  imports: [AsyncPipe, TagCreatorPopupComponent],
  templateUrl: './tags.component.html'
})
export class TagsComponent {
  tags$!: Observable<Tag[]>;
  constructor(private tagService: TagService) {}

  ngOnInit(): void {
    this.refresh();
  }

  refresh() : void {
    this.tags$ = this.tagService.getAll();
  }
}
