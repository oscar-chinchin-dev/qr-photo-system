import { Routes } from '@angular/router';
import { QrEntryComponent } from './pages/qr-entry/qr-entry.component';
import { GalleryComponent } from './pages/gallery/gallery.component';

export const routes: Routes = [
    {
        path: '',
        component: QrEntryComponent
    },
    {
        path: 'gallery/:qr',
        component: GalleryComponent
    },
    {
        path: '**',
        redirectTo: ''
    }
];

