System.register(['../../../../node_modules/angular2/core', '../../../../node_modules/angular2/router'], function(exports_1) {
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
    var core_1, router_1;
    var UtilityService;
    return {
        setters:[
            function (core_1_1) {
                core_1 = core_1_1;
            },
            function (router_1_1) {
                router_1 = router_1_1;
            }],
        execute: function() {
            UtilityService = (function () {
                function UtilityService(router) {
                    this._router = router;
                }
                UtilityService.prototype.convertDateTime = function (date) {
                    var _formattedDate = new Date(date.toString());
                    return _formattedDate.toDateString();
                };
                UtilityService.prototype.navigate = function (path) {
                    this._router.navigate([path]);
                };
                UtilityService.prototype.navigateToSignIn = function () {
                    this.navigate('/Account/Login');
                };
                UtilityService = __decorate([
                    core_1.Injectable(), 
                    __metadata('design:paramtypes', [router_1.Router])
                ], UtilityService);
                return UtilityService;
            })();
            exports_1("UtilityService", UtilityService);
        }
    }
});
