import { Injectable } from '@angular/core';
import { EditorMeta } from './data/editor-meta';
import { EditorData } from './data/editor-data';

@Injectable({ providedIn: 'root' })
export class EditorService
{
  private innerAction!: ((data:EditorData, meta:EditorMeta[]) => Promise<boolean>);

  public async show(data:EditorData, meta:EditorMeta[]): Promise<boolean> {
    return await this.innerAction(data, meta);
  }

  public setAction(action: ((data:EditorData, meta:EditorMeta[]) => Promise<boolean>)) {
      this.innerAction = action;
  }
}
