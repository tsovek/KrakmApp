///<reference path="../../../bower_components/googlemaps-ts/last/google.maps.d.ts"/>
import { Component, OnInit } from 'angular2/core';
import { RouteParams } from 'angular2/router';
import { CORE_DIRECTIVES, FORM_DIRECTIVES} from 'angular2/common';
import { MembershipService } from '../core/services/membershipService';
import { NotificationService } from '../core/services/notificationService';
import { User } from '../core/domain/user';
import { DataService } from '../core/services/dataService';
import { Result } from '../core/domain/result';
import { Partner } from '../core/domain/partner';
import { ANGULAR2_GOOGLE_MAPS_DIRECTIVES } from 'angular2-google-maps/core';

@Component({
    selector: 'addHotel',
    providers: [MembershipService, NotificationService, DataService],
    templateUrl: './app/components/addPartner.html',
    bindings: [MembershipService, NotificationService, DataService],
    directives: [CORE_DIRECTIVES, FORM_DIRECTIVES, ANGULAR2_GOOGLE_MAPS_DIRECTIVES]
})
export class AddPartner implements OnInit {
    private _user: User;
    private _newPartner: Partner;
    private _partnersApi: string = 'api/partners/';
    private _map: google.maps.Map;
    private _title: string = 'Define new partner';

    constructor(
        public _membershipSerivce: MembershipService,
        public _notificationService: NotificationService,
        public _dataService: DataService,
        private params: RouteParams) {

        _dataService.set(this._partnersApi);
        this._newPartner = new Partner(0, '', 50.0666501, 19.9449799, '');

        var id: string = params.get('id');
        if (id !== null) {
            _dataService.set(this._partnersApi + id);
            this._title = 'Edit partner';
            this._dataService.get()
                .subscribe(res => {
                    var data: any = res.json();
                    this._newPartner = data;
                    if (this._map) {
                        this._map.setCenter(new google.maps.LatLng(
                            this._newPartner.Latitude, this._newPartner.Longitude));
                        var iconDefault = {
                            url: 'http://localhost:5000/images/marker-green.png',
                            size: new google.maps.Size(100, 100),
                            origin: new google.maps.Point(0, 0),
                            anchor: new google.maps.Point(17, 34),
                            scaledSize: new google.maps.Size(50, 50)
                        };
                        var defaultMarker = new google.maps.Marker({
                            position: new google.maps.LatLng(
                                this._newPartner.Latitude, this._newPartner.Longitude),
                            map: this._map,
                            icon: iconDefault
                        });
                    }
                }, error => console.error('Error: ' + error));
        }

        if (_membershipSerivce.isUserAuthenticated()) {
            this._user = _membershipSerivce.getLoggedInUser();
        }
    }

    getLatLng(): google.maps.LatLng {
        if (this._newPartner.Id === 0) {
            return new google.maps.LatLng(50.0666501, 19.9449799);
        }
        return new google.maps.LatLng(this._newPartner.Latitude, this._newPartner.Longitude);
    }

    ngOnInit() {
        var myLatlng: google.maps.LatLng = this.getLatLng();

        var styleArray = [
            {
                featureType: "all",
                stylers: [
                    { saturation: -80 }
                ]
            }, {
                featureType: "road.arterial",
                elementType: "geometry",
                stylers: [
                    { hue: "#00ffee" },
                    { saturation: 50 }
                ]
            }, {
                featureType: "poi.business",
                elementType: "labels",
                stylers: [
                    { visibility: "off" }
                ]
            }
        ];
        var mapOptions: any = {
            center: myLatlng,
            zoom: 17,
            mapTypeId: google.maps.MapTypeId.ROADMAP,
            styles: styleArray,
            disableDefaultUI: false,
            scaleControl: true
        };
        this._map = new google.maps.Map(document.getElementById("map"),
            mapOptions);
        var iconDefault = {
            url: 'http://localhost:5000/images/marker-green.png',
            size: new google.maps.Size(100, 100),
            origin: new google.maps.Point(0, 0),
            anchor: new google.maps.Point(17, 34),
            scaledSize: new google.maps.Size(50, 50)
        };
        var defaultMarker = new google.maps.Marker({
            position: myLatlng,
            map: this._map,
            icon: iconDefault
        });

        var input = <HTMLInputElement>document.getElementById('adress-input');
        var searchBox = new google.maps.places.SearchBox(input);
        var localThis = this;
        this._map.addListener('bounds_changed', function () {
            searchBox.setBounds(localThis._map.getBounds());
        });
        var markers = [];
        searchBox.addListener('places_changed', function () {
            var places = searchBox.getPlaces();

            if (places.length == 0) {
                return;
            }
            markers = [];
            defaultMarker.setMap(null);
            var bounds = new google.maps.LatLngBounds();
            places.forEach(function (place) {
                localThis._newPartner.Latitude = place.geometry.location.lat();
                localThis._newPartner.Longitude = place.geometry.location.lng();

                bounds.extend(place.geometry.location);
            });
            localThis._map.fitBounds(bounds);
            localThis._map.setZoom(17);
        });
    }

    addNewPartner(): void {
        var result: Result = new Result(false, '');
        this._dataService.post(JSON.stringify(this._newPartner))
            .subscribe(res => {
                result.Succeeded = res.Succeeded;
                result.Message = res.Message;
            },
                error => console.error('Error: ' + error),
                () => {
                    if (result.Succeeded) {
                        this._notificationService
                            .printSuccessMessage(
                                'Your partner: ' + this._newPartner.Name + ' is created');
                        // todo: redirect to manage hotel
                    }
                    else {
                        this._notificationService
                            .printErrorMessage(result.Message);
                    }
                });
    }
}