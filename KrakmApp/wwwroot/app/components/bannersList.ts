import { Component } from 'angular2/core';
import { Router, ROUTER_DIRECTIVES } from 'angular2/router';
import { DataService } from '../core/services/dataService';
import { UtilityService } from '../core/services/utilityService';
import { Banner } from '../core/domain/banner';
import { Routes } from '../routes';

@Component({
    selector: 'bannersList',
    templateUrl: './app/components/bannersList.html',
    directives: [ROUTER_DIRECTIVES]
})
export class BannersList {
    private _bannerApi: string = 'api/banners';
    private _banners: Array<Banner>;
    private routes = Routes;

    constructor(
        public bannersService: DataService,
        public utilityService: UtilityService) {

        this.routes = Routes;
        this.bannersService.set(this._bannerApi);
        this.getBanners();
    }

    getBanners(): void {
        this.bannersService.get().subscribe(
            res => {
                var data: any = res.json();
                this._banners = data;
            },
            error => console.error('Error: ' + error));
    }
}