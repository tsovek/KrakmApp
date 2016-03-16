System.register([], function(exports_1) {
    var Registration;
    return {
        setters:[],
        execute: function() {
            Registration = (function () {
                function Registration(username, password, email) {
                    this.Username = username;
                    this.Password = password;
                    this.Email = email;
                }
                return Registration;
            })();
            exports_1("Registration", Registration);
        }
    }
});
