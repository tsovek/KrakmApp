System.register(['../../../../node_modules/angular2/core'], function(exports_1) {
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
    var core_1;
    var NotificationService;
    return {
        setters:[
            function (core_1_1) {
                core_1 = core_1_1;
            }],
        execute: function() {
            NotificationService = (function () {
                function NotificationService() {
                    this._notifier = alertify;
                }
                NotificationService.prototype.printSuccessMessage = function (message) {
                    this._notifier.success(message);
                };
                NotificationService.prototype.printErrorMessage = function (message) {
                    this._notifier.error(message);
                };
                NotificationService.prototype.printConfirmationDialog = function (message, okCallback) {
                    this._notifier.confirm(message, function (e) {
                        if (e) {
                            okCallback();
                        }
                        else {
                        }
                    });
                };
                NotificationService = __decorate([
                    core_1.Injectable(), 
                    __metadata('design:paramtypes', [])
                ], NotificationService);
                return NotificationService;
            })();
            exports_1("NotificationService", NotificationService);
        }
    }
});
