import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { HeaderComponent } from './header/header.component';
import { SidebarComponent } from './sidebar/sidebar.component';

@Component({
  standalone: true,
  imports: [CommonModule, HeaderComponent, SidebarComponent],
  selector: 'div[app-layout]',
  templateUrl: './layout.component.html'
})
export class LayoutComponent {
}
