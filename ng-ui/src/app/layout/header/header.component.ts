import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HeaderService } from '../../services/header.service';

@Component({
  selector: 'header[app-header]',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './header.component.html'
})
export class HeaderComponent implements OnInit {

  constructor(private headerService: HeaderService) { }

  ngOnInit(): void { }

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
