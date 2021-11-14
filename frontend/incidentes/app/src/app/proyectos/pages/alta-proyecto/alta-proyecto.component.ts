import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MessageService } from 'primeng/api';
import { Proyecto } from '../../../interfaces/proyecto.interface';
import { ProyectoService } from '../../services/proyecto.service';
import { MenuItem } from 'primeng/api';

@Component({
  selector: 'app-alta-proyecto',
  templateUrl: './alta-proyecto.component.html'
})

export class AltaProyectoComponent implements OnInit {

  constructor(private fb: FormBuilder,
    private proyectoService: ProyectoService,
    private messageService: MessageService) { }

  ngOnInit(): void { }

  home: MenuItem = { icon: 'pi pi-home', routerLink: '/' };
  public items: MenuItem[] = [
    { label: 'Proyectos', routerLink: '/proyectos' },
  ];

  miFormulario: FormGroup = this.fb.group({
    nombre: [, [Validators.required,
    Validators.minLength(5),
    Validators.maxLength(25),
    Validators.pattern("[a-zA-Z0-9 ]*")
    ]
    ],
  })

  campoEsValido(campo: string) {
    return this.miFormulario.controls[campo].errors
      && this.miFormulario.controls[campo].touched
  }

  altaProyecto() {
    if (this.miFormulario.invalid) {
      this.miFormulario.markAllAsTouched();
      return;
    }
    const proyecto: Proyecto = {
      nombre: this.miFormulario.value.nombre
    }
    this.proyectoService.alta(proyecto)
      .subscribe((data: any) => {
        this.messageService.add({
          severity: 'success', summary: 'Listo',
          detail: 'Proyecto creado correctamente.'
        });
        this.miFormulario.reset();
      },
        (({ error }: any) => {
          this.messageService.add({ severity: 'error', summary: 'Error', detail: error });
        }
        )
      );
  }
}
