import { CommonModule } from '@angular/common';
import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
  selector: 'app-thumbnail-list',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './thumbnail-list.component.html',
  styleUrl: './thumbnail-list.component.css',
})
export class ThumbnailListComponent {
  @Input() photos: any[] = [];         // <--- Esto permite el [photos]
  @Input() selectedIndex: number = 0;  // <--- Esto permite el [selectedIndex]


  @Output() photoSelected = new EventEmitter<number>(); // <--- Para el evento


  selectPhoto(index: number) {
    this.photoSelected.emit(index);
  }
}
