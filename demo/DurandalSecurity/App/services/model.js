define([],
    function () {
        var datacontext = null;

        var initialize = function (context) {
            datacontext = context;
            var store = datacontext.metadataStore;

            store.registerEntityTypeCtor("Login", loginConstructor, validationInitializer);
        };

        var loginConstructor = function (login) {
        };

        var validationInitializer = function (entity) {
            var validationErrors = ko.observableArray([]);

            entity.entityAspect.validationErrorsChanged.subscribe(function (validationChangeArgs) {
                validationChangeArgs.added.forEach(function (item) { addError(item); });
                validationChangeArgs.removed.forEach(function (item) { validationErrors.remove(item); });
            });

            entity.hasError = function (propertyName) {
                var array = validationErrors();
                var match = array.filter(function (item) {
                    return item.propertyName == propertyName;
                });
                if (match.length > 0) {
                    return true;
                } else return false;
            };

            entity.getErrorMessage = function (propertyName) {
                var array = validationErrors();
                var match = array.filter(function (item) {
                    return item.propertyName == propertyName;
                });
                if (match.length > 0) {
                    return match[0].errorMessage;
                } else return '';
            };

            function addError(item) {
                validationErrors.remove(function (i) {
                    return i.propertyName == item.propertyName;
                });

                validationErrors.push(item);
            }
        };

        var vm = {
            initialize: initialize
        };

        return vm;
    }
);