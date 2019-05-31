
angular.module('umbraco')
    .controller('DigitalMomentum.Videolizer.Search',
        function ($http, $scope, $sce, notificationsService, vimeoApi) {

            $scope.hideLabel = "true"; //Hides the modal Dialog label at the top of the page
            $scope.errorStr = null;
            $scope.model.searchTerm = "";
            $scope.model.searchType = $scope.model.dialogData.defaultSearchType;
            //$scope.model = {
            //	searchTerm: "",
            //       ytApi: $scope.model.dialogData.ytApi,
            //       ytChannelId: $scope.model.dialogData.ytChannelId,
            //       vimeoClientId: $scope.model.dialogData.vimeoClientId,
            //       vimeoClientSecret: $scope.model.dialogData.vimeoClientSecret,
            //       vimeoUserId: $scope.model.dialogData.vimeoUserId,
            //       searchType: $scope.model.dialogData.defaultSearchType
            //		}

            //console.log($scope.model)


            $scope.results = {
                yt: [],
                vimeo: []
            };

            function init() {

                vimeoApi.init($scope.model.dialogData.vimeoClientId, $scope.model.dialogData.vimeoClientSecret, $scope.model.dialogData.vimeoUserId);


                $scope.search(); //Run defaut search to get list of latest videos

                //Select the appropriate Default Radio
                //if ($scope.model.ytApi) {
                //	if ($scope.model.dialogData.ytChannelId) {
                //		$scope.searchType = "ytChannel";
                //	} else {
                //		$scope.searchType = "ytAll";
                //	}

                //}
            }

            function searchYouTube(searchTerm) {
                console.log($scope.model);
                var ApiUrl = 'https://www.googleapis.com/youtube/v3/search?part=snippet&key=' + $scope.model.dialogData.ytApi;
                var hasASearch = false;

                var req = {
                    method: 'GET',
                    url: ApiUrl,
                    headers: {
                        'Content-Type': "json",

                    },
                    umbIgnoreErrors: true //Tell Umbraco to ignore the errors - http://issues.umbraco.org/issue/U4-5588
                    //data: { test: 'test' }
                }

                if (searchTerm) {

                    req.url += '&q=' + searchTerm;

                    hasASearch = true;
                }

                if ($scope.model.searchType == "ytChannel") {
                    req.url += "&channelId=" + $scope.model.dialogData.ytChannelId
                    if (searchTerm == "") {
                        //We are getting the latest from our channel
                        req.url += "&order=date";
                    }
                    hasASearch = true;
                }


                $http(req).then(function successCallback(response) {
                    // this callback will be called asynchronously
                    // when the response is available
                    //console.log(response);
                    var data = response.data.items;

                    angular.forEach(response.data.items, function (value, key) {
                        value.embedUrl = $sce.trustAsResourceUrl("http://www.youtube.com/embed/" + value.id.videoId + "?controls=1");
                    });

                    $scope.results.yt = data;
                }, function errorCallback(response) {
                    // called asynchronously if an error occurs
                    // or server returns response with an error status.
                    console.log("err", response);
                    ShowError(response.data.error.errors[0].reason, response.data.error.errors[0].message)
                    notificationsService.error("YouTube Search Error", response.data.error.errors[0].reason);

                });

            }

            function ShowError(title, msg) {
                $scope.errorStr = {
                    title: title,
                    message: msg
                };
            }


            $scope.search = function () {
                $scope.errorStr = null;
                //console.log($scope.model.searchType)
                $scope.results.yt = [];
                $scope.results.vimeo = [];
                var hasASearch = false;
                var searchTerm = $scope.model.searchTerm;
                if ($scope.model.searchType == "ytChannel" || $scope.model.searchType == "ytAll") {
                    searchYouTube(searchTerm);
                    return;
                } else {
                    vimeoApi.search(searchTerm, $scope.model.searchType, function (data) {
                        $scope.results.vimeo = data;
                    });
                    return;
                }
            }


            $scope.selectVideo = function (video) {
                //console.log(video);
                var videoInfo = {};
                if (video.kind == "Vimeo") {
                    videoInfo.id = video.id;
                    videoInfo.type = "Vimeo";
                    videoInfo.url = "https://vimeo.com/" + video.id;
                    videoInfo.embedUrl = "https://player.vimeo.com/video/" + video.id;
                } else {
                    //YouTube
                    videoInfo.id = video.id.videoId;
                    videoInfo.type = "YouTube";
                    videoInfo.url = "https://www.youtube.com/watch?v=" + video.id.videoId;
                    videoInfo.embedUrl = $sce.trustAsResourceUrl("https://www.youtube.com/embed/" + video.id.videoId);
                }

                $scope.model.close();

                $scope.model.submit(videoInfo);
            };






            init();





            //Temp Code





        });