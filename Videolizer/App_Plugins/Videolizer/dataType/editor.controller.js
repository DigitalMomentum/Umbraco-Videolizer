angular.module("umbraco")
    .controller("DigitalMomentum.Videolizer",
        function ($scope, dialogService) {

            $scope.localConfig = {
                searchEnabled: false,
                youTube: {
                    enabled: false,
                    myVideos: false,
                    publicVideos: false
                },
                vimeo: {
                    enabled: false,
                    myVideos: false,
                    publicVideos: false
                }
            }

            $scope.checkVideoUrl = function () {
                $scope.model.value = null;
                if ($scope.vidUrl !== "") {
                    var vidId = ytVidId($scope.vidUrl); //Try Youtube
                    if (vidId !== false) {
                        $scope.model.value = {
                            url: $scope.vidUrl,
                            id: vidId,
                            embedUrl: "//www.youtube.com/embed/" + vidId,
                            type: "YouTube"
                        }
                    }
                    if ($scope.model.value === null) {
                        vidId = vimeoVidId($scope.vidUrl); //Try Vimeo
                        if (vidId !== false) {
                            $scope.model.value = {
                                url: $scope.vidUrl,
                                id: vidId,
                                embedUrl: "//player.vimeo.com/video/" + vidId,
                                type: "Vimeo"
                            }
                        }
                    }

                    if ($scope.model.value === null) {
                        $scope.model.value = {
                            url: $scope.vidUrl,
                            id: null,
                            embedUrl: null,
                            type: "Unknown"
                        }
                    }
                }

            }


            $scope.openSearchWindow = function () {
                dialogService.open({
                    // set the location of the view
                    template: "/App_Plugins/Videolizer/search.html",
                    // pass in data used in dialog
                    dialogData: {
                        config: $scope.localConfig
                    },
                    // function called when dialog is closed
                    callback: function (value) {
                        $scope.model.value = value;
                        $scope.vidUrl = value.url;
                    }
                });
            }



            function init() {

                if ($scope.model.value != null) {
                    if (typeof $scope.model.value.url != "undefined") {
                        $scope.vidUrl = $scope.model.value.url;
                    } else {
                        //Doesn't seem to be our usual object. 
                        //Lets try to see if it was a Textbox in a previous life!
                        if (typeof $scope.model.value == "string") {
                            //could be a url stored as a plain string. Lets give it a go!
                            $scope.vidUrl = $scope.model.value;
                            $scope.checkVideoUrl();
                        }
                    }
                }

                if (typeof $scope.model.config.youTubeSettings !== "undefined" && $scope.model.config.youTubeSettings.isConnected) {
                    $scope.localConfig.defaultProvider = $scope.model.config.defaultProvider;

                    if ($scope.model.config.youTubeSettings.enableSearch && ($scope.model.config.youTubeSettings.searchPublicVideos || $scope.model.config.youTubeSettings.searchMyVideos)) {
                        $scope.localConfig.searchEnabled = true;
                        $scope.localConfig.youTube.enabled = true;

                        if ($scope.model.config.youTubeSettings.searchPublicVideos) {
                            $scope.localConfig.youTube.publicVideos = true;
                        }
                        if ($scope.model.config.youTubeSettings.searchMyVideos) {
                            $scope.localConfig.youTube.myVideos = true;
                        }
                    }
                }


                if (typeof $scope.model.config.vimeoSettings !== "undefined" && $scope.model.config.vimeoSettings.isConnected) {
                    if ($scope.model.config.vimeoSettings.enableSearch && ($scope.model.config.vimeoSettings.searchPublicVideos || $scope.model.config.vimeoSettings.searchMyVideos)) {
                        $scope.localConfig.searchEnabled = true;
                        $scope.localConfig.vimeo.enabled = true;

                        if ($scope.model.config.vimeoSettings.searchPublicVideos) {
                            $scope.localConfig.vimeo.publicVideos = true;
                        }
                        if ($scope.model.config.vimeoSettings.searchMyVideos) {
                            $scope.localConfig.vimeo.myVideos = true;
                        }
                    }
                }


            }
            init();



            /**
     * JavaScript function to match (and return) the video Id 
     * of any valid Youtube Url, given as input string.
     * @author: Stephan Schmitz <eyecatchup@gmail.com>
     * @url: http://stackoverflow.com/a/10315969/624466
     */
            function ytVidId(url) {
                var p = /^(?:https?:\/\/)?(?:www\.)?(?:youtu\.be\/|youtube\.com\/(?:embed\/|v\/|watch\?v=|watch\?.+&v=))((\w|-){11})(?:\S+)?$/;
                return (url.match(p)) ? RegExp.$1 : false;
            }


            function vimeoVidId(url) {
                var p = /^(?:https?:\/\/)?(?:www\.|player\.)?vimeo.com\/(?:channels\/(?:\w+\/)?|groups\/([^\/]*)\/videos\/|album\/(\d+)\/video\/|video\/|)(\d+)(?:$|\/|\?)?$/;
                if (url.match(p)) {
                    return RegExp.$3
                }

                var p = /^(?:https?:\/\/)?(www\.|player\.)?vimeo.com\/(\d+)\/(.+)/;
                if (url.match(p)) {

                    return RegExp.$2
                }
                //console.log(RegExp);
                return false;

            }

        });