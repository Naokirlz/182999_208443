import { TestBed } from '@angular/core/testing';

import { UserNotLoggedGuard } from './user-not-logged.guard';

describe('UserNotLoggedGuard', () => {
  let guard: UserNotLoggedGuard;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    guard = TestBed.inject(UserNotLoggedGuard);
  });

  it('should be created', () => {
    expect(guard).toBeTruthy();
  });
});
