System.register(['../../../../node_modules/angular2/core', './dataService', '../domain/user'], function(exports_1) {
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
    var core_1, dataService_1, user_1;
    var MembershipService;
    return {
        setters:[
            function (core_1_1) {
                core_1 = core_1_1;
            },
            function (dataService_1_1) {
                dataService_1 = dataService_1_1;
            },
            function (user_1_1) {
                user_1 = user_1_1;
            }],
        execute: function() {
            MembershipService = (function () {
                function MembershipService(accountService) {
                    this.accountService = accountService;
                    this._accountRegisterAPI = 'api/account/register/';
                    this._accountLoginAPI = 'api/account/authenticate/';
                    this._accountLogoutAPI = 'api/account/logout/';
                }
                MembershipService.prototype.register = function (newUser) {
                    this.accountService.set(this._accountRegisterAPI);
                    return this.accountService.post(JSON.stringify(newUser));
                };
                MembershipService.prototype.login = function (creds) {
                    this.accountService.set(this._accountLoginAPI);
                    return this.accountService.post(JSON.stringify(creds));
                };
                MembershipService.prototype.logout = function () {
                    this.accountService.set(this._accountLogoutAPI);
                    return this.accountService.post(null, false);
                };
                MembershipService.prototype.isUserAuthenticated = function () {
                    var _user = localStorage.getItem('user');
                    if (_user != null)
                        return true;
                    else
                        return false;
                };
                MembershipService.prototype.getLoggedInUser = function () {
                    var _user;
                    if (this.isUserAuthenticated()) {
                        var _userData = JSON.parse(localStorage.getItem('user'));
                        _user = new user_1.User(_userData.Username, _userData.Password);
                    }
                    return _user;
                };
                MembershipService = __decorate([
                    core_1.Injectable(), 
                    __metadata('design:paramtypes', [dataService_1.DataService])
                ], MembershipService);
                return MembershipService;
            })();
            exports_1("MembershipService", MembershipService);
        }
    }
});
