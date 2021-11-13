import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { MessageService } from 'primeng/api';
import { EstadosService } from 'src/app/estados/service/estados.service';
import { IncidentesService } from 'src/app/incidentes/service/incidentes.service';
import { Usuario } from 'src/app/interfaces/dtoUsuario.interface';
import { Incidente } from 'src/app/interfaces/incidente.interface';
import { LoginDTO } from 'src/app/interfaces/login.interface';
import { Proyecto } from 'src/app/interfaces/proyecto.interface';
import { LoginService } from 'src/app/login/services/login.service';
import { ProyectoService } from 'src/app/proyectos/services/proyecto.service';
import { AsociacionesService } from '../../../asociaciones/service/asociaciones.service';
import { TesterService } from '../../../tester/service/tester.service';

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
  public estado:string='Activo';

  public tester:boolean = false;
  public desarrollador:boolean = false;
  
  constructor(private loginService: LoginService,
              private testerService: TesterService,
              private incidenteService:IncidentesService,
              private estadosService:EstadosService,
              private messageService: MessageService,
              private _router: Router) {

    this.usuario = this.loginService.getLoginData()?.id!;
    this.tester = this.loginService.isTesterIn();
    this.desarrollador = this.loginService.isDesarrolladorIn();
    
   }

  ngOnInit(): void {

    this.usuario = this.loginService.getLoginData()?.id!;
  
    this.incidenteService.getMisIncidentes()
    .subscribe(
      (data: any) => {
            this.incidentes =data;
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

  eliminar(id:number){

    this.testerService.delete(id)
    .subscribe(
      (data: any) => {
        this.messageService.add({severity:'success', summary:'Service Message', detail:'Via MessageService'});
        alert(data);
      },
      (({error}:any) => {
        
        alert(error);
        console.log(JSON.stringify(error));
        
      }
      ),
      () => {window.location.reload();}
    );
    
  }

  obtenerEstado(id:number):string{

    if(id === 1) return 'Activo';
    return 'Resuelto'

  }

  detalle(id: number): void {

  if(this.tester){
        this._router.navigate([`/tester/incidentes/${id}`]);
  }
  else{this._router.navigate([`/desarrollador/incidentes/${id}`]);}

  }
  

  resolver(ide:number){
  
    const incidente:Incidente = this.incidentes?.find(i => i.id == ide)!;
    
    if(incidente.estadoIncidente == 1){incidente.estadoIncidente = 2}
    else{incidente.estadoIncidente = 1 }

    const aModificar: Incidente = {
      Nombre: incidente.nombre,
      Descripcion: incidente.descripcion,
      Version: incidente.version,
      Duracion: incidente.duracion,
      Id: incidente.id,
      //UsuarioId: incidente.desarrolladorId,
      ProyectoId: incidente.proyectoId,
      EstadoIncidente: incidente.estadoIncidente,
      DesarrolladorId:this.usuario
      
    }
    
    
    this.estadosService.put(aModificar)
        .subscribe((data: Incidente) => {
          this.messageService.add({
            severity: 'success', summary: 'Listo',
            detail: 'Incidente Actualizado.'
          });
          //alert('exito');
          //window.location.reload();
            
        },
          (({ error }: any) => {
            this.messageService.add({ severity: 'error', summary: 'Error', detail: error});
          }
          )
        );


  }

}
