﻿(function () {
    'use strict';

    function resultController($scope) {

        var pvm = this;

        pvm.showDetails = true;
        pvm.results = $scope.vm.report;
        pvm.server = $scope.vm.server;

        pvm.complete = isComplete(pvm.results);

        // marks this as the last step in the process.
        $scope.vm.state.complete = pvm.complete;

        if ($scope.model != null && $scope.vm != null && $scope.vm.selectedServer != null) {
            var server = $scope.vm.selectedServer;
            $scope.model.title = server.name + ' Complete';
            $scope.model.subtitle = server.description + ': [' + server.url + ']';
        }

        function isComplete(results) {
            if (results != undefined && results !== null) {

                for (let i = 0; i < results.length; i++) {
                    if (results[i].Change === 'WillChange') {
                        return false;
                    }
                };
            }
            return true;
        }
    }

    angular.module('umbraco')
        .controller('uSyncPublisherResultController', resultController);
})();