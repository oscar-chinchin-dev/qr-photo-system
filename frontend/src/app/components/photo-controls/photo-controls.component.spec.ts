import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PhotoControlsComponent } from './photo-controls.component';

describe('PhotoControlsComponent', () => {
  let component: PhotoControlsComponent;
  let fixture: ComponentFixture<PhotoControlsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [PhotoControlsComponent]
    })
      .compileComponents();

    fixture = TestBed.createComponent(PhotoControlsComponent);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
