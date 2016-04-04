import { Component } from 'angular2/core';
import { Router } from 'angular2/router';
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
        public utilityService: UtilityService,
        private router: Router) {

        this.hotelsService.set(this._hotelsApi);
        this.getHotels();
    }

    getHotels(): void {
        this.hotelsService.get().subscribe(
            res => {
                var data: any = res.json();
                this._hotels = data;
                console.log(this._hotels);
            },
            error => console.error('Error: ' + error));
    }

    remove(hotel: Hotel): void {
        
    }

    manage(hotel: Hotel): void {
        if (hotel == null) {
            return;
        }
        this.router.navigate(['Hotels', { id: hotel.Id }]);
    }

    edit(hotel: Hotel): void {

    }
}