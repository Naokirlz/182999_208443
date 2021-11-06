import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Tarea } from '../../interfaces/tarea.interface';

@Injectable({
  providedIn: 'root'
})
export class TareaService {

  constructor(private http: HttpClient) { }

  private apiUrl: string = 'http://localhost:5000/api/Tareas';
  token: string = localStorage.getItem('token')!;

  getTareas(): Observable<Tarea[]> {
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
      })
    };

    httpOptions.headers = httpOptions.headers.set('autorizacion', this.token);

    return this.http.get<Tarea[]>(
      this.apiUrl,
      httpOptions
    );
  }

  altaTareas(tarea: Tarea): Observable<Tarea> {
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        'autorizacion': this.token
      })
    };

    return this.http.post<Tarea>(
      this.apiUrl,
      tarea,
      httpOptions
    )
  }

  deleteTarea(id: number): Observable<any> {

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

  update(tarea: Tarea): Observable<Tarea> {
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        'autorizacion': this.token
      })
    };

    return this.http.put<Tarea>(
      this.apiUrl,
      tarea,
      httpOptions
    );
  }
}
