import { Component, signal } from '@angular/core';
import { RatesResponse } from '../../core/models/rates.model';
import {
  CurrencyFormComponent,
  FormValue,
} from './components/currency-form/currency-form.component';
import { RatesStatsComponent } from './components/rates-stats/rates-stats.component';
import { RatesTableComponent } from './components/rates-stats/rates-table.component';
import { ApiService } from '../../core/http/api.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-exchange',
  standalone: true,
  imports: [CurrencyFormComponent, RatesStatsComponent, RatesTableComponent, CommonModule],
  templateUrl: './exchange.page.html',
  styleUrls: ['./exchange.page.scss'],
})
export class ExchangePage {
  loading = signal(false);
  result = signal<RatesResponse | null>(null);

  constructor(private api: ApiService) {}

  onSubmit(v: FormValue) {
    this.loading.set(true);
    this.api.getRates(v.providerId, v.source, v.target, v.from, v.to).subscribe({
      next: (r) => {
        this.result.set(r);
        this.loading.set(false);
      },
      error: (_) => {
        this.result.set(null);
        this.loading.set(false);
      },
    });
  }
}
