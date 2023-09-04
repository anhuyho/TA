import { NgModule } from '@angular/core';
import {CommonModule, DatePipe} from '@angular/common';

import { EmployeeRoutingModule } from './employee-routing.module';
import { EmployeeComponent } from './employee.component';
import {SharedModule} from "../shared/shared.module";
import { AddUpdateComponent } from './components/add-update/add-update.component';
import {ReactiveFormsModule} from "@angular/forms";
import { DetailsComponent } from './components/details/details.component';


@NgModule({
  declarations: [
    EmployeeComponent,
    AddUpdateComponent,
    DetailsComponent
  ],
  imports: [
    CommonModule,
    EmployeeRoutingModule,
    SharedModule,
    ReactiveFormsModule
  ],
  providers:[
    DatePipe
  ]
})
export class EmployeeModule { }
