import { HttpErrorResponse, HttpInterceptorFn } from '@angular/common/http';
import { catchError, throwError } from 'rxjs';

export const httpErrorInterceptor: HttpInterceptorFn = (req, next) =>
  next(req).pipe(
    catchError((err: unknown) => {
      if (err instanceof HttpErrorResponse) {
        if (err.status === 204) console.info('No content');
        else if (err.status === 0) console.error('Network error');
        else console.error('HTTP', err.status, err.message);
      }
      return throwError(() => err);
    }),
  );
