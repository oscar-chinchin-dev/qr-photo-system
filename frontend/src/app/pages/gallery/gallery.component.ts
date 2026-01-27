import { ChangeDetectorRef, Component } from '@angular/core';
import { ThumbnailListComponent } from "../../components/thumbnail-list/thumbnail-list.component";
import { PhotoViewerComponent } from "../../components/photo-viewer/photo-viewer.component";
import { PhotoControlsComponent } from "../../components/photo-controls/photo-controls.component";
import { ActivatedRoute } from '@angular/router';
import { CommonModule } from '@angular/common';
import { Visita, Foto, VisitaService } from '../../services/visita.service';


// Componente principal de la galería de la visita.
// Responsable de renderizar las fotos y manejar la interacción del usuario.

@Component({
  selector: 'app-gallery',
  imports: [ThumbnailListComponent, PhotoViewerComponent, PhotoControlsComponent, CommonModule],
  templateUrl: './gallery.component.html',
  styleUrl: './gallery.component.css',
})



export class GalleryComponent {
  console: any;
  isBuying = false;
  qrInvalido = false;
  errorMessage = '';
  lastPurchaseCount = 0;
  showPurchaseSummary = false;

  constructor(
    private route: ActivatedRoute,
    private visitaService: VisitaService,
    private cdr: ChangeDetectorRef
  ) { }

  photos: Foto[] = [];
  selectedIndex = 0;
  selectedPhotos: number[] = [];
  visita!: Visita;

  // --- Gestión de Sesión y Carga Inicial ---
  // Captura el QR de la URL para identificar la sesión y sincronizar los datos del servidor.

  ngOnInit() {
    const qr = this.route.snapshot.paramMap.get('qr');
    console.log('QR recibido:', qr);

    if (!qr) {
      console.log('No hay QR');
      return;
    }

    this.visitaService.getByQr(qr).subscribe({
      next: visita => {
        console.log('Visita recibida:', visita);
        this.visita = visita;
        this.photos = visita.fotos ?? [];
        this.selectedIndex = 0;
        this.cdr.detectChanges();
      },
      error: () => {
        console.error('Error backend');
        this.qrInvalido = true;
      }
    });
  }

  // --- Navegación de la Galería ---
  // Controla el flujo visual del carrusel, permitiendo al usuario recorrer sus fotos.

  onPhotoSelected(index: number) {
    this.selectedIndex = index;
  }

  prevPhoto() {
    if (this.selectedIndex > 0) {
      this.selectedIndex--;
    }
  }

  nextPhoto() {
    if (this.selectedIndex < this.photos.length - 1) {
      this.selectedIndex++;
    }
  }

  // --- Lógica de Selección (Carrito de Compra) ---
  // Actúa como un "carrito" temporal. Solo permite marcar fotos no pagadas 
  // para prepararlas para una compra en lote.

  toggleFavorite() {
    const photo = this.photos[this.selectedIndex];

    if (photo.pagada) return;

    photo.favorite = !photo.favorite;

    if (photo.favorite) {
      this.selectedPhotos.push(photo.id);
    } else {
      this.selectedPhotos = this.selectedPhotos.filter(
        id => id !== photo.id
      );
    }
  }

  // --- Procesamiento de Transacciones ---
  // buyPhoto: Ejecuta la compra inmediata de la imagen que se está visualizando.
  // buySelection: Formaliza la compra de todo el grupo de fotos marcado como "favoritas".

  buyPhoto() {
    if (this.isBuying) return;

    const photo = this.photos[this.selectedIndex];
    if (photo.pagada) return;

    this.isBuying = true;

    this.visitaService
      .createSeleccion(this.visita.id, [photo.id])
      .subscribe({
        next: () => {
          this.lastPurchaseCount = 1;
          this.showPurchaseSummary = true;

          this.reloadVisita();
          this.isBuying = false;
        },
        error: () => {
          this.errorMessage = 'No se pudo completar la compra. Intenta nuevamente.';
          this.isBuying = false;
        }
      });
  }

  buySelection() {
    if (this.isBuying) return;
    if (this.selectedPhotos.length === 0) return;

    this.isBuying = true;

    this.visitaService
      .createSeleccion(this.visita.id, this.selectedPhotos)
      .subscribe({
        next: () => {
          this.lastPurchaseCount = this.selectedPhotos.length;
          this.showPurchaseSummary = true;

          this.reloadVisita();
          this.isBuying = false;
        },
        error: err => {
          console.error('Error al comprar selección', err);
          this.isBuying = false;
        }
      });
  }

  // --- Sincronización de Estado ---
  // Verifica si el cliente ya ha adquirido la totalidad de su galería.

  get todasPagadas(): boolean {
    return this.photos.length > 0 && this.photos.every(p => p.pagada);
  }

  // Actualiza la interfaz tras una compra para reflejar los nuevos permisos (ej. quitar marcas de agua).

  reloadVisita() {
    this.visitaService.getByQr(this.visita.qrCode).subscribe({
      next: visita => {
        this.visita = visita;
        this.photos = visita.fotos;
        this.selectedIndex = 0;
        this.selectedPhotos = [];
      },
      error: err => {
        console.error('Error al recargar visita', err);
      }



    });
  }
}






