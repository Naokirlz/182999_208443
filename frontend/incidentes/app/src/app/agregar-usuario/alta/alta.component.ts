import { Component, OnInit } from '@angular/core';
import { AltaService } from '../services/alta.service';

@Component({
  selector: 'app-alta',
  templateUrl: './alta.component.html',
  styles: [
  ]
})
export class AltaComponent implements OnInit {

  constructor(private altaService:AltaService) { }

  ngOnInit(): void {
  }

}
