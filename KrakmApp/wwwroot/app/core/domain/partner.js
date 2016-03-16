System.register([], function(exports_1, context_1) {
    "use strict";
    var __moduleName = context_1 && context_1.id;
    var Partner;
    return {
        setters:[],
        execute: function() {
            Partner = (function () {
                function Partner(id, name, latitude, longitude, description) {
                    this.Id = id;
                    this.Name = name;
                    this.Latitude = latitude;
                    this.Longitude = longitude;
                    this.Description = description;
                }
                return Partner;
            }());
            exports_1("Partner", Partner);
        }
    }
});
//# sourceMappingURL=partner.js.map