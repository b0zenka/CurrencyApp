import { Component, HostBinding } from '@angular/core';
@Component({
  selector: 'ui-card',
  standalone: true,
  template: `<ng-content />`,
  styles: [
    `
      :host {
        display: block;
        border: 1px solid #333;
        border-radius: 12px;
        padding: 16px;
        background: #111;
      }
    `,
  ],
})
export class CardComponent {
  @HostBinding('class') cls = 'card';
}
