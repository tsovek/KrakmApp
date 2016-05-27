import { Route, Router } from 'angular2/router';
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

export var Routes = {
    home: new Route({ path: '/', name: 'Home', component: Home }),
    account: new Route({ path: '/account/...', name: 'Account', component: Account }),

    partners: new Route({ path: '/partners', name: 'Partners', component: PartnersList }),
    addPartner: new Route({ path: 'partner', name: 'AddPartner', component: AddPartner }),
    editPartner: new Route({ path: 'partner/:id', name: 'EditPartner', component: AddPartner }),

    hotels: new Route({ path: '/hotels', name: 'HotelsList', component: HotelsList }),
    addHotel: new Route({ path: '/hotel', name: 'AddHotels', component: AddHotel }),
    editHotel: new Route({ path: '/hotel/:id', name: 'EditHotel', component: AddHotel }),

    addMonument: new Route({ path: '/monument', name: 'AddMonument', component: AddMonument }),
    editMonument: new Route({ path: '/monument/:id', name: 'EditMonument', component: AddMonument }),
    monuments: new Route({ path: '/monuments', name: 'Monuments', component: MonumentsList })
};

export const APP_ROUTES = Object.keys(Routes).map(r => Routes[r]);