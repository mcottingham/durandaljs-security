define(['plugins/router', 'durandal/app', 'services/datacontext'], function (router, app, datacontext) {
    return {
        router: router,
        search: function() {
            //It's really easy to show a message box.
            //You can add custom options too. Also, it returns a promise for the user's response.
            app.showMessage('Search not yet implemented...');
        },
        activate: function () {
            return datacontext.primeData().then(boot);

            function boot() {
                router.map([
                    { route: '', title: 'Welcome', moduleId: 'viewmodels/welcome', nav: true },
                    { route: 'login', title: 'Login', moduleId: 'viewmodels/login', nav: true },
                    { route: 'flickr', moduleId: 'viewmodels/flickr', nav: true }
                ]).buildNavigationModel();

                return router.activate();
            }
        }
    };
});