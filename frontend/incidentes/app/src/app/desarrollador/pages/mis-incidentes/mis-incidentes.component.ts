import { Component, OnInit } from '@angular/core';
import { MessageService } from 'primeng/api';
import { EstadosService } from 'src/app/estados/service/estados.service';
import { Usuario } from 'src/app/interfaces/dtoUsuario.interface';
import { Incidente } from 'src/app/interfaces/incidente.interface';
import { LoginDTO } from 'src/app/interfaces/login.interface';
import { Proyecto } from 'src/app/interfaces/proyecto.interface';
import { LoginService } from 'src/app/login/services/login.service';
import { ProyectoService } from 'src/app/proyectos/services/proyecto.service';
import { AsociacionesService } from '../../../asociaciones/service/asociaciones.service';

@Component({
  selector: 'app-mis-incidentes',
  templateUrl: './mis-incidentes.component.html',
  styles: [
  ]
})
export class MisIncidentesComponent implements OnInit {


  public usuario:number = 0;
  public usuarios:Usuario[]=[];
  public proyectos:Proyecto[]=[];
  public incidentes:Incidente[] | undefined=[];
  
  constructor(private loginService: LoginService,
              private asociacionesService:AsociacionesService,
              private estadosService:EstadosService,
              private messageService: MessageService) {

    this.usuario = this.loginService.getLoginData()?.id!;

   }

  ngOnInit(): void {

    this.usuario = this.loginService.getLoginData()?.id!;
  
    this.asociacionesService.getBy(this.usuario)
    .subscribe(
      (data: any) => {
            this.result(data);
      },
      (({error}:any) => {
        alert(error);
        console.log(JSON.stringify(error));
      }
      )
    );
   
  }

  private result(data: Array<Proyecto>): void {
            
    this.proyectos = data;
    
    
    this.proyectos.forEach(e => {
      
     e.incidentes?.forEach(i => {

      this.incidentes?.push(i);
             
     });

    });

    console.log(this.proyectos);

  }

  resolver(ide:number){
  
    const incidente:Incidente = this.incidentes?.find(i => i.id == ide)!;
    incidente.estadoIncidente = 2;
    
    this.estadosService.put(incidente)
        .subscribe((data: Incidente) => {
          this.messageService.add({
            severity: 'success', summary: 'Listo',
            detail: 'Incidente Actualizado.'
          });
            
        },
          (({ error }: any) => {
            this.messageService.add({ severity: 'error', summary: 'Error', detail: error});
          }
          )
        );


  }

}
