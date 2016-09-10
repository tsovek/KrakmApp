///<reference path="../../../bower_components/googlemaps-ts/last/google.maps.d.ts"/>
import { Component, OnInit } from 'angular2/core';
import { RouteParams } from 'angular2/router';
import { CORE_DIRECTIVES, FORM_DIRECTIVES} from 'angular2/common';
import { MembershipService } from '../core/services/membershipService';
import { NotificationService } from '../core/services/notificationService';
import { User } from '../core/domain/user';
import { DataService } from '../core/services/dataService';
import { Result } from '../core/domain/result';
import { Entertainment } from '../core/domain/entertainment';
import { ANGULAR2_GOOGLE_MAPS_DIRECTIVES } from 'angular2-google-maps/core';
import { UtilityService } from '../core/services/utilityService';

@Component({
    selector: 'addEntertainment',
    providers: [MembershipService, NotificationService, DataService],
    templateUrl: './app/components/addEntertainment.html',
    bindings: [MembershipService, NotificationService, DataService],
    directives: [CORE_DIRECTIVES, FORM_DIRECTIVES, ANGULAR2_GOOGLE_MAPS_DIRECTIVES]
})
export class AddEntertainment implements OnInit {
    private _user: User;
    private _newEntertainment: Entertainment;
    private _entertainment: string = 'api/entertainment/';
    private _map: google.maps.Map;
    private _title: string = 'Define new entertainment';

    constructor(
        public _membershipSerivce: MembershipService,
        public _notificationService: NotificationService,
        public _dataService: DataService,
        private params: RouteParams,
        private _utility: UtilityService) {

        _dataService.set(this._entertainment);
        this._newEntertainment = new Entertainment(0, '',
            50.0666501, 19.9449799, '', false, '', '');
        var iconDefault = {
            url: 'http://localhost:5000/images/marker-pink.png',
            size: new google.maps.Size(100, 100),
            origin: new google.maps.Point(0, 0),
            anchor: new google.maps.Point(17, 34),
            scaledSize: new google.maps.Size(50, 50)
        };

        var id: string = params.get('id');
        if (id !== null) {
            _dataService.set(this._entertainment + id);
            this._title = 'Edit entertainment';
            this._dataService.get()
                .subscribe(res => {
                    var data: any = res.json();
                    this._newEntertainment = data;
                    if (this._map) {
                        this._map.setZoom(17);
                        var latLng = new google.maps.LatLng(
                            this._newEntertainment.latitude, this._newEntertainment.longitude);
                        this._map.setCenter(latLng);
                        var marker = new google.maps.Marker({
                            position: latLng,
                            map: this._map,
                            icon: iconDefault
                        });
                    }
                }, error => console.error('Error: ' + error));
        }
        else {
            this._dataService.set(this._entertainment);
            this._dataService.get()
                .subscribe(res => {
                    var bounds = new google.maps.LatLngBounds();
                    var data: any = res.json();
                    var entertainments: Array<Entertainment> = <Array<Entertainment>>data;
                    for (var entr in entertainments) {
                        var p: Entertainment = entertainments[entr];
                        var pos: google.maps.LatLng = new google.maps.LatLng(
                            p.latitude, p.longitude)
                        var defaultMarker = new google.maps.Marker({
                            position: pos,
                            map: this._map,
                            icon: iconDefault
                        });
                        bounds.extend(pos);
                    }
                    this._map.fitBounds(bounds);
                }, error => console.error('Error: ' + error));
        }

        if (_membershipSerivce.isUserAuthenticated()) {
            this._user = _membershipSerivce.getLoggedInUser();
        }
    }

    getLatLng(): google.maps.LatLng {
        if (this._newEntertainment.id === 0) {
            return new google.maps.LatLng(50.0666501, 19.9449799);
        }
        return new google.maps.LatLng(this._newEntertainment.latitude, this._newEntertainment.longitude);
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
            var bounds = new google.maps.LatLngBounds();
            places.forEach(function (place) {
                var iconDefault = {
                    url: 'images/marker-pink.png',
                    size: new google.maps.Size(100, 100),
                    origin: new google.maps.Point(0, 0),
                    anchor: new google.maps.Point(17, 34),
                    scaledSize: new google.maps.Size(50, 50)
                };
                var defaultMarker = new google.maps.Marker({
                    position: place.geometry.location,
                    map: localThis._map,
                    icon: iconDefault
                });
                localThis._newEntertainment.latitude = place.geometry.location.lat();
                localThis._newEntertainment.longitude = place.geometry.location.lng();

                bounds.extend(place.geometry.location);
            });

            localThis._map.fitBounds(bounds);
            localThis._map.setZoom(17);
        });
    }

    addNewEntertainment(): void {
        var result: Result = new Result(false, '');
        this._dataService.post(JSON.stringify(this._newEntertainment))
            .subscribe(res => {
                result.succeeded = res.succeeded;
                result.message = res.message;
            },
                error => console.error('Error: ' + error),
                () => {
                    if (result.succeeded) {
                        this._notificationService
                            .printSuccessMessage(
                                'Entertainment: ' + this._newEntertainment.name + ' is created');
                        this._utility.navigate('/Entertainments');
                    }
                    else {
                        this._notificationService
                            .printErrorMessage(result.message);
                    }
                });
    }

    file_srcs: string[] = [];

    fileChange(input) {
        var reader = [];

        for (var i = 0; i < input.files.length; i++) {
            reader.push(new FileReader());

            reader[i].addEventListener("load", (event) => {
                this.file_srcs.push(event.target.result);
                this._newEntertainment.imageUrl = event.target.result;
            }, false);
            if (input.files[i]) {
                reader[i].readAsDataURL(input.files[i]);
                document.getElementById("uploadFile").setAttribute(
                    "placeholder", input.value);
            }
        }
    }
}