(function () {
    angular.module('app').controller('app.views.mails.mailList', [
        '$scope', '$uibModal', '$state', 'abp.services.app.mail',
        function ($scope, $uibModal, $state, mailService) {
            var vm = this;
            vm.newMailsCount = 0;
            vm.threads = [];
            vm.tId = '';
            vm.Refresh = function () {
                mailService.calculateNewMails({}).then(function (x) {
                    vm.newMailsCount = x.data;
                    if (vm.newMailsCount > 0) {
                        mailService.getMailsFromGmail({}).then(function (result) {
                            mailService.syncMails({}).then(function (y) {
                                if (y.status == 200)
                                    getMails();
                            });
                        });
                    }
                });
            }
            setInterval(function () {
                mailService.calculateNewMails({}).then(function (x) {
                    vm.newMailsCount = x.data;
                    if (vm.newMailsCount > 0) {
                        mailService.getMailsFromGmail({}).then(function (result) {
                            mailService.syncMails({}).then(function (y) {
                                if (y.status == 200)
                                    getMails();
                            });
                        });
                    }
                });
            },300000)
            setInterval(function () {
                mailService.calculateNewMails().success(function (result) {
                    if (result > 0) {
                        vm.newMailsCount = result;
                    }
                    else vm.newMailsCount = 0;
                });
                abp.notify.info(App.localize('You have ' + vm.newMailsCount + ' new Mails'));
            }, 300000)

            vm.InsertMailsIntoLocalDB = function () {
                mailService.insertMailsIntoDB({}).result.then(function () {
                    getMails();
                });
            };

            function getMails() {
                mailService.getMails({}).success(function (result) {
                    vm.threads = result.items;
                });
            }

            vm.openMailCreationModal = function () {
                var modalInstance = $uibModal.open({
                    templateUrl: '/App/Main/views/mails/createMail.cshtml',
                    controller: 'app.views.users.createMail as vm',
                    backdrop: 'static'
                });

                modalInstance.result.then(function () {
                    getMails();
                });
            };
            
            vm.viewMail = function (tId) {
               // var x = tId.thread.threadId;
            // $state.go('openMail', { threadId: x.thread.threadId });
            };
            getMails();
            vm.Refresh();
            vm.viewMail();
        }
    ]);
})();