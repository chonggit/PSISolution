import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { Component } from '@angular/core';
import { HeaderComponent } from './header/header.component';
import { SidebarComponent } from './sidebar/sidebar.component';

@Component({
  standalone: true,
  selector: 'div[app-layout]',
  templateUrl: './layout.component.html',
  imports: [CommonModule, RouterModule, HeaderComponent, SidebarComponent]
})
export class LayoutComponent {
}
