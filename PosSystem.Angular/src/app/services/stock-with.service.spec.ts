import { TestBed } from '@angular/core/testing';

import { StockWithService } from './stock-with.service';

describe('StockWithService', () => {
  let service: StockWithService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(StockWithService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
