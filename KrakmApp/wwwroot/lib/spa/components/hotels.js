System.register(['../../../node_modules/angular2/core', '../../../node_modules/angular2/common', '../../../node_modules/angular2/router', '../core/services/dataService'], function(exports_1) {
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
    var core_1, common_1, router_1, dataService_1;
    var Hotels;
    return {
        setters:[
            function (core_1_1) {
                core_1 = core_1_1;
            },
            function (common_1_1) {
                common_1 = common_1_1;
            },
            function (router_1_1) {
                router_1 = router_1_1;
            },
            function (dataService_1_1) {
                dataService_1 = dataService_1_1;
            }],
        execute: function() {
            Hotels = (function () {
                function Hotels(hotelsService) {
                    this.hotelsService = hotelsService;
                    this._hotelsAPI = 'api/hotels/';
                    hotelsService.set(this._hotelsAPI);
                    this.getHotels();
                }
                Hotels.prototype.getHotels = function () {
                    var _this = this;
                    this.hotelsService.get()
                        .subscribe(function (res) {
                        var data = res.json();
                        _this._hotels = data.Items;
                    }, function (error) { return console.error('Error: ' + error); });
                };
                Hotels = __decorate([
                    core_1.Component({
                        selector: 'hotels',
                        providers: [dataService_1.DataService],
                        templateUrl: './app/components/hotels.html',
                        bindings: [dataService_1.DataService],
                        directives: [common_1.CORE_DIRECTIVES, common_1.FORM_DIRECTIVES, router_1.RouterLink]
                    }), 
                    __metadata('design:paramtypes', [dataService_1.DataService])
                ], Hotels);
                return Hotels;
            })();
            exports_1("Hotels", Hotels);
        }
    }
});
