System.register([], function(exports_1, context_1) {
    "use strict";
    var __moduleName = context_1 && context_1.id;
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
            }());
            exports_1("Result", Result);
        }
    }
});
//# sourceMappingURL=Result.js.map