import { Route, Router } from 'angular2/router';
import { Home } from '../../components/home';
import { Empty } from '../../components/clients/empty';
import { AddClient } from '../../components/clients/addClient';

export var Routes = {
    add: new Route({ path: '/add', name: 'Add', component: AddClient }),
    edit: new Route({ path: '/edit/:id', name: 'Edit', component: AddClient }),
    empty: new Route({ path: '/list', name: 'Empty', component: Empty }),
    home: new Route({ path: '/home', name: 'Home', component: Home })
};

export const APP_ROUTES = Object.keys(Routes).map(r => Routes[r]);