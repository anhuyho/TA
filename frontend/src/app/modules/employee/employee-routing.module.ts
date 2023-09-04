import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { EmployeeComponent } from './employee.component';
import {AddUpdateComponent} from "./components/add-update/add-update.component";
import {DetailsComponent} from "./components/details/details.component";

const routes: Routes = [
  { path: "", component: EmployeeComponent },
  { path: "add", component: AddUpdateComponent },
  { path: "update/:id", component: AddUpdateComponent },
  { path: "details/:id", component: DetailsComponent }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class EmployeeRoutingModule { }
