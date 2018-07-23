angular.module("umbraco")
    .controller("DigitalMomentum.Videolizer.GridEditor",
	function ($scope, dialogService) {
		$scope.hasSearchFunction = false;

		var defaultEmbedConfig = {
			Portrait: true,
			Title: true,
			Byline: true,
			StartAt: 0,
			Controls:true
		}

        $scope.checkVideoUrl = function () {
			$scope.control.value = null;
            if ($scope.vidUrl != "") {
                var vidId = ytVidId($scope.vidUrl); //Try Youtube
                if (vidId != false) {
					$scope.control.value = {
                        url: $scope.vidUrl,
                        id: vidId,
                        embedUrl: "//www.youtube.com/embed/" + vidId,
						type: "YouTube",
						EmbedConfig: defaultEmbedConfig
                    }
                }
				if ($scope.control.value == null) {
                    var vidId = vimeoVidId($scope.vidUrl); //Try Vimeo
                    if (vidId != false) {
						$scope.control.value = {
                            url: $scope.vidUrl,
                            id: vidId,
                            embedUrl: "//player.vimeo.com/video/" + vidId,
							type: "Vimeo",
							EmbedConfig: defaultEmbedConfig
                        }
                    }
                }

				if ($scope.control.value == null) {
					$scope.control.value = {
                        url: $scope.vidUrl,
                        id: null,
                        embedUrl: null,
						type: "Unknown",
						EmbedConfig: defaultEmbedConfig
                    }
				}
				//console.log("model", $scope.control.value)
            }
           
		}


		$scope.openSearchWindow = function () {
			//console.log($scope.model.config.vimeoClientId, $scope.model.config.vimeoClientSecret)
			dialogService.open({
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
					$scope.control.value = value;
					$scope.vidUrl = value.url;
				}
			});
		}



        function activate() {
			if ($scope.control.value != null) {
				if (typeof $scope.control.value.url != "undefined") {
					$scope.vidUrl = $scope.control.value.url;
					
				} else {
					//Doesn't seem to be our usual object. 
					//Lets try to see if it was a Textbox in a previous life!
					if (typeof $scope.control.value == "string") {
						//could be a url stored as a plain string. Lets give it a go!
						$scope.vidUrl = $scope.control.value;
						$scope.checkVideoUrl();
					}
				}

				if (typeof $scope.control.value.EmbedConfig == "undefined") {
					//Backwards compatibility - Handles existing pages created before the property was added
					$scope.control.value.EmbedConfig = {};
				}

				if ((typeof ($scope.model.config.ytApi) === "undefined" || $scope.model.config.ytApi == "") && (typeof ($scope.model.config.vimeoClientId) === "undefined" || $scope.model.config.vimeoClientId == "" || typeof ($scope.model.config.vimeoClientSecret) === "undefined" || $scope.model.config.vimeoClientSecret == "")) {
					$scope.hasSearchFunction = false;
				} else {
					$scope.hasSearchFunction = true;
				}
			}

			
        }
        activate();



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