import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, PatternValidator, Validators } from '@angular/forms';
import { Usuario } from 'src/app/interfaces/dtoUsuario.interface';
import { UsuariosService } from '../../services/usuarios.service';
import { MessageService } from 'primeng/api';

@Component({
  selector: 'app-alta-usuario',
  templateUrl: './alta-usuario.component.html',
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

  public Rol: number;
  /* private emailPattern: string = '^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+.[a-zA-Z0-9-.]+$'; */
  private emailPattern: RegExp = /^(?=.{6,30}@)[0-9a-z]+(?:\.[0-9a-z]+)*@[a-z0-9]{2,}(?:\.[a-z]{2,})+$/;
  private passwordPattern: RegExp = /^\S*$/; 

  miFormulario: FormGroup = this.fb.group({
    Nombre: [, [Validators.required, Validators.minLength(3), Validators.maxLength(25), Validators.pattern("[a-zA-Z0-9 ]*")]],
    Apellido: [, [Validators.required, Validators.minLength(3), Validators.maxLength(25), Validators.pattern("[a-zA-Z0-9 ]*")]],
    Contrasenia: [, [Validators.required, Validators.minLength(3), Validators.maxLength(25), Validators.pattern(this.passwordPattern)]],
    ContraseniaRep: [, [Validators.required, Validators.minLength(3), Validators.maxLength(25), Validators.pattern(this.passwordPattern)]],
    RolUsuario: [, Validators.required],
    Valor: [0, [Validators.min(0), Validators.max(1000)]],
    Email: [, [Validators.required, Validators.minLength(3), Validators.maxLength(25), Validators.pattern(this.emailPattern)]],
    NombreUsuario: [, [Validators.required, Validators.minLength(3), Validators.maxLength(25)]]
  })

  campoEsValido(campo: string) {
    this.Rol = parseInt(this.miFormulario.value.RolUsuario);
    return this.miFormulario.controls[campo].errors
      && this.miFormulario.controls[campo].touched
  }

  altaUsuario(): void {
    if (this.miFormulario.invalid) {
      this.miFormulario.markAllAsTouched();
      return;
    }

    if (this.miFormulario.value.Contrasenia != this.miFormulario.value.ContraseniaRep) {
      this.messageService.add({ severity: 'error', summary: 'Error', detail: 'Las contraseñas no coinciden' });
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
      .subscribe(
        (data: Usuario) => 
        {
          this.messageService.add({
            severity: 'success', summary: 'Listo',
            detail: 'Usuario guardado correctamente.'
          });
          this.miFormulario.reset({
            RolUsuario: 0
          });
       },
        (({ error }: any) => {
          this.messageService.add({ severity: 'error', summary: 'Error', detail: error });
        }
        )
      );
  }

}
