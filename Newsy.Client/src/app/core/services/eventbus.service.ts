import { Injectable } from '@angular/core';
import { Subject, Subscription, filter, map } from 'rxjs';
import { BusEvents, EmitEvent } from '../models/emit_event';

@Injectable({ providedIn: 'root' })
export class EventBus
{
  private subject = new Subject<any>();

  public emit(event: EmitEvent): void {
      this.subject.next(event);
  }

  public on(event: BusEvents, action: any): Subscription {
      return this.subject
          .pipe(
              filter((e: EmitEvent) => e.name === event),
              map((e: EmitEvent) => e.value),
          )
          .subscribe(action);
  }
}
