System.register([], function(exports_1) {
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
            })();
            exports_1("Partner", Partner);
        }
    }
});
