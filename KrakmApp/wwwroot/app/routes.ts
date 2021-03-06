﻿import { Route, Router } from 'angular2/router';
import { Home } from './components/home';
import { Hotels } from './components/hotels';
import { HotelPartners } from './components/hotelPartners';
import { Account } from './components/account/account';
import { AddHotel } from './components/addHotel';
import { HotelsList } from './components/hotelsList';
import { PartnersList } from './components/partnersList';
import { AddPartner } from './components/addPartner';
import { AddMonument } from './components/addMonument';
import { MonumentsList } from './components/monumentsList'; 
import { AddEntertainment } from './components/addEntertainment';
import { EntertainmentsList } from './components/entertainmentsList';
import { AddBanner } from './components/addBanner';
import { BannersList } from './components/bannersList';
import { ClientsList } from './components/clients/clientsList';
import { RoutesMain } from './components/routes/routesMain';
import { RouteDetails } from './components/routeDetails';

export var Routes = {
    home: new Route({ path: '/', name: 'Home', component: Home }),
    routeDetails: new Route({ path: '/routedetails/:id', name: 'RouteDetails', component: RouteDetails }),
    account: new Route({ path: '/account/...', name: 'Account', component: Account }),
    clients: new Route({ path: '/clients/...', name: 'Clients', component: ClientsList }),
    routes: new Route({ path: '/routes/...', name: 'Routes', component: RoutesMain }),

    partners: new Route({ path: '/partners', name: 'Partners', component: PartnersList }),
    addPartner: new Route({ path: 'partner', name: 'AddPartner', component: AddPartner }),
    editPartner: new Route({ path: 'partner/:id', name: 'EditPartner', component: AddPartner }),

    hotels: new Route({ path: '/hotels', name: 'HotelsList', component: HotelsList }),
    addHotel: new Route({ path: '/hotel', name: 'AddHotels', component: AddHotel }),
    editHotel: new Route({ path: '/hotel/:id', name: 'EditHotel', component: AddHotel }),

    addMonument: new Route({ path: '/monument', name: 'AddMonument', component: AddMonument }),
    editMonument: new Route({ path: '/monument/:id', name: 'EditMonument', component: AddMonument }),
    monuments: new Route({ path: '/monuments', name: 'Monuments', component: MonumentsList }),

    addEntertainment: new Route({ path: '/entertainment', name: 'AddEntertainment', component: AddEntertainment }),
    editEntertainment: new Route({ path: '/entertainment/:id', name: 'EditEntertainment', component: AddEntertainment }),
    entertainments: new Route({ path: '/entertainments', name: 'Entertainments', component: EntertainmentsList }),

    addBanner: new Route({ path: '/banner', name: 'AddBanner', component: AddBanner }),
    editBanner: new Route({ path: '/banner/:id', name: 'EditBanner', component: AddBanner }),
    banners: new Route({ path: '/banners', name: 'Banners', component: BannersList })
};

export const APP_ROUTES = Object.keys(Routes).map(r => Routes[r]);