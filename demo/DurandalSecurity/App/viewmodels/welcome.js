define(['services/datacontext', 'plugins/http'],
    function (datacontext, http) {
        var ctor = function () {
        };

        ctor.prototype.clickHandler = function () {            
        };

        ctor.prototype.createClick = function () {
            var entity = datacontext.createEntity("Test");
            entity.name("Foo1");
            datacontext.saveChanges().then(success).fail(failure);

            function success() {
                console.log("Success!");
            }

            function failure(error) {
                console.log("Failure!");
                console.log(error);
            }
        };

        return ctor;
    });