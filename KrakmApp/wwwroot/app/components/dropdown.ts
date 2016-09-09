import { Component, Input, Output, EventEmitter } from 'angular2/core'
import { CORE_DIRECTIVES, FORM_DIRECTIVES } from 'angular2/common';
import { Hotel } from '../core/domain/hotel';
import { Client } from '../core/domain/client';

@Component({
    selector: 'my-dropdown',
    template: `
    <select class="form-control" (change)="onSelect($event.target.value)">
      <option *ngFor="#hotel of hotels" [value]="hotel.id">{{hotel.name}}</option>
    </select>
    `,
    directives: [CORE_DIRECTIVES, FORM_DIRECTIVES]
})
export class DropDownComponent {
    @Input()
    public hotels: Hotel[];
    
    @Input()
    public client: Client;

    onSelect(productId) {
        for (var i = 0; i < this.hotels.length; i++) {
            if (this.hotels[i].id == productId) {
                this.client.hotelName = this.hotels[i].name;
            }
        }
    }
}