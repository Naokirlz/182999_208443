import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Usuario } from '../../interfaces/dtoUsuario.interface';
import { Observable, Subject, throwError } from 'rxjs';
import { LoginDTO } from 'src/app/interfaces/login.interface';


@Injectable({
  providedIn: 'root'
})
export class LoginService {

  private apiUrl: string = 'http://localhost:5000/api/Autenticaciones';
  private user: LoginDTO[];
  private user$: Subject<LoginDTO[]>;

  constructor(private http: HttpClient) {
    this.user = []; 
    this.user$ = new Subject();
  }

  actualizarUsuario(usu: LoginDTO){
    while(this.user.length > 0){
      this.user.pop();
    }
    this.user.push(usu);
    this.user$.next(this.user);
  }

  eliminarUsuario(){
    while(this.user.length > 0){
      this.user.pop();
    }
    this.user$.next(this.user);
  }

  obtenerUser$(): Observable<LoginDTO[]>{
    return this.user$.asObservable();
  }

  login(loginData: Usuario): Observable<Usuario> {
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
    });

    return this.http.post<LoginDTO>(
      this.apiUrl,
      loginData
    )
  }

  getLoginData(): LoginDTO | undefined {
    const localData: string | null = sessionStorage.getItem('authData');
    if (localData) {
      let userLocal:LoginDTO = JSON.parse(localData);
      this.actualizarUsuario(userLocal);
      return JSON.parse(localData);
    }
    return undefined;
  }

  getAuthorizationToken(): string {
    return (sessionStorage.getItem('token') ? sessionStorage.getItem('token') : '') as string;
  }

  isLoggedIn(): boolean {
    const token = sessionStorage.getItem('token');
    if(!token){return false;}
    return (token != '') ;
  }

  isAdminLoggedIn(): boolean {
    return this.getLoginData()?.rolUsuario == 0;
  }

  isDesarrolladorIn(): boolean {
    return this.getLoginData()?.rolUsuario == 1;
  }

  isTesterIn(): boolean {
    return this.getLoginData()?.rolUsuario == 2;
  }

  logout(): Observable<any> {
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        'autorizacion': this.getAuthorizationToken()
      })
    };
    let id : any = this.getLoginData()?.id;
    return this.http.delete<any>(
      this.apiUrl + '/' + id,
      httpOptions
    );
  }
}



