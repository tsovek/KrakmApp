import { Route, Router } from 'angular2/router';
import { Empty } from '../../components/routes/empty';
import { AddRoute } from '../../components/routes/addRoute';
import { Home } from '../../components/home';
import { RouteDetails } from '../../components/routeDetails';

export var Routes = {
    empty: new Route({ path: '/list', name: 'Empty', component: Empty }),
    add: new Route({ path: '/add', name: 'AddRoute', component: AddRoute }),
    edit: new Route({ path: '/edit/:id', name: 'EditRoute', component: AddRoute }),
    routeDetails: new Route({ path: '/routedetails/:id', name: 'Details', component: RouteDetails }),
    home: new Route({ path: '/home', name: 'Home', component: Home })
};

export const APP_ROUTES = Object.keys(Routes).map(r => Routes[r]);