import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-qr-entry',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './qr-entry.component.html',
  styleUrl: './qr-entry.component.css',
})
export class QrEntryComponent {

  qrCode = '';

  constructor(private router: Router) { }

  verFotos() {
    if (!this.qrCode || this.qrCode.trim() === '') return;

    this.router.navigate(['/gallery', this.qrCode]);
  }
}

