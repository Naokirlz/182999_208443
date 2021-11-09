import { Component, OnInit, Input } from '@angular/core';
import { LoginService } from 'src/app/login/services/login.service';
import { LoginDTO } from '../../interfaces/login.interface';

@Component({
  selector: 'app-sidebar',
  templateUrl: './sidebar.component.html',
  styles: [
    `
    li{
      cursor:pointer;
    }
  `
  ]
})
export class SidebarComponent implements OnInit {


  public usuario: LoginDTO;
  public rol: string;

  constructor(private loginService: LoginService) {

    this.usuario = this.loginService.getLoginData()!;
    this.rol = 'No autenticado';

  //   if(this.loginService.isLoggedIn()){

  //   if (this.usuario.rolUsuario == 0) {

  //     this.rol = 'Administrador';

  //   } else if (this.usuario.rolUsuario == 1) {

  //     this.rol = 'Tester';

  //   } else { this.rol = 'Desarrollador'; }

  // }
}
  Login: boolean = true;
  @Input() colap: boolean = false;

  ngOnInit(): void {
  }

  login() {
    this.Login = !this.Login;
  }

  logout() {
    this.loginService.logout();
  }

}
