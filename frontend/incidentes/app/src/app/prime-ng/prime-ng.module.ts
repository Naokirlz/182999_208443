import { NgModule } from '@angular/core';
import { MessageService } from 'primeng/api';

import {ButtonModule} from 'primeng/button';
import {CardModule} from 'primeng/card';
import {MenubarModule} from 'primeng/menubar';
import {TableModule} from 'primeng/table';
import {ToastModule} from 'primeng/toast';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { BrowserModule } from '@angular/platform-browser';
import {DataViewModule} from 'primeng/dataview';
import {BreadcrumbModule} from 'primeng/breadcrumb';



@NgModule({
  declarations: [],
  imports: [
    BrowserAnimationsModule,
    BrowserModule,
    BreadcrumbModule
  ],
  exports: [
    ButtonModule,
    CardModule,
    MenubarModule,
    TableModule,
    ToastModule,
    DataViewModule,
    BreadcrumbModule
  ],
  providers:[MessageService]

})
export class PrimeNgModule { }
