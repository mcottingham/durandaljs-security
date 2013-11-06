define([],
    function () {
        var validationOptions = {
            validateOnQuery: false,
            validateOnAttach: false,
            validateOnPropertyChange: true,
            validateOnSave: true
        };

        var vm = {
            validationOptions: validationOptions
        };

        return vm;
    }
);