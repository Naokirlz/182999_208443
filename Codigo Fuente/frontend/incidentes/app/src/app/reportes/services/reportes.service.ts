import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ReporteBugsDesarrollador, ReporteBugsProyecto } from 'src/app/interfaces/reportes.interface';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ReportesService {

  constructor(private http: HttpClient) { }

  private apiUrl: string = `${environment.envApiUrl}/Reportes`;
  token:string = sessionStorage.getItem('token')!;

  getAll():Observable<ReporteBugsProyecto[]>{

    const httpOptions = {

      headers: new HttpHeaders({
      'Content-Type': 'application/json',
      })
    };

    httpOptions.headers = httpOptions.headers.set('autorizacion', this.token );

    return this.http.get<ReporteBugsProyecto[]>(
      this.apiUrl + '/incidentes/proyectos',
      httpOptions
    );

  }

  getby(id:number):Observable<ReporteBugsDesarrollador>{

    const httpOptions = {

      headers: new HttpHeaders({
      'Content-Type': 'application/json',
      })
    };

    httpOptions.headers = httpOptions.headers.set('autorizacion', this.token );

    return this.http.get<ReporteBugsDesarrollador>(
      this.apiUrl + '/' + id + '/incidentes',
      httpOptions
    );

}

}
