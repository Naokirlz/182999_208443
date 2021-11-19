import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Incidente } from '../../interfaces/incidente.interface';

@Injectable({
  providedIn: 'root'
})
export class TesterService {

  private apiUrl: string = `${environment.envApiUrl}/Incidentes`;
  token: string = sessionStorage.getItem('token')!;

  constructor(private http: HttpClient) { }


  delete(id: number): Observable<any> {

 
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










}
