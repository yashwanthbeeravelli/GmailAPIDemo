(function () {
    angular.module('app').controller('app.views.users.createMail', [
        '$scope', '$uibModalInstance', 'abp.services.app.mail',
        function ($scope, $uibModalInstance, mailService) {
            var vm = this;

            vm.mail = {
                isRead: false
            };

            vm.send = function () {
                mailService.sendEmail(vm.mail)
                    .success(function () {
                        abp.notify.info(App.localize('Mail Sent Successfully'));
                        $uibModalInstance.close();
                    },
                    function () {
                        var modalInstance = $uibModal.open({
                            templateUrl: '/App/Main/views/mails/mailList.cshtml',
                            controller: 'app.views.mails.mailList as vm',
                            backdrop: 'static'
                        })
                    }
                        );
            };

            vm.cancel = function () {
                $uibModalInstance.dismiss();
            };
        }
    ]);
})();