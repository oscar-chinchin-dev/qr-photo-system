import { inject, Injectable } from '@angular/core';
import { environment } from '../environments/environment';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root',
})
export class WeatherforecastService {

  constructor() { }
  private http = inject(HttpClient);
  private URLbase = environment.apiURL + '/weatherforecast';

  public obtenerClima() {
    return this.http.get<any>(this.URLbase);
  }

}
