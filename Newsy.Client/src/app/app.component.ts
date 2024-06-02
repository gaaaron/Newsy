import { Component, inject, TemplateRef } from '@angular/core';
import { RouterLink, RouterOutlet } from '@angular/router';
import { DashboardComponent } from './dashboard/dashboard.component';
import { NgbOffcanvas } from '@ng-bootstrap/ng-bootstrap';
import { MenuComponent } from './menu/menu.component';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, RouterLink, DashboardComponent, MenuComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  private offcanvasService = inject(NgbOffcanvas);
  title = 'Newsy.Client';

  open(content: TemplateRef<any>) {
		this.offcanvasService.open(content, { ariaLabelledBy: 'offcanvas-basic-title' });
	}

}
