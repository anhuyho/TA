import {Component, EventEmitter, Input, Output} from '@angular/core';

@Component({
  selector: 'app-success',
  templateUrl: './success.component.html',
  styleUrls: ['./success.component.css']
})
export class SuccessComponent {

  @Input()
  header: string = "";
  @Input()
  body: string = "";

  @Output()
  onClose: EventEmitter<any> = new EventEmitter();

  close(): void {
    this.onClose.emit();
  }
}
