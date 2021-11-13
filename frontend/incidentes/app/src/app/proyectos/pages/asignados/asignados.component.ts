import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Proyecto } from 'src/app/interfaces/proyecto.interface';
import { ProyectoService } from '../../services/proyecto.service';
import { UsuariosService } from '../../../usuarios/services/usuarios.service';
import { Usuario } from 'src/app/interfaces/dtoUsuario.interface';
import { FilterOperator, MessageService } from 'primeng/api';

@Component({
  selector: 'app-asignados',
  templateUrl: './asignados.component.html',
  styles: [
  ]
})
export class AsignadosComponent implements OnInit {


  public proyectoId: number;
  public proyectos:Proyecto[]=[];
  public usuarios:Usuario[]=[];
  public asignados:Usuario[] | undefined=[];
  

  constructor(private _route: ActivatedRoute,
              private proyectoService:ProyectoService,
              private _router: Router,
              private usuarioServie:UsuariosService,
              private messageService: MessageService) { 

    this.proyectoId = 0;

  }

  ngOnInit(): void {

    this.proyectoId = this._route.snapshot.params['proyectoId'];
    //let numeroRol : number = parseInt(this.proyectoId);
    
    this.proyectoService.getBy(this.proyectoId)
    .subscribe(
      ((data: Proyecto) => this.result(data)),
    );

  }

  eliminar(id:number){

    this.asignados = this.asignados!.filter(p => p.id !== id);

    this.usuarioServie.getBy(id)
    .subscribe(
      ((data: Usuario) => this.usuarios?.push(data)),
    );

  }
  
  private result(data: Proyecto): void {
    this.proyectos.push(data);
    this.asignados = this.proyectos[0].asignados;

    this.usuarioServie.getUsuario()
    .subscribe(
      ((data: Array<Usuario>) => this.result2(data)),
    );
    
  }

  private result2(data: Array<Usuario>): void {
   
   this.usuarios = data;
   this.asignados?.forEach((e)=>{
   this.usuarios = this.usuarios.filter(p => p.id !== e.id);
   this.usuarios = this.usuarios.filter(p => p.rolUsuario !== 0);

   })
   

  }
  
  agregar(id:number){

  this.usuarios = this.usuarios!.filter(p => p.id !== id);
 
  this.usuarioServie.getBy(id)
  .subscribe(
    ((data: Usuario) => this.asignados?.push(data)),
  );
 
  }

  actualizar(){

    this.proyectos[0].asignados=this.asignados;
    this.proyectoService.update(this.proyectos[0])
      .subscribe((data: Proyecto) => {
        this.messageService.add({
          severity: 'success', summary: 'Listo',
          detail: 'Usuario guardado correctamente.'
        });
          this.volver();
      },
        (({ error }: any) => {
          this.messageService.add({ severity: 'error', summary: 'Error', detail: error});
        }
        )
      );

    
    
    
    this.volver();

}


  volver(){

    this._router.navigate([`/proyectos`]);

  }

}
