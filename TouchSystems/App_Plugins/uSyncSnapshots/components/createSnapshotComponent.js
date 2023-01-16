(function () {
    'use strict';

    var createSnapshotComponent = {
        templateUrl: Umbraco.Sys.ServerVariables.application.applicationPath + 'App_Plugins/uSyncSnapshots/components/createComponent.html',
        controllerAs: 'vm',
        controller: createSnapshotController
    };

    function createSnapshotController($rootScope, $q, $scope,
        uSync8DashboardService,
        uSyncSnapshotService,
        eventsService,
        notificationsService,
        uSyncHub) {

        var vm = this;

        init();

        vm.createButton = {
            state: 'init',
            defaultButton: {
                labelKey: 'usync_create-snapshot',
                handler: function () {
                    createSnapshot('');
                }
            },
            subButtons: []
        };

        getHandlerGroups();

        function init() {
            vm.working = false;
            vm.reported = false;
            vm.result = '';

            vm.create = createSnapshot;
            vm.snapshot = {
                name: '',
                includeFolders: true
            };
        }

        ///
        function createSnapshot(group, isFull) {
            vm.createButton.state = 'busy';
            vm.working = true;
            vm.reported = false;
            vm.result = "";

            var promises = [initHub()];

            $q.all(promises).then(function () {

                console.log('starting');

                uSyncSnapshotService.createSnapshot(vm.snapshot.name, vm.snapshot.includeFolders, group, isFull, getClientId())
                    .then(function (result) {
                        vm.createButton.state = 'success';
                        vm.reported = true;

                        if (result.data.fileCount === 0) {
                            vm.result = 'Empty Snapshot, no changes detected';
                        }
                        else {
                            vm.result = 'Snapshot created ' + result.data.fileCount + ' changes captured';
                        }

                        $rootScope.$broadcast('usync-snapshot-reloaded');
                        notificationsService.success('complete', 'snapshot created');
                    }, function (error) {
                        vm.working = false;
                        vm.createButton.state = 'error';
                        notificationsService.error('failed', error.data.exceptionMessage);
                    });

            });

        }

        function getHandlerGroups() {
            uSync8DashboardService.getHandlerGroups()
                .then(function (result) {
                    _.forEach(result.data, function (group, key) {

                        if (!key.startsWith('_')) {
                            vm.createButton.subButtons.push({
                                handler: function () {
                                    createSnapshot(group, false);
                                },
                                labelKey: 'usync_create-' + key.toLowerCase()
                            });
                        }
                    });

                    vm.createButton.subButtons.push({
                        handler: function () {
                            createSnapshot('', true);
                        },
                        labelKey: 'usync_create-full'
                    });
                });
        }

        function initHub() {

            var defer = $q.defer();

            uSyncHub.initHub(function (hub) {
                vm.hub = hub;

                vm.hub.on('add', function (data) {
                    vm.status = data;
                });

                vm.hub.on('update', function (update) {
                    vm.update = update;
                });

                vm.hub.start(function (result) {
                    defer.resolve('complete ' + result);
                });


            });

            return defer.promise;

        }


        function getClientId() {
            if ($.connection !== undefined) {
                return $.connection.connectionId;
            }
            return "";
        }

        /// reset when the tab changes (and its been completed)
        var evts = [];

        evts.push(eventsService.on('usync-snapshot.tab.change', function (event) {
            if (vm.reported) {
                init();
            }
        }));

        //ensure to unregister from all events!
        $scope.$on('$destroy', function () {
            for (var e in evts) {
                eventsService.unsubscribe(evts[e]);
            }
        });
    }

    angular.module('umbraco')
        .component('usyncSnapshotCreate', createSnapshotComponent);
})();