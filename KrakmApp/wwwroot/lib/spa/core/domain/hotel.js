System.register([], function(exports_1) {
    var Hotel;
    return {
        setters:[],
        execute: function() {
            Hotel = (function () {
                function Hotel(name, latitude, longitude, description) {
                    this.Name = name;
                    this.Latitude = latitude;
                    this.Longitude = longitude;
                    this.Description = description;
                }
                return Hotel;
            })();
            exports_1("Hotel", Hotel);
        }
    }
});
