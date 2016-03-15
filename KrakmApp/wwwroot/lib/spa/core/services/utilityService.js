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
import { Injectable } from '../../../../node_modules/angular2/core';
import { Router } from '../../../../node_modules/angular2/router';
export let UtilityService = class {
    constructor(router) {
        this._router = router;
    }
    convertDateTime(date) {
        var _formattedDate = new Date(date.toString());
        return _formattedDate.toDateString();
    }
    navigate(path) {
        this._router.navigate([path]);
    }
    navigateToSignIn() {
        this.navigate('/Account/Login');
    }
};
UtilityService = __decorate([
    Injectable(), 
    __metadata('design:paramtypes', [Router])
], UtilityService);
