import {Component, Input} from '@angular/core';

@Component({
  selector: 'app-error',
  templateUrl: './error.component.html',
  styleUrls: ['./error.component.css']
})
export class ErrorComponent {

  @Input()
  header: string = "";
  @Input()
  body: string = "";

  close(): void {
    $("#errorModal").hide();
  }
}
