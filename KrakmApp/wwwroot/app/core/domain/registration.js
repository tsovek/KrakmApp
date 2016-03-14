System.register([], function(exports_1, context_1) {
    "use strict";
    var __moduleName = context_1 && context_1.id;
    var Registration;
    return {
        setters:[],
        execute: function() {
            class Registration {
                constructor(username, password, email) {
                    this.Username = username;
                    this.Password = password;
                    this.Email = email;
                }
            }
            exports_1("Registration", Registration);
        }
    }
});
//# sourceMappingURL=registration.js.map