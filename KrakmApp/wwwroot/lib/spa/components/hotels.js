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
import { RouterLink } from '../../../node_modules/angular2/router';
import { DataService } from '../core/services/dataService';
export let Hotels = class {
    constructor(hotelsService) {
        this.hotelsService = hotelsService;
        this._hotelsAPI = 'api/hotels/';
        hotelsService.set(this._hotelsAPI);
        this.getHotels();
    }
    getHotels() {
        this.hotelsService.get()
            .subscribe(res => {
            var data = res.json();
            this._hotels = data.Items;
        }, error => console.error('Error: ' + error));
    }
};
Hotels = __decorate([
    Component({
        selector: 'hotels',
        providers: [DataService],
        templateUrl: './app/components/hotels.html',
        bindings: [DataService],
        directives: [CORE_DIRECTIVES, FORM_DIRECTIVES, RouterLink]
    }), 
    __metadata('design:paramtypes', [DataService])
], Hotels);
