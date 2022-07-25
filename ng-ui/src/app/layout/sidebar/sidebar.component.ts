import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'nav[app-sidebar]',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './sidebar.component.html'
})
export class SidebarComponent implements OnInit {

  constructor() { }

  ngOnInit(): void {
  }

}
