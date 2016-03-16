System.register(['../../../../node_modules/angular2/core', '../../../../node_modules/angular2/common', '../../../../node_modules/angular2/router', './routes'], function(exports_1) {
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
    var core_1, common_1, router_1, routes_1;
    var Account;
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
            function (routes_1_1) {
                routes_1 = routes_1_1;
            }],
        execute: function() {
            Account = (function () {
                function Account() {
                }
                Account = __decorate([
                    core_1.Component({
                        selector: 'account',
                        templateUrl: './app/components/account/account.html',
                        directives: [router_1.ROUTER_DIRECTIVES, common_1.CORE_DIRECTIVES, common_1.FORM_DIRECTIVES]
                    }),
                    router_1.RouteConfig(routes_1.APP_ROUTES), 
                    __metadata('design:paramtypes', [])
                ], Account);
                return Account;
            })();
            exports_1("Account", Account);
        }
    }
});
