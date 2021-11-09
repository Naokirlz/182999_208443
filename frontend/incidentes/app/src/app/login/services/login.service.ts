import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Usuario } from '../../interfaces/dtoUsuario.interface';
import { Observable , throwError} from 'rxjs';
import { LoginDTO } from 'src/app/interfaces/login.interface';


@Injectable({
  providedIn: 'root'
})
export class LoginService {

  private apiUrl: string = 'http://localhost:5000/api/Login';
   
  constructor(private http: HttpClient) { }

  login(loginData: Usuario): Observable<Usuario> {
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
    });
    
    return this.http.post<LoginDTO>(
      this.apiUrl,
      loginData
    )
          
  }

  ngOnDestroy(){
    //eliminar suscripcion  .unsubscribe()
  }

  getLoginData(): LoginDTO|undefined  {
    const localData: string|null = sessionStorage.getItem('authData') ;
    if (localData) {
      return JSON.parse(localData);
    }
    return undefined;
  }

  getAuthorizationToken(): string {
    return (sessionStorage.getItem('token') ? sessionStorage.getItem('token') : '') as string;
  }
  
  isLoggedIn():boolean{

    return sessionStorage.getItem('token') != null;

  }
  
  logout():void{

      sessionStorage.setItem('token', '');
      sessionStorage.setItem('authData', '');
  }

  }



