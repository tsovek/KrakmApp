import { Component } from 'angular2/core';
import { DataService } from '../core/services/dataService';
import { UtilityService } from '../core/services/utilityService';
import { Hotel } from '../core/domain/hotel';

@Component({
    selector: 'hotelsList',
    templateUrl: './app/components/hotelsList.html'
})
export class HotelsList {
    private _hotelsApi: string = 'api/hotels';
    private _hotels: Array<Hotel>;

    constructor(
        public hotelsService: DataService,
        public utilityService: UtilityService) {

        this.hotelsService.set(this._hotelsApi);
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