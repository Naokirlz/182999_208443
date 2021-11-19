import { Component, OnInit, Input } from '@angular/core';
import { Router } from '@angular/router';
import { MessageService } from 'primeng/api';
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
  public admin: boolean = false;
  public tester: boolean = false;
  public desarrollador: boolean = false;
  public logged: boolean = false;
  public user: LoginDTO | null = null;
  public users: LoginDTO[] = [];

  constructor(private loginService: LoginService,
    private messageService: MessageService,
    private _router: Router) {
    this.admin = this.user?.rolUsuario == 0;
    this.tester = this.user?.rolUsuario == 2;
    this.desarrollador = this.user?.rolUsuario == 1;
    this.logged = this.isLoogedIn();
  }

  @Input() colap: boolean = false;

  ngOnInit(): void {
    this.loginService.obtenerUser$().subscribe(usu => {
      this.users = usu;
      if (usu.length > 0) {
        this.user = usu[0];
        this.admin = this.user.rolUsuario == 0;
        this.tester = this.user.rolUsuario == 2;
        this.desarrollador = this.user.rolUsuario == 1;
      } else {
        this.user = null;
        this.admin = false;
        this.tester = false;
        this.desarrollador = false;
        this.logged = this.isLoogedIn();
      }
    });
    this.loginService.getLoginData();
  }

  isLoogedIn(): boolean {
    return this.user != null;
  }

  obtenerRol(): string {
    if (this.user) {
      if (this.user.rolUsuario == 0) {
        return "Administrador";
      } else if (this.user.rolUsuario == 1) {
        return "Desarrollador";
      } else if (this.user.rolUsuario == 2) {
        return "Tester";
      }
    }
    return "";
  }

  logout() {
    this.loginService.eliminarUsuario();
    this.loginService.logout()
      .subscribe((data: any) => {
        sessionStorage.setItem('token', '');
        sessionStorage.setItem('authData', '');
        window.location.reload();
      },
        (({ error }: any) => {
          this.messageService.add({ severity: 'error', summary: 'Error', detail: error });
        }
        )
      );
  }
}
