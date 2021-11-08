import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { dtoImportaciones } from 'src/app/interfaces/dtoImportaciones.interface';

@Injectable({
  providedIn: 'root'
})
export class ImportacionesService {

  constructor(private http: HttpClient) { }

  private apiUrl: string = 'http://localhost:5000/api/Importaciones';

  getImportaciones() : Observable<dtoImportaciones[]> {
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json'
      })
    };
    
    return this.http.get<dtoImportaciones[]>(
      this.apiUrl,
      httpOptions
    );
  }

  postImportaciones(importaciones: dtoImportaciones) : Observable<dtoImportaciones> {
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json'
      })
    };
    
    return this.http.post<dtoImportaciones>(
      this.apiUrl,
      importaciones,
      httpOptions
    );
  }
}
