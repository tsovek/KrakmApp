import { Component } from 'angular2/core';
import { CORE_DIRECTIVES, FORM_DIRECTIVES } from 'angular2/common';
import { Router, RouterLink, RouteParams } from 'angular2/router';
import { Routes, APP_ROUTES } from '../routes';
import { Result } from '../core/domain/result';
import { Banner } from '../core/domain/banner';
import { User } from '../core/domain/user';
import { MembershipService } from '../core/services/membershipService';
import { NotificationService } from '../core/services/notificationService';
import { DataService } from '../core/services/dataService';
import { UtilityService } from '../core/services/utilityService';

@Component({
    selector: 'addBanner',
    templateUrl: './app/components/addBanner.html',
    bindings: [NotificationService],
    directives: [CORE_DIRECTIVES, FORM_DIRECTIVES, RouterLink]
})
export class AddBanner {
    private routes = Routes;
    private _router: Router;
    private _banner: Banner;
    private _user: User;
    private _bannersApi: string = 'api/banners/';

    constructor(
        public _membershipSerivce: MembershipService,
        public _notificationService: NotificationService,
        public _dataService: DataService,
        private params: RouteParams,
        private _utility: UtilityService,
        router: Router) {

        this._dataService.set(this._bannersApi);
        this._user = _membershipSerivce.getLoggedInUser();
        this.routes = Routes;
        this._router = router;
        this._banner = new Banner(0, '', '', new Date(), new Date(), '');
        var id: string = params.get('id');
        if (id !== null) {
            this._dataService.set(this._bannersApi + id);
            this._dataService.get().subscribe(
                res => {
                    var data: any = res.json();
                    console.log(data);
                    this._banner = data;
                },
                error => console.error('Error: ' + error));
            this._dataService.set(this._bannersApi);
        }
    }

    addBanner(): void {
        var result: Result = new Result(false, '');
        this._dataService.post(JSON.stringify(this._banner))
            .subscribe(res => {
                result.succeeded = res.succeeded;
                result.message = res.message;
            },
                error => console.error('Error: ' + error),
                () => {
                    if (result.succeeded) {
                        this._notificationService
                            .printSuccessMessage(
                                'Banner: ' + this._banner.name + ' is created');
                        this._utility.navigate('/Banners');
                    }
                    else {
                        this._notificationService
                            .printErrorMessage(result.message);
                    }
                });
    };
}