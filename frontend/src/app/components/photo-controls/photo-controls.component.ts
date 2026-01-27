import { Component, EventEmitter, Output } from '@angular/core';

@Component({
  selector: 'app-photo-controls',
  standalone: true,
  imports: [],
  templateUrl: './photo-controls.component.html',
  styleUrl: './photo-controls.component.css',
})
export class PhotoControlsComponent {
  @Output() prev = new EventEmitter<void>();
  @Output() next = new EventEmitter<void>();
  @Output() toggleFavorite = new EventEmitter<void>();
}
