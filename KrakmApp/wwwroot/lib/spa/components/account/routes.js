System.register(['../../../../node_modules/angular2/router', './login', './register', '../../components/home'], function(exports_1) {
    var router_1, login_1, register_1, home_1;
    var Routes, APP_ROUTES;
    return {
        setters:[
            function (router_1_1) {
                router_1 = router_1_1;
            },
            function (login_1_1) {
                login_1 = login_1_1;
            },
            function (register_1_1) {
                register_1 = register_1_1;
            },
            function (home_1_1) {
                home_1 = home_1_1;
            }],
        execute: function() {
            exports_1("Routes", Routes = {
                login: new router_1.Route({ path: '/', name: 'Login', component: login_1.Login }),
                register: new router_1.Route({ path: '/register', name: 'Register', component: register_1.Register }),
                home: new router_1.Route({ path: '/home', name: 'Home', component: home_1.Home })
            });
            exports_1("APP_ROUTES", APP_ROUTES = Object.keys(Routes).map(function (r) { return Routes[r]; }));
        }
    }
});
