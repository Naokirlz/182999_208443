import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Incidente } from 'src/app/interfaces/incidente.interface';

@Injectable({
  providedIn: 'root'
})
export class IncidentesService {

  
  private apiUrl: string = 'http://localhost:5000/api/Incidentes';
  private apiUrlEstados: string = 'http://localhost:5000/api/Estados';

  token: string = sessionStorage.getItem('token')!;

  constructor(private http: HttpClient) { }

  getProyecto(): Observable<Incidente[]> {
    const httpOptions = {

      headers: new HttpHeaders({
        'Content-Type': 'application/json',
      })
    };

    httpOptions.headers = httpOptions.headers.set('autorizacion', this.token);

    return this.http.get<Incidente[]>(
      this.apiUrl,
      httpOptions
    );

  }

  getMisIncidentes(): Observable<Incidente[]> {
    const httpOptions = {

      headers: new HttpHeaders({
        'Content-Type': 'application/json',
      })
    };

    httpOptions.headers = httpOptions.headers.set('autorizacion', this.token);

    return this.http.get<Incidente[]>(
      this.apiUrl,
      httpOptions
    );

  }

  getBy(id: number): Observable<Incidente> {
    const httpOptions = {

      headers: new HttpHeaders({
        'Content-Type': 'application/json',
      })
    };

    httpOptions.headers = httpOptions.headers.set('autorizacion', this.token);

    return this.http.get<Incidente>(
      this.apiUrl + '/' + id,
      httpOptions
    );

  }

  altaIncidente(incidente: Incidente): Observable<Incidente> {
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        'autorizacion': this.token
      })
    };

    return this.http.post<Incidente>(
      this.apiUrl,
      incidente,
      httpOptions
    )
  }

  modificarIncidente(incidente: Incidente): Observable<Incidente> {
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        'autorizacion': this.token
      })
    };

    return this.http.put<Incidente>(
      this.apiUrl + '/' + incidente.Id,
      incidente,
      httpOptions
    )
  }
}
