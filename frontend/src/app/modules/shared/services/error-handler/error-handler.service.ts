import { Injectable } from '@angular/core';
import {Router} from "@angular/router";
import {HttpErrorResponse} from "@angular/common/http";

@Injectable({
  providedIn: 'root'
})
export class ErrorHandlerService {

  errMsg: string = "";

  constructor(private router: Router) { }

  public handleError (error: HttpErrorResponse) {
    switch (error.status) {
      case 500:
        this.errMsg = error.statusText;
        this.router.navigate(["/500"])
        break;
      case 404:
        this.errMsg = error.statusText;
        this.router.navigate(["/404"])
        break;
      default:
        this.errMsg = error.statusText;
        break;
    }
  }
}
