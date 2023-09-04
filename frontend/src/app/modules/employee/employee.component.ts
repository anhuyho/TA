import {Component, OnDestroy, OnInit} from '@angular/core';
import {Employee} from "../../models/employee";
import {ErrorHandlerService} from "../shared/services/error-handler/error-handler.service";
import {EmployeeService} from "../shared/services/employee/employee.service";
import {Subscription} from "rxjs";

@Component({
  selector: 'app-employee',
  templateUrl: './employee.component.html',
  styleUrls: ['./employee.component.css']
})
export class EmployeeComponent implements OnInit, OnDestroy {

  employees: Employee[] = [];
  title: string = "";
  body: string = "";
  errMsg: string = "";
  subscriptions: Subscription[] = [];

  constructor(private employeeService: EmployeeService,
              private errorHandler: ErrorHandlerService) { }

  ngOnDestroy() {
    this.subscriptions.forEach(s => s.unsubscribe());
  }

  ngOnInit(): void {
    this.getAllUsers();
  }

  getAllUsers(): void {
    this.subscriptions.push(
      this.employeeService.getEmployees().subscribe(
        value => this.employees = value,
        err => {
          this.errorHandler.handleError(err);
          this.errMsg = this.errorHandler.errMsg;
          this.title = "ERROR MESSAGE";
          $("#errorModal").show();
        }
      )
    );
    $("#successModal").hide();
  }

  delete(id: string | undefined): void {
    this.subscriptions.push(
      this.employeeService.deleteEmployee(id!).subscribe(
        value => {
          this.title = "DELETE MESSAGE";
          this.body = "Deleting Successfully";
          $("#successModal").show();
        },
        err => {
          this.errorHandler.handleError(err);
          this.errMsg = this.errorHandler.errMsg;
          this.title = "ERROR MESSAGE";
          $("#errorModal").show();
        }
      )
    );
  }
}
