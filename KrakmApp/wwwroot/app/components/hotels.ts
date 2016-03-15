import { Component } from '../../../node_modules/angular2/core';
import {CORE_DIRECTIVES, FORM_DIRECTIVES} from '../../../node_modules/angular2/common';
import {Router, RouterLink} from '../../../node_modules/angular2/router';
import { Hotel } from '../core/domain/hotel';
import { DataService } from '../core/services/dataService';

@Component({
    selector: 'hotels',
    providers: [DataService],
    templateUrl: './app/components/hotels.html',
    bindings: [DataService],
    directives: [CORE_DIRECTIVES, FORM_DIRECTIVES, RouterLink]
})
export class Hotels {
    private _hotelsAPI: string = 'api/hotels/';
    private _hotels: Array<Hotel>;

    constructor(public hotelsService: DataService) {

        hotelsService.set(this._hotelsAPI);
        this.getHotels();
    }

    getHotels(): void {
        this.hotelsService.get()
            .subscribe(res => {
                var data: any = res.json();
                this._hotels = data.Items;
            },
            error => console.error('Error: ' + error));
    }
}