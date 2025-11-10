import { ApplicationConfig, provideBrowserGlobalErrorListeners } from '@angular/core';

import { provideHttpClient, withInterceptors } from '@angular/common/http';
import { httpErrorInterceptor } from './core/http/http-error.interceptor';

export const appConfig: ApplicationConfig = {
  providers: [
    provideBrowserGlobalErrorListeners(),
    provideHttpClient(withInterceptors([httpErrorInterceptor]))
  ]
};
