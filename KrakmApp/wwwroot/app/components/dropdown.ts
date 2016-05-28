import { Component, Input, Output, EventEmitter } from 'angular2/core'
import { CORE_DIRECTIVES, FORM_DIRECTIVES } from 'angular2/common';

export class Product {
    id: number;
    name: string;
}

@Component({
    selector: 'my-dropdown',
    template: `
    <select class="form-control" (change)="onSelect($event.target.value)">
      <option *ngFor="#product of products" [value]="product.id">{{product.name}}</option>
    </select>
    `,
    directives: [CORE_DIRECTIVES, FORM_DIRECTIVES]
})
export class DropDownComponent {
    public products: Product[] = [
        { "id": 1, "name": "Table" },
        { "id": 2, "name": "Chair" },
        { "id": 3, "name": "Light" }
    ];
    public selectedProduct: Product = this.products[0];
    onSelect(productId) {
        this.selectedProduct = null;
        for (var i = 0; i < this.products.length; i++) {
            if (this.products[i].id == productId) {
                this.selectedProduct = this.products[i];
            }
        }
    }
}