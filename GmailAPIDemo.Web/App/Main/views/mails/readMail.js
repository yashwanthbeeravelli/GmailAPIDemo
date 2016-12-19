(function () {
    angular.module('app').controller('app.views.mails.openMail', [
        '$scope', '$stateParams','$location', 'abp.services.app.mail',
        function ($scope, $stateParams, $location,mailService) {

            var vm = this;
            vm.mails = [];
            var params = $location.search();
            vm.mailData = "";
            var x = $location.path().split(/[\s/]+/).pop();
            function init() {
                mailService.getMail(x).then(function (data) {
                   vm.mailData =  data.data;
                });
            }
            //vm.viewMail = function (tId) {
            //    $state.go('openMail', { threadId: tId });
            //};
            init();
            //vm.viewMail();
        }
    ]);
})();