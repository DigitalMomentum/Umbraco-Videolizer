
angular.module('umbraco')
    .controller('DigitalMomentum.Videolizer.PropertyEditor.YouTubeSettings',
        function ($http, $scope, notificationsService, dialogService) {
            $scope.status = {
                message: "Please wait...",
                color: "auto"
            }

            function init() {
                if ($scope.model.value == null) {
                    $scope.model.value = {
                        isConnected: false
                    }
                }
                $scope.CheckServiceStatus();
            }

            $scope.CheckServiceStatus = function () {
                $scope.status = {
                    message: "Please wait...",
                    color: "auto"
                }
                var req = {
                    method: 'POST',
                    url: "/umbraco/backoffice/Plugins/Videolizer/Controllers/Settings/YouTubeStatus",
                    headers: {
                        'Content-Type': "json",

                    },
                    umbIgnoreErrors: true //Tell Umbraco to ignore the errors - http://issues.umbraco.org/issue/U4-5588
                }

                $http(req).then(function successCallback(response) {

                    
                    $scope.model.value.isConnected = (response.data === "Connected");
                    $scope.status = {
                        message: response.data,
                        color: ($scope.model.value.isConnected) ? "green" : "red"
                    }

                }, function errorCallback(response) {
                    // called asynchronously if an error occurs
                    // or server returns response with an error status.
                    notificationsService.error("Could not check Youtube connection Status - Try Re-Authorising");

                });
            }


            $scope.openSearchWindow = function () {
                //console.log($scope.model.config.vimeoClientId, $scope.model.config.vimeoClientSecret)
                dialogService.open({
                    // set the location of the view
                    template: "/umbraco/backoffice/Plugins/Videolizer/Controllers/Oauth/YouTube",
                    // pass in data used in dialog
                    dialogData: {
                        test: "Hello World!"
                    },
                    // function called when dialog is closed
                    callback: function (value) {
                        $scope.CheckServiceStatus();
                    }
                });
            }


            init();


        });