import { Injectable } from '@angular/core';
import { HttpClient, HttpResponse } from '@angular/common/http';
import { Usuario } from '../interfaces/dtoUsuario.interface';

@Injectable({
  providedIn: 'root'
})
export class LoginService {

  private apiUrl: string = 'http://localhost:5000/api';
  private header: {} = {"headers":'43FGZ5Ad&zTVc45pXnS5PoM!TrQsWCi2'};

  resource:string = '';
   
  constructor(private http: HttpClient) { }

  login(usuario: Usuario){

    this.resource= "/Login"
    const url = `${this.apiUrl}${this.resource}`

    return this.http.post<HttpResponse<string>>(url,usuario);

  }


}
