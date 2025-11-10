import { Component, Input } from '@angular/core';
import { RatesResponse } from '../../../../core/models/rates.model';
import { CardComponent } from '../../../../shared/ui/card.component';
@Component({
  selector: 'rates-stats',
  standalone: true,
  imports: [CardComponent],
  templateUrl: './rates-stats.component.html',
})
export class RatesStatsComponent {
  @Input() data!: RatesResponse;
}
