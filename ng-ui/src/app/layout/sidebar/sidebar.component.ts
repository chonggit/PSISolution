import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
@Component({
  selector: 'nav[app-sidebar]',
  standalone: true,
  templateUrl: './sidebar.component.html',
  imports: [CommonModule, RouterModule]
})
export class SidebarComponent implements OnInit {

  constructor() { }

  ngOnInit(): void {
  }

}
