import { Component } from '@angular/core';
import { RouterModule } from '@angular/router';

@Component({
  standalone: true,
  selector: 'div[app-root]',
  imports: [RouterModule],
  template: '<router-outlet></router-outlet>'
})
export class AppComponent {
  title = 'ng-ui';
}
