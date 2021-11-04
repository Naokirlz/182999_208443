import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Usuario } from 'src/app/interfaces/dtoUsuario.interface';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UsuariosService {

  constructor(private http: HttpClient) { }

  private apiUrl: string = 'http://localhost:5000/api/Usuarios';
  token:string = localStorage.getItem('token')!;

  
  alta(usuario: Usuario):boolean{

    const httpOptions = {

      headers: new HttpHeaders({
      'Content-Type': 'application/json',
      })
    };

    httpOptions.headers = httpOptions.headers.set('autorizacion', this.token );
    let correcto : boolean = false;
    this.http.post<Usuario>(
      this.apiUrl,
      usuario,
      httpOptions
    ).subscribe(
      (data: any) => {
        alert('Éxito')
        correcto= true;
      },
      (({error}:any) => {
        alert(JSON.stringify(error));
        console.log(JSON.stringify(error));
      }
      )
    );
    return correcto;
        
  }

  getUsuario():Observable<Usuario[]>{
    const httpOptions = {

      headers: new HttpHeaders({
      'Content-Type': 'application/json',
      })
    };

    httpOptions.headers = httpOptions.headers.set('autorizacion', this.token );

    return this.http.get<Usuario[]>(
      this.apiUrl,
      httpOptions
    );
        
  }

  

  


}
