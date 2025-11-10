import { Component, EventEmitter, OnInit, Output, signal } from '@angular/core';
import { ApiService } from '../../../../core/http/api.service';
import { CardComponent } from '../../../../shared/ui/card.component';
import { CommonModule } from '@angular/common';

export interface FormValue {
  providerId: string;
  source: string;
  target: string;
  from: string;
  to: string;
}
function todayIso() {
  const d = new Date();
  const m = String(d.getMonth() + 1).padStart(2, '0');
  const day = String(d.getDate()).padStart(2, '0');
  return `${d.getFullYear()}-${m}-${day}`;
}

@Component({
  selector: 'currency-form',
  standalone: true,
  imports: [CardComponent, CommonModule],
  templateUrl: './currency-form.component.html',
  styleUrls: ['./currency-form.component.scss'],
})
export class CurrencyFormComponent implements OnInit {
  @Output() submitForm = new EventEmitter<FormValue>();

  providers: { key: string; name: string }[] = [];
  providerId = signal('');

  currencies: { code: string; name: string; }[] = [];
  source = signal('');
  target = signal('');
  from = signal(todayIso());
  to = signal(todayIso());

  constructor(private api: ApiService) {}

  ngOnInit() {
    this.api.getProviders().subscribe({
      next: (p) => {
        this.providers = p;
        this.providerId.set(p[0]?.key ?? '');
        if (this.providerId()) this.loadCurrencies();
      },
      error: (_) => {
        this.providers = [];
      },
    });
  }

  loadCurrencies() {
    if (!this.providerId()) return;
    this.api.getCurrencies(this.providerId()).subscribe({
      next: (res) => {
        this.currencies = res;
      },
      error: (_) => {
        this.currencies = [];
      },
    });
  }

  submit() {
    if (!this.providerId() || !this.source() || !this.target()) return;
    this.submitForm.emit({
      providerId: this.providerId(),
      source: this.source(),
      target: this.target(),
      from: this.from(),
      to: this.to(),
    });
  }
}
