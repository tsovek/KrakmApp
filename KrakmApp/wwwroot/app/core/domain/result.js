System.register([], function(exports_1, context_1) {
    "use strict";
    var __moduleName = context_1 && context_1.id;
    var Result;
    return {
        setters:[],
        execute: function() {
            class Result {
                constructor(succeeded, message) {
                    this.Succeeded = succeeded;
                    this.Message = message;
                }
            }
            exports_1("Result", Result);
        }
    }
});
//# sourceMappingURL=result.js.map