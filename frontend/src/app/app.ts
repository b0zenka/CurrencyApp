import { Component } from '@angular/core';
import { ExchangePage } from './features/exchange/exchange.page';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [ExchangePage],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App {
}
