import { Component, Input } from '@angular/core';
import { RatesResponse } from '../../../../core/models/rates.model';
import { CommonModule } from '@angular/common';
import { CardComponent } from '../../../../shared/ui/card.component';
@Component({
  selector: 'rates-table',
  standalone: true,
  imports: [CommonModule, CardComponent],
  templateUrl: './rates-table.component.html',
})
export class RatesTableComponent {
  @Input() data!: RatesResponse;
}
