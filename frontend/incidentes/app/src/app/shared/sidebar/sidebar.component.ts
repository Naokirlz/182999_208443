import { Component, OnInit, Input } from '@angular/core';

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

  constructor() { }
  Login: boolean = true;
  @Input() colap: boolean = false;
  
  ngOnInit(): void {
  }

  login(){
    this.Login = !this.Login;
  }

}
