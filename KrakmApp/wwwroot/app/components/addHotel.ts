///<reference path="../../../bower_components/googlemaps-ts/last/google.maps.d.ts"/>
import { Component, OnInit } from 'angular2/core';
import { RouteParams } from 'angular2/router';
import { CORE_DIRECTIVES, FORM_DIRECTIVES} from 'angular2/common';
import { MembershipService } from '../core/services/membershipService';
import { NotificationService } from '../core/services/notificationService';
import { User } from '../core/domain/user';
import { DataService } from '../core/services/dataService';
import { Result } from '../core/domain/result';
import { Hotel } from '../core/domain/hotel';
import { ANGULAR2_GOOGLE_MAPS_DIRECTIVES } from 'angular2-google-maps/core';

@Component({
    selector: 'addHotel',
    providers: [MembershipService, NotificationService, DataService],
    templateUrl: './app/components/addHotel.html',
    bindings: [MembershipService, NotificationService, DataService],
    directives: [CORE_DIRECTIVES, FORM_DIRECTIVES, ANGULAR2_GOOGLE_MAPS_DIRECTIVES]
})
export class AddHotel implements OnInit {
    private _user: User;
    private _newHotel: Hotel;
    private _hotelApi: string = 'api/hotels/';
    private _map: google.maps.Map;
    private _title: string = 'Define new hotel';

    constructor(
        public _membershipSerivce: MembershipService,
        public _notificationService: NotificationService,
        public _dataService: DataService,
        private params: RouteParams) {

        _dataService.set(this._hotelApi);
        this._newHotel = new Hotel(0, '', 0, 0, '', '', '');

        var id: string = params.get('id');
        if (id !== null) {
            _dataService.set(this._hotelApi + id);
            this._title = 'Edit hotel';
            this._dataService.get()
                .subscribe(res => {
                    var data: any = res.json();
                    this._newHotel = data;
                    if (this._map)
                    {
                        this._map.setCenter(new google.maps.LatLng(
                            this._newHotel.latitude, this._newHotel.longitude));
                        var iconDefault = {
                            url: 'http://localhost:5000/images/marker-green.png',
                            size: new google.maps.Size(100, 100),
                            origin: new google.maps.Point(0, 0),
                            anchor: new google.maps.Point(17, 34),
                            scaledSize: new google.maps.Size(50, 50)
                        };
                        var defaultMarker = new google.maps.Marker({
                            position: new google.maps.LatLng(
                                this._newHotel.latitude, this._newHotel.longitude),
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

    getLatLng(): google.maps.LatLng{
        if (this._newHotel.id === 0) {
            return new google.maps.LatLng(50.0666501, 19.9449799);
        }
        return new google.maps.LatLng(this._newHotel.latitude, this._newHotel.longitude);
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
            mapTypeId: google.maps.MapTypeId.ROADMAP,
            styles: styleArray,
            disableDefaultUI: false,
            scaleControl: true
        };
        this._map = new google.maps.Map(document.getElementById("map"),
            mapOptions);

        this._map.setZoom(12);

        var input = <HTMLInputElement>document.getElementById('adress-input');
        var searchBox = new google.maps.places.SearchBox(input);
        var that = this;
        this._map.addListener('bounds_changed', function () {
            searchBox.setBounds(that._map.getBounds());
        });
        var markers = [];
        searchBox.addListener('places_changed', function () {
            var places = searchBox.getPlaces();

            if (places.length == 0) {
                return;
            }
            markers = [];
            var bounds = new google.maps.LatLngBounds();
            places.forEach(function (place) {
                var icon = {
                    url: 'http://localhost:5000/images/marker-green.png',
                    size: new google.maps.Size(100, 100),
                    origin: new google.maps.Point(0, 0),
                    anchor: new google.maps.Point(17, 34),
                    scaledSize: new google.maps.Size(50, 50)
                };

                markers.push(new google.maps.Marker({
                    map: that._map,
                    icon: icon,
                    title: place.name,
                    position: place.geometry.location
                }));
                that._newHotel.latitude = place.geometry.location.lat();
                that._newHotel.longitude = place.geometry.location.lng();

                bounds.extend(place.geometry.location);
            });
            that._map.fitBounds(bounds);
            that._map.setZoom(17);
        });
    }
    
    addNewHotel(): void {
        var result: Result = new Result(false, '');
        this._dataService.post(JSON.stringify(this._newHotel))
            .subscribe(res => {
                result.succeeded = res.succeeded;
                result.message = res.message;
            },
            error => console.error('Error: ' + error),
            () => {
                if (result.succeeded) {
                    this._notificationService
                        .printSuccessMessage(
                        'Your hotel: ' + this._newHotel.name + ' is created');
                    // todo: redirect to manage hotel
                }
                else {
                    this._notificationService
                        .printErrorMessage(result.message);
                }
            });
    }
}