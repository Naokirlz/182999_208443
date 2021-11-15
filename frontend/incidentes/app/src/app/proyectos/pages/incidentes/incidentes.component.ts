import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Proyecto } from 'src/app/interfaces/proyecto.interface';
import { Incidente } from '../../../interfaces/incidente.interface';
import { ProyectoService } from '../../services/proyecto.service';
import { Usuario } from 'src/app/interfaces/dtoUsuario.interface';
import { UsuariosService } from 'src/app/usuarios/services/usuarios.service';
import { LoginService } from 'src/app/login/services/login.service';
import { MenuItem } from 'primeng/api';

@Component({
  selector: 'app-incidentes',
  templateUrl: './incidentes.component.html',
  styles: [
  ]
})
export class IncidentesPComponent implements OnInit {

  public proyectoId: number;

  public proyectos:Proyecto[]=[];
  public incidentes:Incidente[] | undefined=[];
  public usuarios:Usuario[]=[];
  public totalHoras:number=0;

  public admin:boolean = false;
  public tester:boolean = false; 
  public desarrollador:boolean = false;  
  
  constructor(private proyectoService:ProyectoService,
              private estadosService:EstadosService,
              private _route: ActivatedRoute,
              private _router: Router,
              private usuarioServie:UsuariosService,
              private messageService: MessageService,
              private loginService: LoginService) 
              
              { 

              this.proyectoId = 0;
              this.admin = this.loginService.isAdminLoggedIn();
              this.tester = this.loginService.isTesterIn();
              this.desarrollador = this.loginService.isDesarrolladorIn();

              }

  
  
  
  



  ngOnInit(): void {

    this.proyectoId = this._route.snapshot.params['proyectoId'];

    this.proyectoService.getBy(this.proyectoId)
      .subscribe(
        ((data: Proyecto) => this.result(data)),
      );

    this.usuarioServie.getUsuario()
      .subscribe(
        ((data: Array<Usuario>) => this.usuarios = data),
      );
  }

  home: MenuItem = { icon: 'pi pi-home', routerLink: '/' };
  public items: MenuItem[] = [
    { label: 'Proyectos', routerLink: '/proyectos' },
    { label: 'Incidentes' },
  ];

  private result(data: Proyecto): void {
    this.proyectos.push(data);
    this.incidentes = this.proyectos[0].incidentes;
    this.obtenerTotalHoras();
  }

  obtenerEstado(id: number): string {
    if (id === 1) return 'Activo';
    return 'Resuelto'
  }

  obtenerNombre(id: number): string {
    const usuario = this.usuarios.find(usuarios => usuarios.id === id);
    return (usuario?.nombreUsuario) ? usuario.nombreUsuario : '';
  }

  obtenerTotalHoras():void {
    this.incidentes?.forEach(
      inc => this.totalHoras = (inc.duracion! + this.totalHoras)
    )
  }

  volver():void {

    if(this.admin){this._router.navigate([`/proyectos`]);}
    if(this.tester){this._router.navigate([`/tester`]);}
    if(this.desarrollador){this._router.navigate([`/desarrollador/proyectos`]);}
    
  }

}
