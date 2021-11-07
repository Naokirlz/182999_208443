import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Proyecto } from 'src/app/interfaces/proyecto.interface';

@Injectable({
  providedIn: 'root'
})
export class AsociacionesService {

  private apiUrl: string = 'http://localhost:5000/api/Asociaciones';
  token: string = localStorage.getItem('token')!;

  constructor(private http: HttpClient) { }


  getBy(id: number): Observable<Proyecto> {
    const httpOptions = {

      headers: new HttpHeaders({
        'Content-Type': 'application/json',
      })
    };

    httpOptions.headers = httpOptions.headers.set('autorizacion', this.token);

    return this.http.get<Proyecto>(
      this.apiUrl + '/' + id + '/proyectos',
      httpOptions
    );

  }


}
