(function () {
    'use strict';

    function detailViewController($scope, $sce, eventsService, uSyncPublishService) {

        var vm = this;
        vm.loaded = false;
        vm.loading = true;
        vm.item = $scope.model.item;
        vm.server = $scope.model.server;
        vm.viewFirst = $scope.model.viewFirst;
        vm.entity = {};

        vm.isContent = vm.item.itemType != 'IMedia';

        var evts = [];

        if (!vm.loaded && vm.viewFirst) {
            loadView();
        }

        evts.push(eventsService.on("usync-publisher-detail.tab.change", function () {
            if (!vm.loaded) {
                loadView();
            }
        }));

        $scope.$on('$destroy', function () {
            for (var e in evts) {
                eventsService.unsubscribe(evts[e]);
            }
        });


        function loadView() {
            vm.loaded = true;

            if (vm.isContent) {

                uSyncPublishService.getContentEntity(vm.item.key, vm.server.alias)
                    .then(function (result) {
                        vm.entity = result.data;

                        vm.localUrl = $sce.trustAsResourceUrl(vm.entity.local.localUrl);
                        vm.remoteUrl = $sce.trustAsResourceUrl(vm.entity.remote.remoteUrl)
                        vm.loading = false;

                    });
            }
            else {
                uSyncPublishService.getMediaEntity(vm.item.key, vm.server.alias)
                    .then(function (result) {
                        vm.entity = result.data;

                        vm.localUrl = $sce.trustAsResourceUrl(vm.entity.local.localUrl);
                        vm.localMedia = JSON.parse(vm.entity.local.mediaProperty);

                        vm.remoteUrl = $sce.trustAsResourceUrl(vm.entity.remote.remoteUrl)
                        vm.remoteMedia = JSON.parse(vm.entity.remote.mediaProperty);
                        vm.loading = false;

                    });

            }
        }
    }

    angular.module('umbraco')
        .controller('uSyncPublisherDetailViewController', detailViewController);
})();