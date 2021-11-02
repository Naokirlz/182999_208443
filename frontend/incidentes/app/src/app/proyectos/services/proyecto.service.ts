import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Proyecto } from '../../interfaces/proyecto.interface';

@Injectable({
  providedIn: 'root'
})
export class ProyectoService {

  constructor(private http: HttpClient) { }

  private apiUrl: string = 'http://localhost:5000/api/Proyectos';
  token:string = localStorage.getItem('token')!;


  alta(proyecto: Proyecto):void{

    const httpOptions = {

      headers: new HttpHeaders({
      'Content-Type': 'application/json',
      })
    };

    httpOptions.headers = httpOptions.headers.set('autorizacion', this.token );

    this.http.post<Proyecto>(
      this.apiUrl,
      proyecto,
      httpOptions
    ).subscribe(
      (data: any) => {
        alert('Éxito')
      },
      (({error}:any) => {
        
        alert(JSON.stringify(error));
        console.log(JSON.stringify(error));
      }
      )
    );
        
  }

  getProyecto():Observable<Proyecto[]>{
    const httpOptions = {

      headers: new HttpHeaders({
      'Content-Type': 'application/json',
      })
    };

    httpOptions.headers = httpOptions.headers.set('autorizacion', this.token );

    return this.http.get<Proyecto[]>(
      this.apiUrl,
      httpOptions
    );
        
  }




}
