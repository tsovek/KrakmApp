import { Component } from 'angular2/core';
import {CORE_DIRECTIVES, FORM_DIRECTIVES} from 'angular2/common';
import {Router, RouterLink} from 'angular2/router';
import { Hotel } from '../core/domain/hotel';
import { DataService } from '../core/services/dataService';
import {ANGULAR2_GOOGLE_MAPS_DIRECTIVES} from 'angular2-google-maps/core';

@Component({
    selector: 'hotels',
    providers: [DataService],
    templateUrl: './app/components/hotels.html',
    bindings: [DataService],
    directives: [CORE_DIRECTIVES, FORM_DIRECTIVES, RouterLink, ANGULAR2_GOOGLE_MAPS_DIRECTIVES]
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
                this._hotels = data;
            },
            error => console.error('Error: ' + error));
    }

    getCurrentHotelName(): string {
        if (this._hotels == null || this._hotels.length == 0) {
            return "Brak Hotelu"
        }
        return this._hotels[0].Name;
    }

    getCurrentHotelAdress(): string {
        if (this._hotels == null || this._hotels.length == 0) {
            return "Brak Hotelu"
        }
        return this._hotels[0].Adress;
    }
}