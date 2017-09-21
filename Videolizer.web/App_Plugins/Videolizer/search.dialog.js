
angular.module('umbraco')
.controller('DigitalMomentum.Videolizer.Search',
		function ($http, $scope) {
			$scope.hideLabel = "true"; //Hides the modal Dialog label at the top of the page
		
	$scope.model = {
		searchTerm: "",
		ytApi: $scope.dialogData.ytApi,
		ytChannelId: $scope.dialogData.ytChannelId,
		vimeoApi: $scope.dialogData.vimeoApi,
		searchType: $scope.dialogData.defaultSearchType
	}

	$scope.results = {
		yt: [],
		vimeo: []
	}

	function init() {
		$scope.search(); //Run defaut search to get list of latest videos

		//Select the appropriate Default Radio
		//if ($scope.model.ytApi) {
		//	if ($scope.model.ytChannelId) {
		//		$scope.searchType = "ytChannel";
		//	} else {
		//		$scope.searchType = "ytAll";
		//	}
			
		//}
	}

	function searchYouTube(searchTerm) {
		var ApiUrl = 'https://www.googleapis.com/youtube/v3/search?part=snippet&key=' + $scope.dialogData.ytApi
		var hasASearch = false;

		var req = {
			method: 'GET',
			url: ApiUrl,
			headers: {
				'Content-Type': "json",
				
			},
			umbIgnoreErrors:true
			//data: { test: 'test' }
		}

		if (searchTerm) {
			
			req.url += '&q=' + searchTerm;
			
			hasASearch = true;
		}

		if ($scope.model.searchType == "ytChannel") {
			req.url += "&channelId=" + $scope.model.ytChannelId
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
			$scope.results.yt = response.data.items;
		}, function errorCallback(response) {
			// called asynchronously if an error occurs
			// or server returns response with an error status.
		});

	}

	function searchVimeo(searchTerm) {
		var ApiUrl = 'https://api.vimeo.com/';
		if ($scope.model.searchType == "vimeoChannel"){
			//Get from my channel
			ApiUrl += "me/";
			hasASearch = true; //Auto load my videos
		}
		ApiUrl += "videos?per_page=8&access_token=" + $scope.dialogData.vimeoApi;


		var req = {
			method: 'GET',
			url: ApiUrl,
			headers: {
				'Content-Type': "json",
				
			},
			umbIgnoreErrors: true
			//data: { test: 'test' }
		}

		if (searchTerm) {
			req.url += '&query=' + searchTerm;
			hasASearch = true;
		}



		$http(req).then(function successCallback(response) {
			// this callback will be called asynchronously
			// when the response is available
			console.log(response);

			var regExp = /\/(\d+)($|\/)/;

			angular.forEach(response.data.data, function (video, key) {
				var match = video.uri.match(regExp);
				if (match) {
					video.kind = "Vimeo";
					video.id = match[1];
				}
			});




			$scope.results.vimeo = response.data.data;
		}, function errorCallback(response) {
			// called asynchronously if an error occurs
			// or server returns response with an error status.
		});
	}

	$scope.search = function () {
		console.log($scope.model.searchType)
		$scope.results.yt = [];
		$scope.results.vimeo = [];
		var hasASearch = false;
		var searchTerm = $scope.model.searchTerm;
		if ($scope.model.searchType == "ytChannel" || $scope.model.searchType == "ytAll") {
			searchYouTube(searchTerm);
			return;
		} else {
			searchVimeo(searchTerm);
			return;
		}
	}


	$scope.selectVideo = function (video) {
		console.log(video);
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
			videoInfo.embedUrl = "https://www.youtube.com/embed/" + video.id.videoId;
		}
		console.log(videoInfo);
		$scope.submit(videoInfo);
	}






	init();

	
});