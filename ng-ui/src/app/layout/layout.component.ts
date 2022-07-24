import { Component } from '@angular/core';

@Component({
  standalone: true,
  selector: 'div[app-layout]',
  templateUrl: './layout.component.html',
  styleUrls: ['./layout.component.css']
})
export class LayoutComponent {
  isCollapsed = false;
}
