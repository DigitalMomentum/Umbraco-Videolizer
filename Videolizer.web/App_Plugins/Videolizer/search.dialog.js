
angular.module('umbraco')
    .controller('DigitalMomentum.Videolizer.Search',
        function ($scope, notificationsService, videolizerApi) {
            $scope.hideLabel = "true"; //Hides the modal Dialog label at the top of the page
            $scope.errorStr = null;
            $scope.model = {
                searchTerm: "",
                searchType: {
                    provider: "YouTube",
                    myVideos: true,
                }
            }

            



            $scope.results = [];


            function init() {
                $scope.config = $scope.dialogData.config;

                if ($scope.config.youTube.enabled) {
                    $scope.model.searchType.provider = "YouTube"
                }else if ($scope.config.vimeo.enabled) {
                    $scope.model.searchType.provider = "Vimeo"
                }
                if ($scope.config.defaultProvider) {
                    if ($scope.config.defaultProvider === "ytChannel" || $scope.config.defaultProvider === "ytAll") {
                        $scope.model.searchType.provider = "YouTube";
                        $scope.model.searchType.myVideos = ($scope.config.defaultProvider === "ytChannel");
                    }
                    if ($scope.config.defaultProvider === "vimeoChannel" || $scope.config.defaultProvider === "vimeoAll") {
                        $scope.model.searchType.provider = "Vimeo";
                        $scope.model.searchType.myVideos = ($scope.config.defaultProvider === "vimeoChannel");
                    }
                }



                $scope.search(); //Run defaut search to get list of latest videos

              
            }

            function isSearchingForMine() {
                if ($scope.showSearchType()) {
                    //return whatever was picked
                    return $scope.model.searchType.myVideos;
                }
                if ($scope.model.searchType.provider === "YouTube" && $scope.dialogData.config.youTube.myVideos) {
                    return true;
                }
                if ($scope.model.searchType.provider === "Vimeo" && $scope.dialogData.config.vimeo.myVideos) {
                    return true;
                }

                return false;
            }

            function ShowError(title, msg) {
                $scope.errorStr = {
                    title: title,
                    message: msg
                }
            }

            $scope.showSearchType = function(){
                if ($scope.model.searchType.provider === "YouTube") {
                    return($scope.dialogData.config.youTube.myVideos && $scope.dialogData.config.youTube.publicVideos )
                }


                if ($scope.model.searchType.provider === "Vimeo") {
                    return ($scope.dialogData.config.vimeo.myVideos && $scope.dialogData.config.vimeo.publicVideos)
                }

                return false;
            }

            $scope.search = function () {
                $scope.errorStr = null;

                $scope.results = [];
                var searchTerm = $scope.model.searchTerm;
                videolizerApi.search(searchTerm, $scope.model.searchType.provider, isSearchingForMine(), null, 20, function (data) {
                    
                    $scope.results = data.items;
                });
            }


            $scope.selectVideo = function (video) {

                $scope.submit(video);
            }



            init();



        });