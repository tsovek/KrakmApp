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
import { Http } from '../../../../node_modules/angular2/http';
import { Injectable } from '../../../../node_modules/angular2/core';
export let DataService = class {
    constructor(http) {
        this.http = http;
    }
    set(baseUri) {
        this._baseUri = baseUri;
    }
    get() {
        return this.http.get(this._baseUri)
            .map(response => (response));
    }
    post(data, mapJson = true) {
        if (mapJson)
            return this.http.post(this._baseUri, data)
                .map(response => response.json());
        else
            return this.http.post(this._baseUri, data);
    }
    delete(id) {
        return this.http.delete(this._baseUri + '/' + id.toString())
            .map(response => response.json());
    }
    deleteResource(resource) {
        return this.http.delete(resource)
            .map(response => response.json());
    }
};
DataService = __decorate([
    Injectable(), 
    __metadata('design:paramtypes', [Http])
], DataService);
