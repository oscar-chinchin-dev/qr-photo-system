import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';


export interface Foto {
  id: number;
  imagenBase64?: string;
  pagada: boolean;
  favorite?: boolean; // 👈 estado solo de UI
}

export interface Visita {
  id: number;
  qrCode: string;
  fecha: string;
  fotos: Foto[];
}

@Injectable({
  providedIn: 'root'
})
export class VisitaService {

  private apiUrl = `${environment.apiURL}/visitas`;

  constructor(private http: HttpClient) { }

  getByQr(qr: string): Observable<Visita> {
    return this.http.get<Visita>(`${this.apiUrl}/${qr}`);
  }

  createSeleccion(visitaId: number, fotoIds: number[]) {
    return this.http.post<{ id: number }>(
      `${environment.apiURL}/selecciones`,
      {
        visitaId,
        fotoIds
      }
    );
  }
}
