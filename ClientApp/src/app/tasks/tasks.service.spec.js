"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var testing_1 = require("@angular/core/testing");
var tasks_service_1 = require("./tasks.service");
describe('TasksService', function () {
    beforeEach(function () { return testing_1.TestBed.configureTestingModule({}); });
    it('should be created', function () {
        var service = testing_1.TestBed.get(tasks_service_1.TasksService);
        expect(service).toBeTruthy();
    });
});
//# sourceMappingURL=tasks.service.spec.js.map