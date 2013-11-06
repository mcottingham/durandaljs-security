define(['services/datacontext', 'plugins/http'],
    function (datacontext, http) {
        var ctor = function () {
            this.entity = ko.observable();
        };

        ctor.prototype.activate = function () {
            this.entity(datacontext.createEntity("Login"));
        };

        ctor.prototype.saveClick = function () {
            datacontext.saveChanges().then(success);

            function success() {
                console.log("Success!");
            }
        };

        return ctor;
    });