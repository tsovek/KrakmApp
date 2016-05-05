import { Route, Router } from 'angular2/router';
import { Home } from './components/home';
import { Hotels } from './components/hotels';
import { Partners } from './components/partners';
import { HotelPartners } from './components/hotelPartners';
import { Account } from './components/account/account';
import { AddHotel } from './components/addHotel';
import { HotelsList } from './components/hotelsList';

export var Routes = {
    home: new Route({ path: '/', name: 'Home', component: Home }),
    hotels: new Route({ path: '/hotels', name: 'HotelsList', component: HotelsList }),
    partners: new Route({ path: '/partners', name: 'Partners', component: Partners }),
    manageHotel: new Route({ path: '/hotels/:id', name: 'Hotels', component: Hotels }),
    account: new Route({ path: '/account/...', name: 'Account', component: Account }),
    addHotel: new Route({ path: '/hotel', name: 'AddHotels', component: AddHotel }),
    editHotel: new Route({ path: '/hotel/:id', name: 'EditHotel', component: AddHotel })
};

export const APP_ROUTES = Object.keys(Routes).map(r => Routes[r]);