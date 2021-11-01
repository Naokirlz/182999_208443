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
   
  constructor(private http: HttpClient) { }

  login(loginData: Usuario): void {
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
    });
    
    this.http.post<LoginDTO>(
      this.apiUrl,
      loginData
    ).subscribe(
      (data: any) => {
        localStorage.setItem('token', data.token);
        localStorage.setItem('authData', JSON.stringify(data));
      }
    );
          
  }

  getLoginData(): LoginDTO|undefined  {
    const localData: string|null = localStorage.getItem('authData') ;
    if (localData) {
      return JSON.parse(localData);
    }
    return undefined;
  }

  getAuthorizationToken(): string {
    return (localStorage.getItem('token') ? localStorage.getItem('token') : '') as string;
  } 
  
  logout():void{

      localStorage.setItem('token', '');
      localStorage.setItem('authData', '');
  }

  }



