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


  
  public admin:boolean = false;
  public tester:boolean = false;
  public desarrollador:boolean = false;
  public logged:boolean = false;

  constructor(private loginService: LoginService) {

    this.admin = this.loginService.isAdminLoggedIn();
    this.tester = this.loginService.isTesterIn();
    this.desarrollador = this.loginService.isDesarrolladorIn();
    this.logged = this.loginService.isLoggedIn();

}
  Login: boolean = true;
  @Input() colap: boolean = false;

  ngOnInit(): void {
  }

  login() {
    this.Login = !this.Login;
  }

  logout(){

    this.loginService.logout()
    .subscribe((data: any) => {
      console.log(data);
      sessionStorage.setItem('token', '');
      sessionStorage.setItem('authData', '');
      window.location.reload();
    },
      (({ error }: any) => {
        console.log(error);
      }
      )
    );
  }

}
