///<reference path="../../../../bower_components/googlemaps-ts/last/google.maps.d.ts"/>
///<reference path="../../../../node_modules/typescript/lib/lib.d.ts"/>
import { Component, OnInit } from 'angular2/core';
import { CORE_DIRECTIVES, FORM_DIRECTIVES } from 'angular2/common';
import { RouteConfig, RouterLink, Router, ROUTER_DIRECTIVES } from 'angular2/router';
import { MembershipService } from '../../core/services/membershipService';
import { ANGULAR2_GOOGLE_MAPS_DIRECTIVES } from 'angular2-google-maps/core';
import { DataService } from '../../core/services/dataService';
import { Routes, APP_ROUTES } from './routes';
import { Route } from '../../core/domain/route';
import { List } from '../linq';

@Component({
    selector: 'routesMain',
    templateUrl: './app/components/routes/routesMain.html',
    directives: [ROUTER_DIRECTIVES, CORE_DIRECTIVES, FORM_DIRECTIVES, RouterLink]
})
@RouteConfig(APP_ROUTES)
export class RoutesMain implements OnInit {
    private _routesApi: string = 'api/routes/';
    private _detailsApi: string = 'api/routedetails/';
    private _map: google.maps.Map;
    private routes = Routes;
    private _definedRoutes: Route[];
    private _checkedItems: Array<number>;
    private _polylines: List<PolylineOwner>;
    private _colors: List<string>;

    constructor(private _dataService: DataService) {
        this._colors = new List<string>();
        this._colors.Add("#008000");
        this._colors.Add("#FF0000");
        this._colors.Add("#000080");
        this._colors.Add("#FF00FF");
        this._colors.Add("#808000");

        this._polylines = new List<PolylineOwner>();
        this._checkedItems = [];
        this._dataService.set(this._routesApi);
        this._dataService.get().subscribe(
            res => {
                var data: any = res.json();
                this._definedRoutes = data;
            },
            error => console.error('Error: ' + error));
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
            zoom: 12,
            mapTypeId: google.maps.MapTypeId.ROADMAP,
            styles: styleArray,
            disableDefaultUI: false,
            scaleControl: true
        };
        this._map = new google.maps.Map(document.getElementById("map"),
            mapOptions);
    }

    disabledActionButtons(): boolean {
        return this._checkedItems && this._checkedItems.length != 1;
    }

    onCheckedChange(route: Route, checked: boolean) {
        if (checked) {
            this._checkedItems.push(route.id);
            this.addRouteView(route.id);
        } 
        else {
            var index = this._checkedItems.indexOf(route.id);
            if (index > -1) {
                this._checkedItems.splice(index, 1);
                this.removeRouteView(route.id);
            }
        }
    }

    removeRouteView(index: number): void {
        var route = this._polylines.FirstOrDefault(e => e.Id == index);
        if (route) {
            route.Polyline.setMap(null);
            this._polylines.Remove(route);

            var bounds = new google.maps.LatLngBounds();
            this._polylines.ForEach(e => e.LatLng.forEach(ll => bounds.extend(ll)));

            let isEmpty: boolean = bounds.isEmpty();
            if (isEmpty) {
                bounds.extend(this.getLatLng());
            }
            this._map.fitBounds(bounds);
            if (isEmpty) {
                this._map.setZoom(12);
            }
        }
    }

    addRouteView(index: number): void {
        this._dataService.set(this._detailsApi + index);
        this._dataService.get().subscribe(
            res => {
                var data: any = res.json();
                var bounds = new google.maps.LatLngBounds();
                
                var polyLines: any[] = [];
                var latLngs: google.maps.LatLng[] = [];
                for (let obj of data) {
                    let latLng = new google.maps.LatLng(obj.object.latitude, obj.object.longitude);
                    latLngs.push(latLng);
                    bounds.extend(latLng);
                    polyLines.push({ lat: latLng.lat(), lng: latLng.lng() });
                }
                this._map.fitBounds(bounds);

                let length: number = this._polylines.Count();
                let correctIndex = length < this._colors.Count() ? length : length % this._colors.Count();
                let color: string = this._colors.ElementAt(correctIndex);
                this._polylines.Add(new PolylineOwner(
                    index,
                    new google.maps.Polyline({
                        path: polyLines,
                        geodesic: true,
                        strokeColor: color,
                        strokeOpacity: 0.6,
                        strokeWeight: 2,
                        map: this._map
                    }),
                    latLngs));
            },
            error => console.error('Error: ' + error));
    }
}

class PolylineOwner {
    constructor(
        public Id: number,
        public Polyline: google.maps.Polyline,
        public LatLng: google.maps.LatLng[]) { }
}