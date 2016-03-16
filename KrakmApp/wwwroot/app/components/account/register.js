System.register(['../../../../node_modules/angular2/core', '../../../../node_modules/angular2/common', '../../../../node_modules/angular2/router', './routes', '../../core/domain/registration', '../../core/domain/result', '../../core/services/membershipService', '../../core/services/notificationService'], function(exports_1, context_1) {
    "use strict";
    var __moduleName = context_1 && context_1.id;
    var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
        var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
        if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
        else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
        return c > 3 && r && Object.defineProperty(target, key, r), r;
    };
    var __metadata = (this && this.__metadata) || function (k, v) {
        if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
    };
    var core_1, common_1, router_1, routes_1, registration_1, result_1, membershipService_1, notificationService_1;
    var Register;
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
            function (registration_1_1) {
                registration_1 = registration_1_1;
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
            Register = (function () {
                function Register(membershipService, notificationService, router) {
                    this.membershipService = membershipService;
                    this.notificationService = notificationService;
                    this.routes = routes_1.Routes;
                    this._newUser = new registration_1.Registration('', '', '');
                    this._router = router;
                    this.routes = routes_1.Routes;
                }
                Register.prototype.register = function () {
                    var _this = this;
                    var _registrationResult = new result_1.Result(false, '');
                    this.membershipService.register(this._newUser)
                        .subscribe(function (res) {
                        _registrationResult.Succeeded = res.Succeeded;
                        _registrationResult.Message = res.Message;
                    }, function (error) { return console.error('Error: ' + error); }, function () {
                        if (_registrationResult.Succeeded) {
                            _this.notificationService.printSuccessMessage('Dear ' + _this._newUser.Username + ', please login with your credentials');
                            _this._router.navigate([_this.routes.login.name]);
                        }
                        else {
                            _this.notificationService.printErrorMessage(_registrationResult.Message);
                        }
                    });
                };
                ;
                Register = __decorate([
                    core_1.Component({
                        selector: 'register',
                        providers: [membershipService_1.MembershipService, notificationService_1.NotificationService],
                        templateUrl: './app/components/account/register.html',
                        bindings: [membershipService_1.MembershipService, notificationService_1.NotificationService],
                        directives: [common_1.CORE_DIRECTIVES, common_1.FORM_DIRECTIVES, router_1.ROUTER_DIRECTIVES]
                    }), 
                    __metadata('design:paramtypes', [membershipService_1.MembershipService, notificationService_1.NotificationService, router_1.Router])
                ], Register);
                return Register;
            }());
            exports_1("Register", Register);
        }
    }
});
//# sourceMappingURL=register.js.map