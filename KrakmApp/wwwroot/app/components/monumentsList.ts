import { Component } from 'angular2/core';
import { Router, ROUTER_DIRECTIVES } from 'angular2/router';
import { DataService } from '../core/services/dataService';
import { UtilityService } from '../core/services/utilityService';
import { Monument } from '../core/domain/monument';
import { Routes } from '../routes';

@Component({
    selector: 'monumentsList',
    templateUrl: './app/components/monumentsList.html',
    directives: [ROUTER_DIRECTIVES]
})
export class MonumentsList {
    private _monumentsApi: string = 'api/monuments';
    private _monuments: Array<Monument>;
    private routes = Routes;

    constructor(
        public monumentsService: DataService,
        public utilityService: UtilityService) {

        this.routes = Routes;
        this.monumentsService.set(this._monumentsApi);
        this.getPartners();
    }

    getPartners(): void {
        this.monumentsService.get().subscribe(
            res => {
                var data: any = res.json();
                this._monuments = data;
            },
            error => console.error('Error: ' + error));
    }
}