System.register(['../../../../node_modules/angular2/http', '../../../../node_modules/angular2/core'], function(exports_1) {
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
    var http_1, core_1;
    var DataService;
    return {
        setters:[
            function (http_1_1) {
                http_1 = http_1_1;
            },
            function (core_1_1) {
                core_1 = core_1_1;
            }],
        execute: function() {
            DataService = (function () {
                function DataService(http) {
                    this.http = http;
                }
                DataService.prototype.set = function (baseUri) {
                    this._baseUri = baseUri;
                };
                DataService.prototype.get = function () {
                    return this.http.get(this._baseUri)
                        .map(function (response) { return (response); });
                };
                DataService.prototype.post = function (data, mapJson) {
                    if (mapJson === void 0) { mapJson = true; }
                    if (mapJson)
                        return this.http.post(this._baseUri, data)
                            .map(function (response) { return response.json(); });
                    else
                        return this.http.post(this._baseUri, data);
                };
                DataService.prototype.delete = function (id) {
                    return this.http.delete(this._baseUri + '/' + id.toString())
                        .map(function (response) { return response.json(); });
                };
                DataService.prototype.deleteResource = function (resource) {
                    return this.http.delete(resource)
                        .map(function (response) { return response.json(); });
                };
                DataService = __decorate([
                    core_1.Injectable(), 
                    __metadata('design:paramtypes', [http_1.Http])
                ], DataService);
                return DataService;
            })();
            exports_1("DataService", DataService);
        }
    }
});
