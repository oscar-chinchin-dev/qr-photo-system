import { ComponentFixture, TestBed } from '@angular/core/testing';

import { QrEntryComponent } from './qr-entry.component';

describe('QrEntryComponent', () => {
  let component: QrEntryComponent;
  let fixture: ComponentFixture<QrEntryComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [QrEntryComponent]
    })
      .compileComponents();

    fixture = TestBed.createComponent(QrEntryComponent);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
