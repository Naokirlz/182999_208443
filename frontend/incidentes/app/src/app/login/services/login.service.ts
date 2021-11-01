import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Usuario } from '../interfaces/dtoUsuario.interface';
import { Observable } from 'rxjs';
import { LoginDTO } from '../interfaces/login.interface';

@Injectable({
  providedIn: 'root'
})
export class LoginService {

  private apiUrl: string = 'http://localhost:5000/api/Login';
  private header: {} = {"headers":'43FGZ5Ad&zTVc45pXnS5PoM!TrQsWCi2'};
  
  private uri = '/api/Login';
  resource:string = '';
   
  constructor(private http: HttpClient) { }

  // login(usuario: Usuario){

  //   this.resource= "/Login"
  //   const url = `${this.apiUrl}${this.resource}`


  //   return this.http.post<Observable<string>>(url,usuario)
  //   .subscribe(
  //     (data: any) => {
  //       this.respuesta = JSON.stringify(data.token);
  //     }
  //   );

  // }

  login(loginData: Usuario): Observable<LoginDTO> {
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
    });
    
    return this.http.post<LoginDTO>(
      this.apiUrl,
      loginData,
      
    )

        
          
  }
  

  }



