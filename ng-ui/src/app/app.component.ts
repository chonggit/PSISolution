import { Component } from '@angular/core';
import { RouterModule } from '@angular/router';
import { HeaderService } from './services/header.service';

@Component({
  standalone: true,
  selector: 'div[app-root]',
  imports: [RouterModule],
  providers: [HeaderService],
  template: '<router-outlet></router-outlet>'
})
export class AppComponent {
  title = 'ng-ui';
}
