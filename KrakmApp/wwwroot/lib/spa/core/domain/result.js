System.register([], function(exports_1) {
    var Result;
    return {
        setters:[],
        execute: function() {
            Result = (function () {
                function Result(succeeded, message) {
                    this.Succeeded = succeeded;
                    this.Message = message;
                }
                return Result;
            })();
            exports_1("Result", Result);
        }
    }
});
