import { Route, Router } from '../../node_modules/angular2/router';
import { Home } from './components/home';
import { Hotels } from './components/hotels';
import { Partners } from './components/partners';
import { HotelPartners } from './components/hotelPartners';
import { Account } from './components/account/account';

export var Routes = {
    home: new Route({ path: '/', name: 'Home', component: Home }),
    hotels: new Route({ path: '/hotels', name: 'Hotels', component: Hotels }),
    partners: new Route({ path: '/partners', name: 'Partners', component: Partners }),
    hotelPartners: new Route({ path: '/hotels/:id/partners', name: 'HotelPartners', component: HotelPartners }),
    account: new Route({ path: '/account/...', name: 'Account', component: Account })
};

export const APP_ROUTES = Object.keys(Routes).map(r => Routes[r]);