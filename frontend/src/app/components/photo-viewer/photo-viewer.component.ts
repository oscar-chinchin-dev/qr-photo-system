import { CommonModule } from '@angular/common';
import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
  selector: 'app-photo-viewer',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './photo-viewer.component.html',
  styleUrl: './photo-viewer.component.css',
})
export class PhotoViewerComponent {
  @Input() photo: any; // <--- Esto permite el [photo]
  @Output() buy = new EventEmitter<void>();
  @Input() isBuying = false;
  @Output() prev = new EventEmitter<void>();
  @Output() next = new EventEmitter<void>();
  @Output() toggleFavorite = new EventEmitter<void>();
  console: any;


  onBuy() {
    if (!this.photo || this.photo.pagada) return;
    this.buy.emit();
  }
}
