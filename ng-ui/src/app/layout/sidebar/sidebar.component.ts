import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { Collapse } from 'bootstrap';

@Component({
  selector: 'div[app-sidebar]',
  standalone: true,
  templateUrl: './sidebar.component.html',
  imports: [CommonModule, RouterModule]
})
export class SidebarComponent implements OnInit {

  homeCollapse!: Collapse;
  accountCollapse!: Collapse;

  constructor() { }

  ngOnInit(): void {

    this.homeCollapse = new Collapse('#home-collapse', { toggle: false });
    this.accountCollapse = new Collapse('#account-collapse', { toggle: false });
  }

  homeCollapseToggle() {
    this.homeCollapse.toggle();
  }

  accountCollapseToggle() {
    this.accountCollapse.toggle();
  }
}
