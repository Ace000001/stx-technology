﻿(function () {
    'use strict';

    function dialogManager($rootScope, $timeout,
        editorService, navigationService, uSyncItemManager) {

        var emptyGuid = '00000000-0000-0000-0000-000000000000';

        return {
            openPublisherPushContent: openPublisherPushContent,
            openPublisherPullContent: openPublisherPullContent,

            openPublisherPushMedia: openPublisherPushMedia,
            openPublisherPullMedia: openPublisherPullMedia,

            openPublisherPushItemDialog: openPublisherPushItemDialog,
            openPublisherPullItemDialog: openPublisherPullItemDialog,

            openPublisherPushFileDialog: openPublisherPushFileDialog,
            openPublisherPullFileDialog: openPublisherPullFileDialog,

            openConfigDialog: openConfigDialog,

            openSyncDialog: openSyncDialog,

            openServerDialog: openServerDialog,
            openNewServerDialog: openNewServerDialog
        };

        /////////////////

        function getLocalItem(options) {
            if (options.action !== undefined && options.action.metaData !== undefined && options.action.metaData !== null) {
                return JSON.parse(options.action.metaData._syncLocalItem);
            }

            return options.items[0];
        }

        // settings 
        function openPublisherPushItemDialog(options, cb) {
            openSyncDialog('Push settings', 'publisherDialog', options, cb, 'settingsPush');
        }

        function openPublisherPullItemDialog(options, cb) {
            openSyncDialog('Pull settings', 'publisherDialog', options, cb, 'settingsPull');
        }

        // content 
        function openPublisherPushContent(options, cb) {
            openSyncDialog('Publish Content', 'publisherDialog', options, cb, 'push');
        }

        function openPublisherPullContent(options, cb) {
            openSyncDialog('Pull Content', 'publisherDialog', options, cb, 'pull');
        }

        // media 
        function openPublisherPushMedia(options, cb) {
            openSyncDialog('Publish Media', 'publisherDialog', options, cb, 'push');
        }

        function openPublisherPullMedia(options, cb) {
            openSyncDialog('Pull Media', 'publisherDialog', options, cb, 'pull');
        }

        function openPublisherPushFileDialog(options, cb) {
            openSyncDialog('Push Files', 'publisherDialog', options, cb, 'filePush');
        }

        function openPublisherPullFileDialog(options, cb) {
            openSyncDialog('Pull Files', 'publisherDialog', options, cb, 'filePull');
        }

        // server
        function openConfigDialog(mode, server, callback) {

            var options = {
                hideItems: true,
                serverAlias: server,
                items: [
                    { udi: 'umb://document-type/' + emptyGuid, name: 'ContentType' },
                    { udi: 'umb://data-type/' + emptyGuid, name: 'DataType' },
                    { udi: 'umb://media-type/' + emptyGuid, name: 'MediaType' }]
            }

            openDialog('Deploy Settings', 'publisherDialog', options, callback, 'config' + mode);
        }

        function openSyncDialog(dialogTitle, dialogView, options, cb, mode, size = 'small') {

            if (options.entity !== undefined) {
                options.items = [options.entity];
            }

            if (options.items.length === 1) {

                var localItem = getLocalItem(options);
                var dialogOptions = Object.assign({}, options);
                dialogOptions.items = [localItem];

                openDialog(dialogTitle, dialogView, dialogOptions, cb, mode, size);
            }
            else {
                openDialog(dialogTitle, dialogView, options, cb, mode, size);
            }
        }

        function openDialog(dialogTitle, dialogView, options, cb, mode, size) {

            editorService.open({
                options: options,
                mode: mode,
                single: options.items.length === 1,
                title: dialogTitle,
                size: size,
                view: Umbraco.Sys.ServerVariables.uSyncPublisher.pluginPath + 'dialogs/' + dialogView + '.html',
                submit: function (done) {
                    editorService.close();
                    navigationService.hideNavigation();
                    if (cb !== undefined) {
                        cb(true);
                    }
                },
                close: function () {
                    editorService.close();
                    navigationService.hideNavigation();
                    if (cb !== undefined) {
                        cb(false);
                    }
                }
            });

            // wrap in a timeout, get rid of the 'bounce' 
            //$timeout(function () {
            //    navigationService.hideDialog();
            //});
        }


        function openNewServerDialog(entity, cb, url = '') {
            editorService.open({
                view: Umbraco.Sys.ServerVariables.uSyncPublisher.pluginPath + 'dialogs/addServer.html',
                title: 'Add Server',
                size: 'small',
                url: url,
                placeholder: url.length == 0 ? 'Server name' : 'name for this server',
                submit: function (model) {
                    editorService.close();
                    navigationService.hideNavigation();
                    openServerDialog(model.alias, cb);
                },
                close: function () {
                    editorService.close();
                    navigationService.hideNavigation();
                    if (cb !== undefined) {
                        cb(false);
                    }
                }
            });
        }

        function openServerDialog(alias, cb) {
            editorService.open({
                serverAlias: alias,
                title: 'Server View',
                view: Umbraco.Sys.ServerVariables.uSyncPublisher.pluginPath + 'backoffice/uSyncPublisher/server.html',
                submit: function (done) {
                    $rootScope.$broadcast('usync-publisher-settings-reload');
                    navigationService.hideNavigation();
                    editorService.close();
                    if (cb !== undefined) {
                        cb(true);
                    }
                },
                close: function () {
                    $rootScope.$broadcast('usync-publisher-settings-reload');
                    navigationService.hideNavigation();
                    editorService.close();
                    if (cb !== undefined) {
                        cb(false);
                    }
                }
            });
        }

    }

    angular.module('umbraco')
        .factory('uSyncPublishDialogManager', dialogManager);
})();