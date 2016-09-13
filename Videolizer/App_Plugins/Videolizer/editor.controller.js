angular.module("umbraco")
    .controller("DigitalMomentum.Videolizer",
    function ($scope) {
        

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

        function activate() {
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
            console.log(RegExp);
            return false;

        }

    });