import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Provider } from '../models/provider.model';
import { Currency } from '../models/currency.model';
import { RatesResponse } from '../models/rates.model';

@Injectable({ providedIn: 'root' })
export class ApiService {
  constructor(private http: HttpClient) {}

  getProviders(): Observable<Provider[]> {
    return this.http.get<Provider[]>(`/api/exchange/providers`);
  }

  getCurrencies(providerId: string): Observable<Currency[]> {
    return this.http.get<Currency[]>(`/api/exchange/currencies`, {
      params: { provider: providerId },
    });
  }

  getRates(
    providerId: string,
    src: string,
    dst: string,
    fromIso: string,
    toIso: string,
  ): Observable<RatesResponse> {
    const params = new HttpParams()
      .set('api', providerId)
      .set('source', src)
      .set('target', dst)
      .set('from', fromIso)
      .set('to', toIso);
    return this.http.get<RatesResponse>(`/api/exchange/rates`, { params });
  }
}
