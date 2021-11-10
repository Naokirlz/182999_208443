import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Incidente } from '../../interfaces/incidente.interface';

@Injectable({
  providedIn: 'root'
})
export class TesterService {

  private apiUrl: string = 'http://localhost:5000/api/Incidentes';
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
