import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Usuario } from 'src/app/interfaces/dtoUsuario.interface';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})

export class UsuariosService {

  constructor(private http: HttpClient) { }

  private apiUrl: string = `${environment.envApiUrl}/Usuarios`;
  token: string = sessionStorage.getItem('token')!;

  alta(usuario: Usuario): Observable<Usuario> {
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        'autorizacion': this.token
      })
    };
    return this.http.post<Usuario>(
      this.apiUrl,
      usuario,
      httpOptions);
  }

  getUsuario(): Observable<Usuario[]> {
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        'autorizacion': this.token
      })
    };
    return this.http.get<Usuario[]>(
      this.apiUrl,
      httpOptions
    );
  }

  getBy(id: number): Observable<Usuario> {
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        'autorizacion': this.token
      })
    };
    return this.http.get<Usuario>(
      this.apiUrl + '/' + id,
      httpOptions
    );
  }
}