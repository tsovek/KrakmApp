import { Component } from 'angular2/core';
import { Router, ROUTER_DIRECTIVES } from 'angular2/router';
import { DataService } from '../core/services/dataService';
import { UtilityService } from '../core/services/utilityService';
import { Entertainment } from '../core/domain/entertainment';
import { Routes } from '../routes';

@Component({
    selector: 'entertainmentsList',
    templateUrl: './app/components/entertainmentsList.html',
    directives: [ROUTER_DIRECTIVES]
})
export class EntertainmentsList {
    private _entertainmentsApi: string = 'api/entertainment';
    private _entertainments: Array<Entertainment>;
    private routes = Routes;

    constructor(
        public entertainmentService: DataService,
        public utilityService: UtilityService) {

        this.routes = Routes;
        this.entertainmentService.set(this._entertainmentsApi);
        this.getPartners();
    }

    getPartners(): void {
        this.entertainmentService.get().subscribe(
            res => {
                var data: any = res.json();
                this._entertainments = data;
            },
            error => console.error('Error: ' + error));
    }
}