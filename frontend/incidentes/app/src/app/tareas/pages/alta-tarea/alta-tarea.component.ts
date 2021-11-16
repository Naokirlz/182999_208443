import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Proyecto } from 'src/app/interfaces/proyecto.interface';
import { ProyectoService } from 'src/app/proyectos/services/proyecto.service';
import { Tarea } from '../../../interfaces/tarea.interface';
import { TareaService } from '../../services/tarea.service';
import { MessageService } from 'primeng/api';
import { Router } from '@angular/router';
import { MenuItem } from 'primeng/api';

@Component({
  selector: 'app-alta-tarea',
  templateUrl: './alta-tarea.component.html'
})
export class AltaTareaComponent implements OnInit {

  public proyectos: Proyecto[] = [];

  constructor(private fb: FormBuilder,
    private proyectoService: ProyectoService,
    private tareasService: TareaService,
    private messageService: MessageService,
    private _router: Router) { }

  ngOnInit(): void {
    this.proyectoService.getProyecto()
      .subscribe(
        (data: Array<Proyecto>) => this.proyectos = data,
      ),
      ({ error }: any) => {
        this.messageService.add({ severity: 'error', summary: 'Error', detail: error });
      };
  }

  home: MenuItem = { icon: 'pi pi-home', routerLink: '/' };
  public items: MenuItem[] = [
    { label: 'Tareas', routerLink: '/tareas' },
    { label: 'Alta', routerLink: '/tareas/alta' },
  ];

  miFormulario: FormGroup = this.fb.group({
    nombre: [, [Validators.required, Validators.minLength(3), Validators.maxLength(25)]],
    costo: [, [Validators.required, Validators.min(0)]],
    duracion: [, [Validators.required, Validators.min(0)]],
    proyecto: [, [Validators.required]]
  })

  campoEsValido(campo: string) {
    return this.miFormulario.controls[campo].errors
      && this.miFormulario.controls[campo].touched
  }

  altaTarea(): void {
    if (this.miFormulario.invalid) {
      this.miFormulario.markAllAsTouched();
      return;
    }

    const tarea: Tarea = {
      nombre: this.miFormulario.value.nombre,
      costo: this.miFormulario.value.costo,
      duracion: this.miFormulario.value.duracion,
      proyectoId: this.miFormulario.value.proyecto,
    }

    this.tareasService.altaTareas(tarea)
      .subscribe((data: Tarea) => {
        this.messageService.add({
          severity: 'success', summary: 'Listo',
          detail: 'Tarea guardada correctamente.'
        });
        this.miFormulario.reset();
      },
        (({ error }: any) => {
          this.messageService.add({ severity: 'error', summary: 'Error', detail: error });
        }
        )
      );
  }

  volver(){
    this._router.navigate([`/tareas`]);
  }
}
