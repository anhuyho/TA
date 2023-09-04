import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {NotFoundComponent} from "./components/not-found/not-found.component";
import {InternalServerComponent} from "./components/internal-server/internal-server.component";
import {SuccessComponent} from "./components/success/success.component";
import {ErrorComponent} from "./components/error/error.component";
import {HTTP_INTERCEPTORS, HttpClientModule} from "@angular/common/http";
import {TokenService} from "./services/interceptor/token.service";

@NgModule({
  declarations: [
    NotFoundComponent,
    InternalServerComponent,
    SuccessComponent,
    ErrorComponent
  ],
  imports: [
    CommonModule,
    HttpClientModule
  ],
  exports: [
    NotFoundComponent,
    InternalServerComponent,
    SuccessComponent,
    ErrorComponent
  ],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: TokenService,
      multi: true,
    }
  ],
})
export class SharedModule { }
