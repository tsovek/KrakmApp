System.register(['../../../../node_modules/angular2/core', '../../../../node_modules/angular2/common', '../../../../node_modules/angular2/router', './routes', '../../core/domain/user', '../../core/domain/result', '../../core/services/membershipService', '../../core/services/notificationService'], function(exports_1) {
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
    var core_1, common_1, router_1, routes_1, user_1, result_1, membershipService_1, notificationService_1;
    var Login;
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
            },
            function (user_1_1) {
                user_1 = user_1_1;
            },
            function (result_1_1) {
                result_1 = result_1_1;
            },
            function (membershipService_1_1) {
                membershipService_1 = membershipService_1_1;
            },
            function (notificationService_1_1) {
                notificationService_1 = notificationService_1_1;
            }],
        execute: function() {
            Login = (function () {
                function Login(membershipService, notificationService, router) {
                    this.membershipService = membershipService;
                    this.notificationService = notificationService;
                    this.routes = routes_1.Routes;
                    this._user = new user_1.User('', '');
                    this.routes = routes_1.Routes;
                    this._router = router;
                }
                Login.prototype.login = function () {
                    var _this = this;
                    var _authenticationResult = new result_1.Result(false, '');
                    this.membershipService.login(this._user)
                        .subscribe(function (res) {
                        _authenticationResult.Succeeded = res.Succeeded;
                        _authenticationResult.Message = res.Message;
                    }, function (error) { return console.error('Error: ' + error); }, function () {
                        if (_authenticationResult.Succeeded) {
                            _this.notificationService.printSuccessMessage('Welcome back ' + _this._user.Username + '!');
                            localStorage.setItem('user', JSON.stringify(_this._user));
                            _this._router.navigate([_this.routes.home.name]);
                        }
                        else {
                            _this.notificationService.printErrorMessage(_authenticationResult.Message);
                        }
                    });
                };
                ;
                Login = __decorate([
                    core_1.Component({
                        selector: 'albums',
                        providers: [membershipService_1.MembershipService, notificationService_1.NotificationService],
                        templateUrl: './app/components/account/login.html',
                        bindings: [membershipService_1.MembershipService, notificationService_1.NotificationService],
                        directives: [common_1.CORE_DIRECTIVES, common_1.FORM_DIRECTIVES, router_1.RouterLink]
                    }), 
                    __metadata('design:paramtypes', [membershipService_1.MembershipService, notificationService_1.NotificationService, router_1.Router])
                ], Login);
                return Login;
            })();
            exports_1("Login", Login);
        }
    }
});
