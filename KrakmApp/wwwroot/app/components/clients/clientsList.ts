import { Component } from 'angular2/core';
import { RouteConfig, Router, ROUTER_DIRECTIVES } from 'angular2/router';
import { CORE_DIRECTIVES, FORM_DIRECTIVES } from 'angular2/common'
import { DataService } from '../../core/services/dataService';
import { UtilityService } from '../../core/services/utilityService';
import { Client } from '../../core/domain/client';
import { Routes, APP_ROUTES } from './routes';

@Component({
    selector: 'clients',
    templateUrl: './app/components/clients/clientsList.html',
    directives: [ROUTER_DIRECTIVES, CORE_DIRECTIVES, FORM_DIRECTIVES]
})
@RouteConfig(APP_ROUTES)
export class ClientsList {
    private _clientsApi: string = 'api/clients';
    private _clients: Array<Client>;
    private routes = Routes;

    constructor(
        public clientsService: DataService,
        public utilityService: UtilityService) {

        this.routes = Routes;
        this.clientsService.set(this._clientsApi);
        this.getClients();
    }

    getClients(): void {
        this.clientsService.get().subscribe(
            res => {
                var data: any = res.json();
                this._clients = data;
            },
            error => console.error('Error: ' + error));
    }

    getFormattedDate(date: string): string {
        return new Date(date).toLocaleDateString();
    }
}