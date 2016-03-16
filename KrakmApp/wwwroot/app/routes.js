System.register(['../../node_modules/angular2/router', './components/home', './components/hotels', './components/partners', './components/hotelPartners', './components/account/account'], function(exports_1, context_1) {
    "use strict";
    var __moduleName = context_1 && context_1.id;
    var router_1, home_1, hotels_1, partners_1, hotelPartners_1, account_1;
    var Routes, APP_ROUTES;
    return {
        setters:[
            function (router_1_1) {
                router_1 = router_1_1;
            },
            function (home_1_1) {
                home_1 = home_1_1;
            },
            function (hotels_1_1) {
                hotels_1 = hotels_1_1;
            },
            function (partners_1_1) {
                partners_1 = partners_1_1;
            },
            function (hotelPartners_1_1) {
                hotelPartners_1 = hotelPartners_1_1;
            },
            function (account_1_1) {
                account_1 = account_1_1;
            }],
        execute: function() {
            exports_1("Routes", Routes = {
                home: new router_1.Route({ path: '/', name: 'Home', component: home_1.Home }),
                hotels: new router_1.Route({ path: '/hotels', name: 'Hotels', component: hotels_1.Hotels }),
                partners: new router_1.Route({ path: '/partners', name: 'Partners', component: partners_1.Partners }),
                hotelPartners: new router_1.Route({ path: '/hotels/:id/partners', name: 'HotelPartners', component: hotelPartners_1.HotelPartners }),
                account: new router_1.Route({ path: '/account/...', name: 'Account', component: account_1.Account })
            });
            exports_1("APP_ROUTES", APP_ROUTES = Object.keys(Routes).map(function (r) { return Routes[r]; }));
        }
    }
});
//# sourceMappingURL=routes.js.map