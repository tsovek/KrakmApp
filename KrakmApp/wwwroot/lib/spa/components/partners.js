System.register(['../../../node_modules/angular2/core', '../../../node_modules/angular2/common', '../../../node_modules/angular2/router', '../core/services/dataService', '../core/services/utilityService', '../routes'], function(exports_1) {
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
    var core_1, common_1, router_1, dataService_1, utilityService_1, routes_1;
    var Partners;
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
            },
            function (utilityService_1_1) {
                utilityService_1 = utilityService_1_1;
            },
            function (routes_1_1) {
                routes_1 = routes_1_1;
            }],
        execute: function() {
            Partners = (function () {
                function Partners(partnersService, utilityService, router) {
                    this.partnersService = partnersService;
                    this.utilityService = utilityService;
                    this.router = router;
                    this._partnersAPI = 'api/partners/';
                    this.routes = routes_1.Routes;
                    this.routes = routes_1.Routes;
                    partnersService.set(this._partnersAPI);
                    this.getPartners();
                }
                Partners.prototype.getPartners = function () {
                    var _this = this;
                    this.partnersService.get()
                        .subscribe(function (res) {
                        var data = res.json();
                        _this._partners = data.Items;
                    }, function (error) {
                        if (error.status == 401) {
                            _this.utilityService.navigateToSignIn();
                        }
                        console.error('Error: ' + error);
                    });
                };
                Partners.prototype.convertDateTime = function (date) {
                    return this.utilityService.convertDateTime(date);
                };
                Partners = __decorate([
                    core_1.Component({
                        selector: 'partners',
                        templateUrl: './app/components/partners.html',
                        directives: [common_1.CORE_DIRECTIVES, common_1.FORM_DIRECTIVES, router_1.RouterLink]
                    }), 
                    __metadata('design:paramtypes', [dataService_1.DataService, utilityService_1.UtilityService, router_1.Router])
                ], Partners);
                return Partners;
            })();
            exports_1("Partners", Partners);
        }
    }
});
