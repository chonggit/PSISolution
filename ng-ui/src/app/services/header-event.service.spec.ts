import { TestBed } from '@angular/core/testing';

import { NavBarService } from './header-event.service';

describe('NavBarService', () => {
  let service: NavBarService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(NavBarService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
