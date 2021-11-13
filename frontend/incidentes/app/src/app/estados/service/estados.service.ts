import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Incidente } from '../../interfaces/incidente.interface';

@Injectable({
  providedIn: 'root'
})
export class EstadosService {

  private apiUrl: string = 'http://localhost:5000/api/Estados';
  token: string = sessionStorage.getItem('token')!;

  constructor(private http: HttpClient) { }

  put(incidente:Incidente): Observable<Incidente> {
    const httpOptions = {

      headers: new HttpHeaders({
        'Content-Type': 'application/json',
      })
    };

    httpOptions.headers = httpOptions.headers.set('autorizacion', this.token);

    return this.http.put<Incidente>(
      this.apiUrl,
      incidente,
      httpOptions
    );

  }


}
