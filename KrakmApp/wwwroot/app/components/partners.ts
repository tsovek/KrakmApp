import { Component } from '../../../node_modules/angular2/core';
import { CORE_DIRECTIVES, FORM_DIRECTIVES } from '../../../node_modules/angular2/common';
import { Router, RouterLink } from '../../../node_modules/angular2/router'
import { Partner } from '../core/domain/partner';
import { DataService } from '../core/services/dataService';
import { UtilityService } from '../core/services/utilityService';
import { Routes, APP_ROUTES } from '../routes';

@Component({
    selector: 'partners',
    templateUrl: './app/components/partners.html',
    directives: [CORE_DIRECTIVES, FORM_DIRECTIVES, RouterLink]
})
export class Partners {
    private _partnersAPI: string = 'api/partners/';
    private _partners: Array<Partner>;
    private routes = Routes;

    constructor(
        public partnersService: DataService,
        public utilityService: UtilityService,
        public router: Router) {

        this.routes = Routes;
        partnersService.set(this._partnersAPI);
        this.getPartners();
    }

    getPartners(): void {
        this.partnersService.get()
            .subscribe(res => {
                var data: any = res.json();
                this._partners = data.Items;
            },
            error => {
                if (error.status == 401) {
                    this.utilityService.navigateToSignIn();
                }
                console.error('Error: ' + error);
            });
    }

    convertDateTime(date: Date) {
        return this.utilityService.convertDateTime(date);
    }
}