import {Component, OnDestroy, OnInit} from '@angular/core';
import {Employee} from "../../../../models/employee";
import {ErrorHandlerService} from "../../../shared/services/error-handler/error-handler.service";
import {ActivatedRoute} from "@angular/router";
import {EmployeeService} from "../../../shared/services/employee/employee.service";
import {Subscription} from "rxjs";

@Component({
  selector: 'app-details',
  templateUrl: './details.component.html',
  styleUrls: ['./details.component.css']
})
export class DetailsComponent implements OnInit, OnDestroy {

  employee: Employee = {};
  title: string = "ERROR MESSAGE"
  errMsg: string = "";
  id: string = "";
  subscriptions: Subscription[] = [];

  constructor(private employeeService: EmployeeService,
              private route: ActivatedRoute,
              private errorHandler: ErrorHandlerService) { }

  ngOnDestroy() {
    this.subscriptions.forEach(s => s.unsubscribe());
  }

  ngOnInit(): void {
    this.subscriptions.push(
      this.route.params.subscribe(
        value => {
          this.id = value["id"];
        }
      )
    );

    this.subscriptions.push(
      this.employeeService.getEmployee(this.id).subscribe(
        value => this.employee = value,
        error => {
          this.errorHandler.handleError(error);
          this.errMsg = this.errorHandler.errMsg;
          $("#errorModal").show();
        }
      )
    );
  }
}
