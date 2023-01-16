(function () {
    'use strict';

    function detailController($scope, eventsService) {

        var vm = this;
        vm.item = $scope.model.item;
        vm.server = $scope.model.server;
        vm.viewFirst = $scope.model.viewFirst;


        vm.selectNavigationItem = function (item) {
            eventsService.emit('usync-publisher-detail.tab.change', item);
        }

        vm.page = {
            navigation: [
                {
                    'name': 'Detail',
                    'alias': 'changes',
                    'icon': 'icon-bulleted-list',
                    'view': Umbraco.Sys.ServerVariables.application.applicationPath + "App_Plugins/uSyncPublisher/dialogs/detail.changes.html",
                    'active': true
                }
            ]
        }

        if (vm.item.itemType == 'IContent' || vm.item.itemType == 'IMedia') {
            vm.page.navigation.push({
                'name': 'View',
                'alias': 'view',
                'icon': 'icon-display',
                'view': Umbraco.Sys.ServerVariables.application.applicationPath + "App_Plugins/uSyncPublisher/dialogs/detail.view.html",
            });

            if (vm.viewFirst)
            {
                vm.page.navigation[0].active = false;
                vm.page.navigation[1].active = true;
                eventsService.emit('usync-publisher-detail.tab.change', vm.page.navigation[1]);
            }

        }


        // methods
        vm.close = close;


        function close() {
            if ($scope.model.close) {
                $scope.model.close();
            }
        }
    }

    angular.module('umbraco')
        .controller('uSyncPublisherDetailController', detailController);

})();