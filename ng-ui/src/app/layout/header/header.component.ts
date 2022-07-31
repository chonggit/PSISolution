import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HeaderService } from '../../services/header.service';
import { Collapse } from 'bootstrap';

@Component({
  selector: 'header[app-header]',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './header.component.html'
})
export class HeaderComponent implements OnInit {

  headerNavCollapse!: Collapse;

  constructor(private headerService: HeaderService) { }

  ngOnInit(): void {

    this.headerNavCollapse = new Collapse('#header_nav', { toggle: false });
  }

  headerNavCollapseToggle() {
    this.headerNavCollapse.toggle();
  }

  add(): void {
    this.headerService.add();
  }

  edit(): void {
    this.headerService.edit();
  }

  remove(): void {
    this.headerService.remove();
  }

  print(): void {
    this.headerService.print();
  }

  view(): void {
    this.headerService.view();
  }
}
