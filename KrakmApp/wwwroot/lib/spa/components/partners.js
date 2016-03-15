var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") return Reflect.decorate(decorators, target, key, desc);
    switch (arguments.length) {
        case 2: return decorators.reduceRight(function(o, d) { return (d && d(o)) || o; }, target);
        case 3: return decorators.reduceRight(function(o, d) { return (d && d(target, key)), void 0; }, void 0);
        case 4: return decorators.reduceRight(function(o, d) { return (d && d(target, key, o)) || o; }, desc);
    }
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
import { Component } from '../../../node_modules/angular2/core';
import { CORE_DIRECTIVES, FORM_DIRECTIVES } from '../../../node_modules/angular2/common';
import { Router, RouterLink } from '../../../node_modules/angular2/router';
import { DataService } from '../core/services/dataService';
import { UtilityService } from '../core/services/utilityService';
import { Routes } from '../routes';
export let Partners = class {
    constructor(partnersService, utilityService, router) {
        this.partnersService = partnersService;
        this.utilityService = utilityService;
        this.router = router;
        this._partnersAPI = 'api/partners/';
        this.routes = Routes;
        this.routes = Routes;
        partnersService.set(this._partnersAPI);
        this.getPartners();
    }
    getPartners() {
        this.partnersService.get()
            .subscribe(res => {
            var data = res.json();
            this._partners = data.Items;
        }, error => {
            if (error.status == 401) {
                this.utilityService.navigateToSignIn();
            }
            console.error('Error: ' + error);
        });
    }
    convertDateTime(date) {
        return this.utilityService.convertDateTime(date);
    }
};
Partners = __decorate([
    Component({
        selector: 'partners',
        templateUrl: './app/components/partners.html',
        directives: [CORE_DIRECTIVES, FORM_DIRECTIVES, RouterLink]
    }), 
    __metadata('design:paramtypes', [DataService, UtilityService, Router])
], Partners);
