import { ApplicationConfig } from '@angular/core';
import { IConfig, provideNgxMask } from 'ngx-mask'

import { provideRouter } from '@angular/router';
import { routes } from './app.routes';
import {  provideHttpClient } from '@angular/common/http';
import { provideAnimations } from '@angular/platform-browser/animations';
import { provideToastr } from 'ngx-toastr';

export const appConfig: ApplicationConfig = {
  providers: [
    provideNgxMask(),
    provideRouter(routes),
    provideHttpClient(),
    provideAnimations(),
    provideToastr(),  
  ]
};

