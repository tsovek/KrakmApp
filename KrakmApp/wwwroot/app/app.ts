﻿///<reference path="../../node_modules/angular2/typings/browser.d.ts"/>
import {provide, Component, View} from '../../node_modules/angular2/core';
import {CORE_DIRECTIVES} from '../../node_modules/angular2/common';
import {bootstrap} from '../../node_modules/angular2/platform/browser';
import {HTTP_BINDINGS, HTTP_PROVIDERS, Headers, RequestOptions, BaseRequestOptions} from '../../node_modules/angular2/http';
import {
RouteConfig, ROUTER_DIRECTIVES, ROUTER_PROVIDERS, ROUTER_BINDINGS,
Location, LocationStrategy, HashLocationStrategy
} from '../../node_modules/angular2/router';
import 'rxjs/add/operator/map';
import {enableProdMode} from '../../node_modules/angular2/core';

enableProdMode();
import { Routes, APP_ROUTES } from './routes';

import { DataService } from './core/services/dataService';
import { MembershipService } from './core/services/membershipService';
import { UtilityService } from './core/services/utilityService';
import { User } from './core/domain/user';

@Component({
    selector: 'krakmapp-app',
    templateUrl: './app/app.html',
    directives: [ROUTER_DIRECTIVES, CORE_DIRECTIVES]
})
@RouteConfig(APP_ROUTES)
export class AppRoot {
    private routes = Routes;

    constructor(public membershipService: MembershipService, location: Location) {
        this.routes = Routes;
        location.go('/');
    }

    isUserLoggedIn(): boolean {
        return this.membershipService.isUserAuthenticated();
    }

    getUserName(): string {
        if (this.isUserLoggedIn()) {
            var _user = this.membershipService.getLoggedInUser();
            return _user.Username;
        }
        else
            return 'Account';
    }

    logout(): void {
        this.membershipService.logout()
            .subscribe(res => {
                localStorage.removeItem('user');
            },
                error => console.error('Error: ' + error),
                () => { });
    }
}

class AppBaseRequestOptions extends BaseRequestOptions {
    headers: Headers = new Headers({
        'Content-Type': 'application/json'
    })
}

bootstrap(AppRoot, [HTTP_PROVIDERS, ROUTER_PROVIDERS,
    provide(RequestOptions, { useClass: AppBaseRequestOptions }),
    provide(LocationStrategy, { useClass: HashLocationStrategy }),
    DataService, MembershipService, UtilityService])
    .catch(err => console.error(err));