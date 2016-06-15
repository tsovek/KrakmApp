///<reference path="../../../bower_components/googlemaps-ts/last/google.maps.d.ts"/>
///<reference path="../../../node_modules/typescript/lib/lib.d.ts"/>
import { Component, OnInit } from 'angular2/core';
import { CORE_DIRECTIVES, FORM_DIRECTIVES, NgClass } from 'angular2/common';
import { RouteConfig, RouterLink, Router, ROUTER_DIRECTIVES, RouteParams } from 'angular2/router';
import { MembershipService } from '../core/services/membershipService';
import { ANGULAR2_GOOGLE_MAPS_DIRECTIVES } from 'angular2-google-maps/core';
import { DataService } from '../core/services/dataService';
import { Routes, APP_ROUTES } from '../routes';
import { Route } from '../core/domain/route';
import { Objects, SingleObject, SortableObject, ObjectGroup } from '../core/domain/objects';

declare var jQuery: any;

@Component({
    selector: 'routesMain',
    templateUrl: './app/components/routeDetails.html',
    directives: [NgClass, CORE_DIRECTIVES, FORM_DIRECTIVES, RouterLink]
})
export class RouteDetails implements OnInit {
    private _routesApi: string = 'api/routes/';
    private _objectsApi: string = 'api/objects/';
    private _map: google.maps.Map;
    private routes = Routes;
    private _route: Route;
    private _objects: Objects;
    private _singleObjects: Array<SortableObject> = [];

    constructor(
        private _dataService: DataService,
        private params: RouteParams) {

        this._dataService.set(this._objectsApi);
        this._dataService.get().subscribe(
            res => {
                var data: any = res.json();
                this._objects = data;
                jQuery('.js-example-basic-multiple').select2({
                    placeholder: 'Select an object',
                    allowClear: true
                });
            },
            error => console.error('Error: ' + error));

        var id: string = params.get('id');
        this._dataService.set(this._routesApi + id);
        this._dataService.get().subscribe(
            res => {
                var data: any = res.json();
                this._route = data;
            },
            error => console.error('Error: ' + error));
    }

    addObject(): void {
        var that = this;
        jQuery("select#objects-selector option").filter(":selected").each(function (i, select) {
            jQuery(this).removeAttr("selected");
            var textObject: string = jQuery(this).text();
            for (var groupObj of that._objects.Objects) {
                for (var singleObj of groupObj.SingleObjects) {
                    if (singleObj.Name == textObject) {
                        var order: number = that._singleObjects.length;
                        that._singleObjects.push(
                            new SortableObject(singleObj, groupObj.Type, order));
                    }
                }
            }
        });
    }

    onDeleteClicked(element: SingleObject): void {
        var iter: number = -1;
        for (var obj of this._singleObjects) {
            iter++;
            if (obj.Object.Name === element.Name) {
                this._singleObjects.splice(iter, 1);
                break;
            }
        }
    }

    onDownClicked(element: SingleObject): void {
        var iter: number = -1;
        for (var obj of this._singleObjects) {
            iter++;
            if (obj.Object.Name === element.Name &&
                iter != this._singleObjects.length - 1) {
                this._singleObjects.splice(iter, 1);
                this._singleObjects.splice(iter + 1, 0, obj);
                break;
            }
        }
    }

    onUpClicked(element: SingleObject): void {
        var iter: number = -1;
        for (var obj of this._singleObjects) {
            iter++;
            if (obj.Object.Name === element.Name && iter != 0) {
                this._singleObjects.splice(iter, 1);
                this._singleObjects.splice(iter - 1, 0, obj);
                break;
            }
        }
    }

    getSpanClass(objType: string): string {
        var returnType: string = "pull-left fa fa-question fa-fw";
        switch (objType) {
            case "Monuments":
                returnType = "pull-left fa fa-bank fa-fw";
                break;
            case "Entertainments":
                returnType = "pull-left fa fa-gamepad fa-fw";
                break;
            case "Partners":
                returnType = "pull-left fa fa-group fa-fw";
                break;
        }
        return returnType;
    }

    onAddButtonEnabled(): boolean {
        return jQuery("select#objects-selector option").filter(":selected").length == 0;
    }

    getLatLng(): google.maps.LatLng {
        return new google.maps.LatLng(50.0666501, 19.9449799);
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

        this._map.addListener('bounds_changed', function () {

        });
    }
}