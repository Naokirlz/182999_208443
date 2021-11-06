import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Usuario } from 'src/app/interfaces/dtoUsuario.interface';
import { UsuariosService } from '../../services/usuarios.service';
import { InputTextModule } from 'primeng/inputtext';
import { MessageService } from 'primeng/api';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-alta-usuario',
  templateUrl: './alta-usuario.component.html',
  styles: [
  ],
  providers: [MessageService]
})
export class AltaUsuarioComponent implements OnInit {

  constructor(private usuarioServive: UsuariosService,
    private fb: FormBuilder,
    private messageService: MessageService) {

    this.Rol = 0;

  }

  ngOnInit(): void {

    this.miFormulario.reset(
      {
        RolUsuario: 0
      }
    )
  }

  public Rol;


  miFormulario: FormGroup = this.fb.group({

    Nombre: [, [Validators.required, Validators.minLength(3)]],
    Apellido: [, Validators.required],
    Contrasenia: [, Validators.required],
    RolUsuario: [],
    Valor: [0, [Validators.min(0), Validators.max(1000)]],
    Email: [, Validators.required],
    NombreUsuario: [, Validators.required]

  })

  campoEsValido(campo: string) {

    this.Rol = parseInt(this.miFormulario.value.RolUsuario);
    return this.miFormulario.controls[campo].errors
      && this.miFormulario.controls[campo].touched
  }

  /* onReject(){
    this.miFormulario.reset(      {
      RolUsuario:1
    });
  }

  onConfirm(){
    this.altaUsuario();
  } */

  /*  prueba(){
     this.messageService.add({summary: 'Success', detail: 'Message Content'});
     this.messageService.clear();
         this.messageService.add({key: 'c', sticky: true, severity:'warn', summary:'Are you sure?', detail:'Confirm to proceed'});
   } */
  altaUsuario() {
    if (this.miFormulario.invalid) {

      this.miFormulario.markAllAsTouched();
      return;

    }

    let numeroRol: number = parseInt(this.miFormulario.value.RolUsuario);

    const usuario: Usuario = {

      Nombre: this.miFormulario.value.Nombre,
      Apellido: this.miFormulario.value.Apellido,
      Contrasenia: this.miFormulario.value.Contrasenia,
      RolUsuario: numeroRol,
      ValorHora: this.miFormulario.value.Valor,
      Email: this.miFormulario.value.Email,
      NombreUsuario: this.miFormulario.value.NombreUsuario,

    }

    if (usuario.RolUsuario === 0) { usuario.ValorHora = 0 };

    this.usuarioServive.alta(usuario)
      .subscribe((data: Usuario) => {
        this.messageService.add({
          severity: 'success', summary: 'Listo',
          detail: 'Usuario guardado correctamente.'
        });
        this.miFormulario.reset({
          RolUsuario: 0
        });
      },
        (({ error }: any) => {
          this.messageService.add({ severity: 'error', summary: 'Error', detail: error});
        }
        )
      );
  }

}
