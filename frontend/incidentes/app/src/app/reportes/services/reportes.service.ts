import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ReporteBugsProyecto } from 'src/app/interfaces/reportes.interface';

@Injectable({
  providedIn: 'root'
})
export class ReportesService {

  constructor(private http: HttpClient) { }

  private apiUrl: string = 'http://localhost:5000/api/Reportes';
  token:string = localStorage.getItem('token')!;

  getAll():Observable<ReporteBugsProyecto[]>{

    const httpOptions = {

      headers: new HttpHeaders({
      'Content-Type': 'application/json',
      })
    };

    httpOptions.headers = httpOptions.headers.set('autorizacion', this.token );

    return this.http.get<ReporteBugsProyecto[]>(
      this.apiUrl,
      httpOptions
    );
       


  }

}
