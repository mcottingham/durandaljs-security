define(['services/model', 'durandal/app', 'config'],
    function (model, app, config) {                        
        var configureBreeze = function () {
            breeze.NamingConvention.camelCase.setAsDefault();         

            // configure to resist CSRF attack
            var antiForgeryToken = $('input[name="__RequestVerificationToken"]').val();
            if (antiForgeryToken) {
                // get the current default Breeze AJAX adapter & add header
                var ajaxAdapter = breeze.config.getAdapterInstance("ajax");
                ajaxAdapter.defaultSettings = {
                    headers: {
                        '__RequestVerificationToken': antiForgeryToken
                    },
                };
            }
        };

        var primeData = function () {
            return manager.fetchMetadata();
        };

        var createEntity = function (entityName) {
            return manager.createEntity(entityName);
        };

        var saveChanges = function (entities) {
            return manager.saveChanges(entities).then(saveSuccess, saveFailure);

            function saveSuccess() {
                console.log("Save Success!");
            }

            function saveFailure(error) {
                console.log("Save Failure!");
                app.showMessage("Save Failed", "Failure", ["Ok"]);
                throw (error);
            }
        };

        configureBreeze();

        var manager = new breeze.EntityManager("/api/security");
        var valOptions = manager.validationOptions.using(config.validationOptions);
        manager.setProperties({ validationOptions: valOptions });

        model.initialize(manager);
        var vm = {
            manager: manager,
            primeData: primeData,
            createEntity: createEntity,
            saveChanges: saveChanges
        };

        return vm;
    }
);