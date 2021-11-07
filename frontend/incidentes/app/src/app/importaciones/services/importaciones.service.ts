import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ImportacionesService {

  constructor(private http: HttpClient) { }

  private apiUrl: string = 'http://localhost:5000/api/Importaciones';

  getImportaciones() {
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json'
      })
    };
    
    return this.http.get(this.apiUrl);
  }
}
