import { Component } from 'angular2/core';
import {CORE_DIRECTIVES, FORM_DIRECTIVES} from 'angular2/common';
import {Router, RouterLink, RouteParams} from 'angular2/router';
import { Hotel } from '../core/domain/hotel';
import { DataService } from '../core/services/dataService';
import {SebmGoogleMap, SebmGoogleMapMarker, SebmGoogleMapInfoWindow} from 'angular2-google-maps/core';

@Component({
    selector: 'hotels',
    providers: [DataService],
    templateUrl: './app/components/hotels.html',
    bindings: [DataService],
    directives: [
        CORE_DIRECTIVES,
        FORM_DIRECTIVES,
        RouterLink,
        SebmGoogleMap,
        SebmGoogleMapMarker,
        SebmGoogleMapInfoWindow]
})
export class Hotels {
    private _hotelsAPI: string = 'api/hotels/';
    private _hotel: Hotel;

    constructor(
        public hotelsService: DataService,
        private params: RouteParams) {

        var id: string = params.get('id');
        hotelsService.set(this._hotelsAPI + id);
        this.getHotels();
    }

    getHotels(): void {
        this.hotelsService.get()
            .subscribe(res => {
                var data: any = res.json();
                this._hotel = data;
            },
            error => console.error('Error: ' + error));
    }
}