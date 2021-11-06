import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Proyecto } from 'src/app/interfaces/proyecto.interface';
import { ProyectoService } from '../../services/proyecto.service';

@Component({
  selector: 'app-editar-proyecto',
  templateUrl: './editar-proyecto.component.html',
  styles: [
  ]
})
export class EditarProyectoComponent implements OnInit {

  public proyectoId: number;
  public proyectos:Proyecto[]=[];

  constructor(private fb: FormBuilder,
             private _route: ActivatedRoute,
             private proyectoService:ProyectoService,
             private _router: Router) { 

    this.proyectoId = 0;

  }

  ngOnInit(): void {

    this.proyectoId = this._route.snapshot.params['proyectoId'];
    
    this.proyectoService.getBy(this.proyectoId)
    .subscribe(
      ((data: Proyecto) => this.result(data)),
    );
      

  }

  private result(data: Proyecto): void {
    this.proyectos.push(data);
       
  }

  miFormulario:FormGroup = this.fb.group({

    nombre       :[, [Validators.required, Validators.minLength(3)] ],

  })

  campoEsValido(campo:string){

    return this.miFormulario.controls[campo].errors  
           && this.miFormulario.controls[campo].touched

  }

  editarProyecto(){

    if(this.miFormulario.invalid){
    
      this.miFormulario.markAllAsTouched();
      return;
    }
    console.log(this.miFormulario.value);

    this.proyectos[0].nombre = this.miFormulario.value.nombre;

    this.proyectoService.update(this.proyectos[0]);

    this.miFormulario.reset(      {
      RolUsuario:1
    });

    this._router.navigate([`/proyectos`]);

  }

  volver(){

    this._router.navigate([`/proyectos`]);

  }



}
