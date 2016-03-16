System.register(['../../../node_modules/angular2/core', '../../../node_modules/angular2/common', '../../../node_modules/angular2/router', '../core/services/dataService', '../core/services/utilityService', '../core/services/notificationService', '../core/domain/Result'], function(exports_1) {
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
    var core_1, common_1, router_1, dataService_1, utilityService_1, notificationService_1, Result_1;
    var HotelPartners;
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
            function (notificationService_1_1) {
                notificationService_1 = notificationService_1_1;
            },
            function (Result_1_1) {
                Result_1 = Result_1_1;
            }],
        execute: function() {
            HotelPartners = (function () {
                function HotelPartners(dataService, utilityService, notificationService, routeParam) {
                    this.dataService = dataService;
                    this.utilityService = utilityService;
                    this.notificationService = notificationService;
                    this._hotelsAPI = 'api/hotels/';
                    this._partnersAPI = 'api/partners/';
                    this._routeParam = routeParam;
                    this._hotelId = this._routeParam.get('id');
                    this._hotelsAPI += this._hotelId + '/hotel/';
                    dataService.set(this._hotelsAPI);
                    this.getHotelPartners();
                }
                HotelPartners.prototype.getHotelPartners = function () {
                    var _this = this;
                    this.dataService.get()
                        .subscribe(function (res) {
                        var data = res.json();
                        _this._partners = data.Items;
                        _this._hotelTitle = "Title";
                    }, function (error) {
                        if (error.status == 401) {
                            _this.utilityService.navigateToSignIn();
                        }
                        console.error('Error: ' + error);
                    }, function () { return console.log(_this._partners); });
                };
                HotelPartners.prototype.search = function (i) {
                    this.getHotelPartners();
                };
                ;
                HotelPartners.prototype.convertDateTime = function (date) {
                    return this.utilityService.convertDateTime(date);
                };
                HotelPartners.prototype.delete = function (partner) {
                    var _this = this;
                    var _removeResult = new Result_1.Result(false, '');
                    this.notificationService.printConfirmationDialog('Are you sure you want to delete the partner?', function () {
                        _this.dataService.deleteResource(_this._partnersAPI + partner.Id)
                            .subscribe(function (res) {
                            _removeResult.Succeeded = res.Succeeded;
                            _removeResult.Message = res.Message;
                        }, function (error) { return console.error('Error: ' + error); }, function () {
                            if (_removeResult.Succeeded) {
                                _this.notificationService.printSuccessMessage(partner.Name + ' removed from hotel.');
                                _this.getHotelPartners();
                            }
                            else {
                                _this.notificationService.printErrorMessage('Failed to remove partner');
                            }
                        });
                    });
                };
                HotelPartners = __decorate([
                    core_1.Component({
                        selector: 'partners-hotel',
                        providers: [notificationService_1.NotificationService],
                        templateUrl: './app/components/hotelPartners.html',
                        bindings: [notificationService_1.NotificationService],
                        directives: [common_1.CORE_DIRECTIVES, common_1.FORM_DIRECTIVES, router_1.RouterLink]
                    }), 
                    __metadata('design:paramtypes', [dataService_1.DataService, utilityService_1.UtilityService, notificationService_1.NotificationService, router_1.RouteParams])
                ], HotelPartners);
                return HotelPartners;
            })();
            exports_1("HotelPartners", HotelPartners);
        }
    }
});
