
angular.module('umbraco')
.controller('DigitalMomentum.Videolizer.Search',
function ($http, $scope) {
	$scope.model = {
		searchTerm: "",
		ytApi: $scope.dialogData.ytApi,
		ytChannelId: $scope.dialogData.ytChannelId
		//name: $scope.dialogData.name,
		//email: $scope.dialogData.email,
		//phonenumber: $scope.dialogData.phonenumber
	}

	$scope.results = []

	$scope.search = function () {
		var req = {
			method: 'GET',
			url: 'https://www.googleapis.com/youtube/v3/search?part=snippet&q=' + $scope.model.searchTerm + '&key=' + $scope.dialogData.ytApi,
			headers: {
				'Content-Type': "json"
			},
			//data: { test: 'test' }
		}
		if ($scope.model.ytChannelId != "") {
			req.url += "&channelId=" + $scope.model.ytChannelId
		}

		$http(req).then(function successCallback(response) {
			// this callback will be called asynchronously
			// when the response is available
			//console.log(response);
			$scope.results = response.data.items;
		}, function errorCallback(response) {
			// called asynchronously if an error occurs
			// or server returns response with an error status.
		});
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
});