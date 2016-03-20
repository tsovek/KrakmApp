﻿import { Component } from 'angular2/core';
import { CORE_DIRECTIVES, FORM_DIRECTIVES } from 'angular2/common';
import { MembershipService } from '../core/services/membershipService';
import { NotificationService } from '../core/services/notificationService';
import { User } from '../core/domain/user';
import { DataService } from '../core/services/dataService';
import { Result } from '../core/domain/result';
import { Hotel } from '../core/domain/hotel';

@Component({
    selector: 'addHotel',
    providers: [MembershipService, NotificationService, DataService],
    templateUrl: './app/components/addHotel.html',
    bindings: [MembershipService, NotificationService, DataService],
    directives: [CORE_DIRECTIVES, FORM_DIRECTIVES]
})
export class AddHotel {
    private _user: User;
    private _newHotel: Hotel;
    private _hotelApi: string = 'api/hotels/';

    constructor(
        public _membershipSerivce: MembershipService,
        public _notificationService: NotificationService,
        public _dataService: DataService) {

        _dataService.set(this._hotelApi);
        this._newHotel = new Hotel('', 0, 0, '', '', '');

        if (_membershipSerivce.isUserAuthenticated()) {
            this._user = _membershipSerivce.getLoggedInUser();
        }
    }

    addNewHotel(): void {
        var result: Result = new Result(false, '');
        this._dataService.post(this._newHotel, true)
            .subscribe(res => {
                result.Succeeded = res.Succeeded;
                result.Message = res.Message;
            },
            error => console.error('Error: ' + error),
            () => {
                if (result.Succeeded) {
                    this._notificationService
                        .printSuccessMessage(
                        'Your hotel: ' + this._newHotel.Name + ' is created');
                    // todo: redirect to manage hotel
                }
                else {
                    this._notificationService
                        .printErrorMessage(result.Message);
                }
            });
    }
}