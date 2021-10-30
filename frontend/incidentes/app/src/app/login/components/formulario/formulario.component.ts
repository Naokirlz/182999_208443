import { Component, Input, OnInit, Output,EventEmitter} from '@angular/core';
import { Usuario } from '../../interfaces/dtoUsuario.interface';
import { LoginService } from '../../services/login.service';

@Component({
  selector: 'app-formulario',
  templateUrl: './formulario.component.html',
  styles: [
  ]
})
export class FormularioComponent implements OnInit {

  @Input() nuevo: Usuario = {

    NombreUsuario:'',
    Contrasenia: ''

  }

  @Output() onLoguearse: EventEmitter<Usuario> = new EventEmitter();

  constructor(private loginService:LoginService) { }

  ngOnInit(): void {
  }

  login(){


    if (this.nuevo.NombreUsuario.trim().length === 0){return;}
    if (this.nuevo.Contrasenia.trim().length === 0){return;}

    //this.onLoguearse.emit(this.nuevo)
    this.loginService.login(this.nuevo)
      .subscribe(resp => console.log(resp.status));

    this.nuevo = { 
      NombreUsuario: "",
      Contrasenia: ""
    } 
    

  }


}
