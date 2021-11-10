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

  public tester:boolean = false;
  public desarrollador:boolean = false;
  
  constructor(private loginService: LoginService,
              private testerService: TesterService,
              private asociacionesService:AsociacionesService,
              private estadosService:EstadosService,
              private messageService: MessageService) {

    this.usuario = this.loginService.getLoginData()?.id!;
    this.tester = this.loginService.isTesterIn();
    this.desarrollador = this.loginService.isDesarrolladorIn();
    
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

  eliminar(id:number){

    this.testerService.delete(id)
    .subscribe(
      (data: any) => {
        sessionStorage.setItem('token', data.token);
        sessionStorage.setItem('authData', JSON.stringify(data));
        this.messageService.add({severity:'success', summary:'Service Message', detail:'Via MessageService'});
        
        window.location.reload();


        if(this.loginService.isLoggedIn())
            {
              alert('exito')
            }


      },
      (({error}:any) => {
        
        alert(error);
        console.log(JSON.stringify(error));
        
      }
      ),
      () => {}
    );
    

    
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
