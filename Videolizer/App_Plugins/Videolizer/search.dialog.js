
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
				'Content-Type': "json"
			},
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
		
	}

	$scope.search = function () {
		console.log($scope.model.searchType)
		var hasASearch = false;
		var searchTerm = $scope.model.searchTerm;
		if ($scope.model.searchType == "ytChannel" || $scope.model.searchType == "ytAll") {
			searchYouTube(searchTerm);
			return;
		} else {

		}

		//var ApiUrl = 'https://www.googleapis.com/youtube/v3/search?part=snippet&key=' + $scope.dialogData.ytApi
		//if (searchFlavour == "vimeo"){
		//	if ($scope.model.searchType == "vimeoChannel") {
		//		ApiUrl = 'https://api.vimeo.com/me/videos';
		//		hasASearch = true;
		//	} else {
		//		ApiUrl = 'https://api.vimeo.com/videos';
		//	}
		//	ApiUrl += '?access_token=' + $scope.dialogData.vimeoApi;
		//}
		
			

		//var req = {
		//	method: 'GET',
		//	url: ApiUrl,
		//	headers: {
		//		'Content-Type': "json"
		//	},
		//	//data: { test: 'test' }
		//}
		//if (searchTerm != "") {
		//	if (searchFlavour == "yt") {
		//		req.url += '&q=' + searchTerm;
		//	} else {
		//		req.url += '&query=' + searchTerm;
		//	}
		//	hasASearch = true;
		//}


		//if ($scope.model.searchType == "ytChannel") {
		//	req.url += "&channelId=" + $scope.model.ytChannelId
		//	if (searchTerm == "") {
		//		//We are getting the latest from our channel
		//		req.url += "&order=date";
		//	}
		//	hasASearch = true;
		//} 

		//$http(req).then(function successCallback(response) {
		//	// this callback will be called asynchronously
		//	// when the response is available
		//	//console.log(response);
		//	$scope.results.yt = response.data.items;
		//}, function errorCallback(response) {
		//	// called asynchronously if an error occurs
		//	// or server returns response with an error status.
		//});
	}


	$scope.selectVideo = function (video) {
		//console.log(video);
		var vidId = video.id.videoId;
		$scope.submit(
		{
				url: "https://www.youtube.com/watch?v=" + vidId,
				id: vidId,
				embedUrl: "https://www.youtube.com/embed/" + vidId,
				type: "YouTube"
			}
		);
	}






	init();

	
});