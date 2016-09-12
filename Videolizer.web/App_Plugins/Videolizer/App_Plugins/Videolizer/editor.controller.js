angular.module("umbraco")
    .controller("DigitalMomentum.Videolizer",
    function ($scope) {
        function activate() {
            if ($scope.model.value != null) {
                $scope.vidUrl = $scope.model.value.url;
            }
        }
        activate();

        $scope.checkVideoUrl = function () {
            $scope.model.value = null;
            if ($scope.vidUrl != "") {
                var vidId = ytVidId($scope.vidUrl); //Try Youtube
                if (vidId != false) {
                    $scope.model.value = {
                        url: $scope.vidUrl,
                        id: vidId,
                        embedUrl: "https://www.youtube.com/embed/" + vidId,
                        type: "YouTube"
                    }
                }
                if ($scope.model.value == null) {
                    var vidId = vimeoVidId($scope.vidUrl); //Try Vimeo
                    console.log(vidId)
                    if (vidId != false) {
                        $scope.model.value = {
                            url: $scope.vidUrl,
                            id: vidId,
                            embedUrl: "//player.vimeo.com/video/" + vidId,
                            type: "Vimeo"
                        }
                    }
                }

                if ($scope.model.value == null) {
                    $scope.model.value = {
                        url: $scope.vidUrl,
                        id: null,
                        embedUrl: null,
                        type: "Unknown"
                    }
                }
            }
           
        }


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
            return (url.match(p)) ? RegExp.$3 : false;
        }

    });