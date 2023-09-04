import {Component, OnDestroy, OnInit} from '@angular/core';
import {FormBuilder, FormGroup, Validators} from "@angular/forms";
import {ActivatedRoute, Router} from "@angular/router";
import {ErrorHandlerService} from "../../../shared/services/error-handler/error-handler.service";
import {Employee} from "../../../../models/employee";
import {EmployeeService} from "../../../shared/services/employee/employee.service";
import {Subscription} from "rxjs";
import {DatePipe} from "@angular/common";

@Component({
  selector: 'app-add-update',
  templateUrl: './add-update.component.html',
  styleUrls: ['./add-update.component.css']
})
export class AddUpdateComponent implements OnInit, OnDestroy {

  employeeForm: FormGroup;
  title: string = "";
  body: string = "";
  errorMsg: string = "";
  id: string = "";
  employee: Employee = {};
  validationMessages: any = {
    "name": {
      "required": "Name is required"
    },
    "hiringDate": {
      "required": "Hiring Date is required",
      "pattern": "Hiring Date is invalid"
    },
    "salary": {
      "required": "Salary is required",
      "pattern": "Salary is invalid"
    }
  };
  formErrors: any = {
    "name": "",
    "hiringDate": "",
    "salary": ""
  };
  subscriptions: Subscription[] = [];

  constructor(private employeeService: EmployeeService,
              private formBuilder: FormBuilder,
              private route: ActivatedRoute,
              private errorHandler: ErrorHandlerService,
              private router: Router,
              private datePipe: DatePipe) {
    this.employeeForm = this.formBuilder.group({
      id: [''],
      name: ['', Validators.required],
      position: [''],
      hiringDate: ['', [Validators.required, Validators.pattern(/^\d{4}\-(0[1-9]|1[012])\-(0[1-9]|[12][0-9]|3[01])$/)]],
      salary: ['', [Validators.required, Validators.pattern(/^[0-9]*$/)]]
    });
  }

  ngOnDestroy() {
    this.subscriptions.forEach(s => s.unsubscribe());
  }

  ngOnInit(): void {
    this.subscriptions.push(
      this.route.params.subscribe(
        value => {
          this.id = value["id"];
          if (this.id === undefined) {
            this.title = "ADD EMPLOYEE";
          } else {
            this.title = "UPDATE EMPLOYEE";
            this.employeeService.getEmployee(this.id).subscribe(
              value => {
                this.employee = value;
                this.employee.hiringDate = this.datePipe.transform(this.employee.hiringDate, "yyyy-MM-dd");
                this.employeeForm.setValue(this.employee);
              },
              error => {
                this.errorHandler.handleError(error);
                this.errorMsg = this.errorHandler.errMsg;
              }
            );
          }
        }
      )
    );

    this.subscriptions.push(
      this.employeeForm.valueChanges.subscribe(
        data => this.checkValidation()
      )
    );
  }

  checkValidation(): void {
    Object.keys(this.employeeForm.controls).forEach(value => {
        this.formErrors[value] = "";
        let control = this.employeeForm.get(value);
        if (control?.invalid && control?.touched) {
          let messages = this.validationMessages[value];
          for (let errorKey in control.errors) {
            this.formErrors[value] = messages[errorKey];
          }
        }
      }
    );
  }

  submit(): void {
    if (this.employeeForm.valid) {
      this.employee = this.employeeForm.value;
      if (this.id === undefined) {
        this.subscriptions.push(
          this.employeeService.createEmployee(this.employee).subscribe(
            data => {
              this.body = "Adding successfully";
              $("#successModal").show();
            },
            error => {
              this.errorHandler.handleError(error);
              this.errorMsg = this.errorHandler.errMsg;
              this.title = "ERROR MESSAGE";
              $("#errorModal").show();
            }
          )
        );
      } else {
        this.subscriptions.push(
          this.employeeService.updateEmployee(this.id, this.employee).subscribe(
            data => {
              this.body = "Updating successfully";
              $("#successModal").show();
            },
            error => {
              this.errorHandler.handleError(error);
              this.errorMsg = this.errorHandler.errMsg;
              this.title = "ERROR MESSAGE";
              $("#errorModal").show();
            }
          )
        );
      }
    }
  }

  close(): void {
    this.router.navigate(["/employee"]);
  }
}
