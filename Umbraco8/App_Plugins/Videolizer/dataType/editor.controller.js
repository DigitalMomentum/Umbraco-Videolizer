angular.module("umbraco")
    .controller("DigitalMomentum.Videolizer",
        function ($sce, $scope, editorService) {
		$scope.hasSearchFunction = false;
        
            $scope.checkVideoUrl = function () {
                $scope.model.value = null;
                if ($scope.vidUrl !== "") {
                    var vidId = ytVidId($scope.vidUrl); //Try Youtube

                    if (vidId !== false) {
                        //console.log($sce.trustAsResourceUrl("//www.youtube.com/embed/" + vidId))
                        $scope.model.value = {
                            url: $scope.vidUrl,
                            id: vidId,
                            embedUrl: "https://www.youtube.com/embed/" + vidId,
                            type: "YouTube"
                        };
                        $scope.embedAsTrusted= $sce.trustAsResourceUrl("https://www.youtube.com/embed/" + vidId);
                    }
                    if ($scope.model.value === null) {
                        vidId = vimeoVidId($scope.vidUrl); //Try Vimeo
                        if (vidId !== false) {
                            $scope.model.value = {
                                url: $scope.vidUrl,
                                id: vidId,
                                embedUrl: "https://player.vimeo.com/video/" + vidId,
                                type: "Vimeo"
                            };
                            $scope.embedAsTrusted= $sce.trustAsResourceUrl("https://player.vimeo.com/video/" + vidId);
                        }
                    }

                    if ($scope.model.value === null) {
                        $scope.model.value = {
                            url: $scope.vidUrl,
                            id: null,
                            embedUrl: null,
                            type: "Unknown"
                        };
                    }
                    //console.log("mode", $scope.model.value)
                }

            };

            $scope.trustUrl = function (url) {
                return $sce.trustAsResourceUrl(url);

            };

            $scope.openSearchWindow = function () {
                //console.log($scope.model.config.vimeoClientId, $scope.model.config.vimeoClientSecret)
               // console.log($scope.model.config)

                var options = {
                    title: "My custom infinite editor",
                    view: "/App_Plugins/Videolizer/search.html",
                   // size: "small",
                    dialogData: {
                        ytApi: $scope.model.config.ytApi,
                        ytChannelId: $scope.model.config.ytChannelId,
                        vimeoClientId: $scope.model.config.vimeoClientId,
                        vimeoClientSecret: $scope.model.config.vimeoClientSecret,
                        vimeoUserId: $scope.model.config.vimeoUserId,
                        defaultSearchType: $scope.model.config.defaultSearchType
                    },
                    submit: function (model) {
                        console.log(model);
                        $scope.vidUrl = model.url;
                        //$scope.model.value.embedAsTrusted = $sce.trustAsResourceUrl(model.url);

                       $scope.model.value = model;
                        //$scope.vidUrl = model.url;
                        //editorService.close();
                        $scope.checkVideoUrl();
                    },
                    close: function () {
                        editorService.close();
                    }
                };
                editorService.open(options); 



             /*   editorService.open({
                    // set the location of the view
                    template: "/App_Plugins/Videolizer/search.html",
                    // pass in data used in dialog
                    dialogData: {
                        ytApi: $scope.model.config.ytApi,
                        ytChannelId: $scope.model.config.ytChannelId,
                        vimeoClientId: $scope.model.config.vimeoClientId,
                        vimeoClientSecret: $scope.model.config.vimeoClientSecret,
                        vimeoUserId: $scope.model.config.vimeoUserId,
                        defaultSearchType: $scope.model.config.defaultSearchType
                    },
                    // function called when dialog is closed
                    callback: function (value) {
                        $scope.model.value = value;
                        $scope.vidUrl = value.url;
                    }
                }); */
            };



            function onInit() {
             
             
			if ($scope.model.value !== null) {
				if (typeof $scope.model.value.url !== "undefined") {
                    $scope.vidUrl = $scope.model.value.url;
                    if (typeof $scope.model.value.embedUrl !== 'undefined') {
                        $scope.embedAsTrusted = $sce.trustAsResourceUrl($scope.model.value.embedUrl);
                    }
				} else {
					//Doesn't seem to be our usual object. 
					//Lets try to see if it was a Textbox in a previous life!
					if (typeof $scope.model.value === "string") {
						//could be a url stored as a plain string. Lets give it a go!
						$scope.vidUrl = $scope.model.value;
						$scope.checkVideoUrl();
					}
				}
            }

            
            if ((!$scope.model.config.ytApi || !$scope.model.config.ytApi.trim()) && (!$scope.model.config.vimeoClientId || !$scope.model.config.vimeoClientId.trim() || !$scope.model.config.vimeoClientSecret || !$scope.model.config.vimeoClientSecret.trim()) ) {
				$scope.hasSearchFunction = false;
			} else {
				$scope.hasSearchFunction = true;
			}
        }
            onInit();



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