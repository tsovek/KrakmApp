import { Component } from '../../../node_modules/angular2/core';
import { CORE_DIRECTIVES, FORM_DIRECTIVES } from '../../../node_modules/angular2/common';
import { RouterLink, RouteParams } from '../../../node_modules/angular2/router'
import { Partner } from '../core/domain/partner';
import { DataService } from '../core/services/dataService';
import { UtilityService } from '../core/services/utilityService';
import { NotificationService } from '../core/services/notificationService';
import { Result } from '../core/domain/Result';

@Component({
    selector: 'partners-hotel',
    providers: [NotificationService],
    templateUrl: './app/components/hotelPartners.html',
    bindings: [NotificationService],
    directives: [CORE_DIRECTIVES, FORM_DIRECTIVES, RouterLink]
})
export class HotelPartners {
    private _hotelsAPI: string = 'api/hotels/';
    private _partnersAPI: string = 'api/partners/';
    private _hotelId: string;
    private _partners: Array<Partner>;
    private _routeParam: RouteParams;
    private _hotelTitle: string;

    constructor(
        public dataService: DataService,
        public utilityService: UtilityService,
        public notificationService: NotificationService,
        routeParam: RouteParams) {

        this._routeParam = routeParam;
        this._hotelId = this._routeParam.get('id');
        this._hotelsAPI += this._hotelId + '/hotel/';
        dataService.set(this._hotelsAPI);
        this.getHotelPartners();
    }

    getHotelPartners(): void {
        this.dataService.get()
            .subscribe(res => {
                var data: any = res.json();
                this._partners = data.Items;
                this._hotelTitle = "Title";
            },
            error => {
                if (error.status == 401) {
                    this.utilityService.navigateToSignIn();
                }

            console.error('Error: ' + error)
            },
            () => console.log(this._partners));
    }

    search(i): void {
        this.getHotelPartners();
    };

    convertDateTime(date: Date) {
        return this.utilityService.convertDateTime(date);
    }

    delete(partner: Partner) {
        var _removeResult: Result = new Result(false, '');

        this.notificationService.printConfirmationDialog('Are you sure you want to delete the partner?',
            () => {
                this.dataService.deleteResource(this._partnersAPI + partner.Id)
                    .subscribe(res => {
                        _removeResult.Succeeded = res.Succeeded;
                        _removeResult.Message = res.Message;
                    },
                    error => console.error('Error: ' + error),
                    () => {
                        if (_removeResult.Succeeded) {
                            this.notificationService.printSuccessMessage(partner.Name + ' removed from hotel.');
                            this.getHotelPartners();
                        }
                        else {
                            this.notificationService.printErrorMessage('Failed to remove partner');
                        }
                    });
            });
    }
}