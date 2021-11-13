import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Proyecto } from '../../interfaces/proyecto.interface';

@Injectable({
  providedIn: 'root'
})

export class ProyectoService {

  constructor(private http: HttpClient) { }

  private apiUrl: string = 'http://localhost:5000/api/Proyectos';
  token: string = sessionStorage.getItem('token')!;

  alta(proyecto: Proyecto): Observable<Proyecto> {

    const httpOptions = {

      headers: new HttpHeaders({
        'Content-Type': 'application/json',
      })
    };

    httpOptions.headers = httpOptions.headers.set('autorizacion', this.token);

    return this.http.post<Proyecto>(
      this.apiUrl,
      proyecto,
      httpOptions
    );
  }

  getProyecto(): Observable<Proyecto[]> {
    const httpOptions = {

      headers: new HttpHeaders({
        'Content-Type': 'application/json',
      })
    };

    httpOptions.headers = httpOptions.headers.set('autorizacion', this.token);

    return this.http.get<Proyecto[]>(
      this.apiUrl,
      httpOptions
    );
  }

  getBy(id: number): Observable<Proyecto> {
    const httpOptions = {

      headers: new HttpHeaders({
        'Content-Type': 'application/json',
      })
    };

    httpOptions.headers = httpOptions.headers.set('autorizacion', this.token);

    return this.http.get<Proyecto>(
      this.apiUrl + '/' + id,
      httpOptions
    );

  }

  deleteProyecto(id: number): Observable<any> {

    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        'autorizacion': this.token
      })
    };

    return this.http.delete<any>(
      this.apiUrl + '/' + id,
      httpOptions
    );
  }

  update(proyecto: Proyecto): Observable<Proyecto> {
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        'autorizacion': this.token
      })
    };

    return this.http.put<Proyecto>(
      this.apiUrl + '/' + proyecto.id,
      proyecto,
      httpOptions
    );
  }
}
