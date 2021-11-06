import { Component, OnInit, Input } from '@angular/core';
import { LoginService } from 'src/app/login/services/login.service';

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

  constructor(private loginService:LoginService) { }
  Login: boolean = true;
  @Input() colap: boolean = false;
  
  ngOnInit(): void {
  }

  login(){
    this.Login = !this.Login;
  }

  logout(){
    this.loginService.logout();
  }

}
