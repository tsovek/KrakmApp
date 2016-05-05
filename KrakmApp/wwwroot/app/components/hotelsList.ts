import { Component } from 'angular2/core';
import { Router, ROUTER_DIRECTIVES } from 'angular2/router';
import { DataService } from '../core/services/dataService';
import { UtilityService } from '../core/services/utilityService';
import { Hotel } from '../core/domain/hotel';
import { Routes } from '../routes';

@Component({
    selector: 'hotelsList',
    templateUrl: './app/components/hotelsList.html',
    directives: [ROUTER_DIRECTIVES]
})
export class HotelsList {
    private _hotelsApi: string = 'api/hotels';
    private _hotels: Array<Hotel>;
    private routes = Routes;

    constructor(
        public hotelsService: DataService,
        public utilityService: UtilityService) {

        this.routes = Routes;
        this.hotelsService.set(this._hotelsApi);
        this.getHotels();
    }

    getHotels(): void {
        this.hotelsService.get().subscribe(
            res => {
                var data: any = res.json();
                this._hotels = data;
            },
            error => console.error('Error: ' + error));
    }
}