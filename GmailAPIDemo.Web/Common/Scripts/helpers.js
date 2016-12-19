var App = App || {};
(function () {

    var appLocalizationSource = abp.localization.getSource('GmailAPIDemo');
    App.localize = function () {
        return appLocalizationSource.apply(this, arguments);
    };

})(App);