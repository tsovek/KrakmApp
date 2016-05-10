import { Component } from 'angular2/core';
import { Router, ROUTER_DIRECTIVES } from 'angular2/router';
import { DataService } from '../core/services/dataService';
import { UtilityService } from '../core/services/utilityService';
import { Partner } from '../core/domain/partner';
import { Routes } from '../routes';

@Component({
    selector: 'hotelsList',
    templateUrl: './app/components/partnersList.html',
    directives: [ROUTER_DIRECTIVES]
})
export class PartnersList {
    private _partnersApi: string = 'api/partners';
    private _partners: Array<Partner>;
    private routes = Routes;

    constructor(
        public partnersService: DataService,
        public utilityService: UtilityService) {

        this.routes = Routes;
        this.partnersService.set(this._partnersApi);
        this.getPartners();
    }

    getPartners(): void {
        this.partnersService.get().subscribe(
            res => {
                var data: any = res.json();
                this._partners = data;
            },
            error => console.error('Error: ' + error));
    }
}