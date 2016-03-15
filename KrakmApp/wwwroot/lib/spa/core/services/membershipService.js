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
import { DataService } from './dataService';
import { User } from '../domain/user';
export let MembershipService = class {
    constructor(accountService) {
        this.accountService = accountService;
        this._accountRegisterAPI = 'api/account/register/';
        this._accountLoginAPI = 'api/account/authenticate/';
        this._accountLogoutAPI = 'api/account/logout/';
    }
    register(newUser) {
        this.accountService.set(this._accountRegisterAPI);
        return this.accountService.post(JSON.stringify(newUser));
    }
    login(creds) {
        this.accountService.set(this._accountLoginAPI);
        return this.accountService.post(JSON.stringify(creds));
    }
    logout() {
        this.accountService.set(this._accountLogoutAPI);
        return this.accountService.post(null, false);
    }
    isUserAuthenticated() {
        var _user = localStorage.getItem('user');
        if (_user != null)
            return true;
        else
            return false;
    }
    getLoggedInUser() {
        var _user;
        if (this.isUserAuthenticated()) {
            var _userData = JSON.parse(localStorage.getItem('user'));
            _user = new User(_userData.Username, _userData.Password);
        }
        return _user;
    }
};
MembershipService = __decorate([
    Injectable(), 
    __metadata('design:paramtypes', [DataService])
], MembershipService);
