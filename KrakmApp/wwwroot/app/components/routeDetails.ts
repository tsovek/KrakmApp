///<reference path="../../../bower_components/googlemaps-ts/last/google.maps.d.ts"/>
///<reference path="../../../node_modules/typescript/lib/lib.d.ts"/>
///<reference path="linq.ts"/>
import { Component, OnInit } from 'angular2/core';
import { List } from './linq';
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
    private _detailsApi: string = 'api/routedetails/';
    private _map: google.maps.Map;
    private routes = Routes;
    private _route: Route;
    private _objects: Objects;
    private _singleObjects: Array<SortableObject> = [];
    private _markers: List<google.maps.Marker>;
    private _polyLine: google.maps.Polyline;

    constructor(
        private _dataService: DataService,
        private params: RouteParams) {

        this._markers = new List<google.maps.Marker>();
        this._polyLine = new google.maps.Polyline();
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

        this._dataService.set(this._detailsApi + id);
        this._dataService.get().subscribe(
            res => {
                var data: any = res.json();
                for (var jsonObj of data) {
                    var obj = jsonObj.Object;
                    var singleObj = new SingleObject(
                        obj.IdInType, obj.Name, obj.Description, obj.ImageUrl, obj.Latitude, obj.Longitude);
                    this._singleObjects.push(new SortableObject(singleObj, jsonObj.ObjType, jsonObj.Order));
                }
                this.updateMap();
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
                        var order: number = that._singleObjects.length + 1;
                        that._singleObjects.push(
                            new SortableObject(singleObj, groupObj.Type, order));
                        that.updateMap();
                    }
                }
            }
        });
    }

    getIconFactory(category: string): any {
        var imageUrl = '';
        switch (category) {
            case "Monuments":
                imageUrl = "http://localhost:5000/images/marker-pink.png";
                break;
            case "Entertainments":
                imageUrl = "http://localhost:5000/images/marker-blue.png";
                break;
            case "Partners":
                imageUrl = "http://localhost:5000/images/marker-green.png";
                break;
        }
        return {
            url: imageUrl,
            size: new google.maps.Size(100, 100),
            origin: new google.maps.Point(0, 0),
            anchor: new google.maps.Point(17, 34),
            scaledSize: new google.maps.Size(50, 50)
        };
    }

    getInfo(object: SortableObject): string {
        return '<div id="content">' +
            '<div id="siteNotice">' +
            '</div>' +
            '<h3 class="firstHeading">' + object.Object.Name + '</h3>' +
            '<div id="bodyContent">' +
            '<p>' +
            object.Object.Description +
            '</p>' +
            '</div>' +
            '</div>';
    }

    updateMap(): void {
        let list: List<SortableObject> = new List<SortableObject>(this._singleObjects);
        list.ForEach((e, i) => e.Order = i);

        this._markers.ForEach(e => {
            e.setMap(null);
        });
        this._markers.RemoveAll(e => true);

        this._polyLine.setMap(null);

        var bounds = new google.maps.LatLngBounds();
        var polyLines: any[] = [];
        let iter = -1;
        for (let obj of this._singleObjects) {
            iter++;
            let icon = this.getIconFactory(obj.ObjType);
            let latLng = new google.maps.LatLng(obj.Object.Latitude, obj.Object.Longitude);
            var marker = new google.maps.Marker({
                position: latLng,
                map: this._map,
                icon: icon
            });
            this._markers.Add(marker);
            bounds.extend(latLng);
            polyLines.push({ lat: latLng.lat(), lng: latLng.lng() });

            this.attachWindow(marker, obj);
        }

        let isEmpty: boolean = bounds.isEmpty();
        if (isEmpty) {
            bounds.extend(this.getLatLng());
        }
        this._map.fitBounds(bounds);
        if (isEmpty)
        {
            this._map.setZoom(12);
        }

        this._polyLine = new google.maps.Polyline({
            path: polyLines,
            geodesic: true,
            strokeColor: '#FF0000',
            strokeOpacity: 0.6,
            strokeWeight: 2
        });

        this._polyLine.setMap(this._map);
    }

    attachWindow(marker: google.maps.Marker, obj: SortableObject): void {
        var infowindow = new google.maps.InfoWindow({
            content: this.getInfo(obj),
            maxWidth: 300
        });

        marker.addListener('click', function () {
            infowindow.open(marker.get('map'), marker);
        });
    }

    getObjToPostRequest(): any {
        var request: any = {
            RouteId: this.params.get('id'),
            SpecificRoutes: []
        };
        var sortableObjs: List<SortableObject> = new List<SortableObject>(this._singleObjects);
        sortableObjs.ForEach(e => request.SpecificRoutes.push({
            Type: e.ObjType,
            IdInType: e.Object.Id,
            Order: e.Order
        }));
        return request;
    }

    onSaveRoute(): void {
        var result: any = { Succeeded: false, Message: "" };
        this._dataService.set(this._detailsApi);
        this._dataService.post(JSON.stringify(this.getObjToPostRequest()))
            .subscribe(res => {
                result.Succeeded = res.Succeeded;
                result.Message = res.Message;
            },
                error => console.error('Error: ' + error),
                () => {

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
        this.updateMap();
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
        this.updateMap();
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
        this.updateMap();
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
    }
}