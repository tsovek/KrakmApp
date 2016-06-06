import { Component } from 'angular2/core';
import { CORE_DIRECTIVES, FORM_DIRECTIVES } from 'angular2/common';
import { Router, RouterLink, RouteParams } from 'angular2/router';
import { Routes, APP_ROUTES } from '../../routes';
import { Result } from '../../core/domain/result';
import { Route } from '../../core/domain/route';
import { User } from '../../core/domain/user';
import { MembershipService } from '../../core/services/membershipService';
import { NotificationService } from '../../core/services/notificationService';
import { DataService } from '../../core/services/dataService';
import { UtilityService } from '../../core/services/utilityService';

@Component({
    selector: 'addRoute',
    templateUrl: './app/components/routes/addRoute.html',
    bindings: [NotificationService],
    directives: [CORE_DIRECTIVES, FORM_DIRECTIVES, RouterLink]
})
export class AddRoute {
    private _route: Route;
    private _routesApi: string = 'api/routes/';
    private routes = Routes;

    constructor(
        public _notificationService: NotificationService,
        public _dataService: DataService,
        private params: RouteParams,
        private _utility: UtilityService) {

        this._route = new Route();
        console.log(this._route);
        var id: string = params.get('id');
        if (id !== null) {
            this._dataService.set(this._routesApi + id);
            this._dataService.get().subscribe(
                res => {
                    var data: any = res.json();
                    console.log(data);
                    this._route = data;
                },
                error => console.error('Error: ' + error));
        }

        this._dataService.set(this._routesApi);
    }

    addRoute(): void {
        var result: Result = new Result(false, '');
        this._dataService.post(JSON.stringify(this._route))
            .subscribe(res => {
                result.Succeeded = res.Succeeded;
                result.Message = res.Message;
            },
                error => console.error('Error: ' + error),
                () => {
                    if (result.Succeeded) {
                        this._notificationService
                            .printSuccessMessage(
                                'Route: ' + this._route.Name + ' is created');
                    }
                    else {
                        this._notificationService
                            .printErrorMessage(result.Message);
                    }
                });
    };
}