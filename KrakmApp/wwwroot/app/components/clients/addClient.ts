﻿import { Component } from 'angular2/core';
import { CORE_DIRECTIVES, FORM_DIRECTIVES } from 'angular2/common';
import { Router, RouterLink, RouteParams } from 'angular2/router';
import { Routes, APP_ROUTES } from '../../routes';
import { Result } from '../../core/domain/result';
import { Client } from '../../core/domain/client';
import { User } from '../../core/domain/user';
import { Hotel } from '../../core/domain/hotel';
import { MembershipService } from '../../core/services/membershipService';
import { NotificationService } from '../../core/services/notificationService';
import { DataService } from '../../core/services/dataService';
import { UtilityService } from '../../core/services/utilityService';
import { DropDownComponent } from '../../components/dropdown';

@Component({
    selector: 'addClient',
    templateUrl: './app/components/clients/addClient.html',
    bindings: [NotificationService],
    directives: [CORE_DIRECTIVES, FORM_DIRECTIVES, RouterLink, DropDownComponent]
})
export class AddClient {
    public _hotels: Hotel[] = [];
    public _selectedHotel: Hotel;
    private routes = Routes;
    private _router: Router;
    private _client: Client;
    private _user: User;
    private _clientsApi: string = 'api/clients/';

    constructor(
        public _membershipSerivce: MembershipService,
        public _notificationService: NotificationService,
        public _dataService: DataService,
        private params: RouteParams,
        private _utility: UtilityService,
        router: Router) {
        this._dataService.set('api/hotels/');
        this._dataService.get().subscribe(
            res => {
                var data: any = res.json();
                this._hotels = data;
                if (this._hotels && this._hotels.length > 0)
                {
                    this._client.hotelName = this._hotels[0].name;
                }
            },
            error => console.error('Error: ' + error));

        this._dataService.set(this._clientsApi);
        this._user = _membershipSerivce.getLoggedInUser();
        this.routes = Routes;
        this._router = router;
        this._client = new Client(0, '', new Date(), new Date(), false, '', '');
        var id: string = params.get('id');
        if (id !== null) {
            this._dataService.set(this._clientsApi + id);
            this._dataService.get().subscribe(
                res => {
                    var data: any = res.json();
                    this._client = data;
                },
                error => console.error('Error: ' + error));
            this._dataService.set(this._clientsApi);
        }
    }

    addClient(): void {
        var result: Result = new Result(false, '');
        this._dataService.post(JSON.stringify(this._client))
            .subscribe(res => {
                result.succeeded = res.succeeded;
                result.message = res.message;
            },
                error => console.error('Error: ' + error),
                () => {
                    if (result.succeeded) {
                        this._notificationService
                            .printSuccessMessage(
                                'Client: ' + this._client.name + ' is created');
                        this._utility.navigate('/Clients/List');
                    }
                    else {
                        this._notificationService
                            .printErrorMessage(result.message);
                    }
                });
    };
}