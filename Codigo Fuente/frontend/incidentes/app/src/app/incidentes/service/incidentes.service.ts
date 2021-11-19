import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Incidente } from 'src/app/interfaces/incidente.interface';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class IncidentesService {

  private apiUrl: string = `${environment.envApiUrl}/Incidentes`;

  token: string = sessionStorage.getItem('token')!;

  constructor(private http: HttpClient) { }


  getMisIncidentesSearch(id_search = 0, pro_search = '', nom_search='', est_search=0): Observable<Incidente[]> {
    const httpOptions = {

      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        'autorizacion': this.token
      })
    };

    let nombreIncidente = (nom_search == '') ? '' : nom_search;
    let nombreProyecto = (pro_search == '') ? '' : pro_search;
    let estadoIncidente = (est_search == 1) ? 'Activo' : (est_search == 2) ? 'Resuelto' : '';
    let idIncidente = (id_search == 0) ? '' : id_search;

    return this.http.get<Incidente[]>(
      this.apiUrl + '/filtrado?nombreProyecto=' + nombreProyecto + '&' + 'nombreIncidente=' + nombreIncidente + '&' + 'estadoIncidente=' + estadoIncidente + '&' + 'idIncidente=' + idIncidente,
      httpOptions
    );
  }

  getIncidentes(): Observable<Incidente[]> {
    const httpOptions = {

      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        'autorizacion': this.token
      })
    };

    return this.http.get<Incidente[]>(
      this.apiUrl + '/filtrado',
      httpOptions
    );

  }

  getMisIncidentes(): Observable<Incidente[]> {
    const httpOptions = {

      headers: new HttpHeaders({
        'Content-Type': 'application/json',
      })
    };

    httpOptions.headers = httpOptions.headers.set('autorizacion', this.token);

    return this.http.get<Incidente[]>(
      this.apiUrl,
      httpOptions
    );

  }

  getBy(id: number): Observable<Incidente> {
    const httpOptions = {

      headers: new HttpHeaders({
        'Content-Type': 'application/json',
      })
    };

    httpOptions.headers = httpOptions.headers.set('autorizacion', this.token);

    return this.http.get<Incidente>(
      this.apiUrl + '/' + id,
      httpOptions
    );

  }

  altaIncidente(incidente: Incidente): Observable<Incidente> {
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        'autorizacion': this.token
      })
    };

    return this.http.post<Incidente>(
      this.apiUrl,
      incidente,
      httpOptions
    )
  }

  modificarIncidente(incidente: Incidente): Observable<Incidente> {
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        'autorizacion': this.token
      })
    };

    return this.http.put<Incidente>(
      this.apiUrl + '/' + incidente.Id,
      incidente,
      httpOptions
    )
  }
}
