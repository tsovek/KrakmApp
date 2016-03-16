System.register([], function(exports_1) {
    var User;
    return {
        setters:[],
        execute: function() {
            User = (function () {
                function User(username, password) {
                    this.Username = username;
                    this.Password = password;
                }
                return User;
            })();
            exports_1("User", User);
        }
    }
});
