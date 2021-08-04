angular.module('umbraco')
    .service('videolizerApi', function ($http, notificationsService) {

        this.search = function (searchTerm, provider, myVideos, channelId, itemsPerPage, callback) {


            var ApiUrl = "/Umbraco/BackOffice/Api/Search/Query?providerType=" + provider;

            ApiUrl += "&itemsPerPage=" + itemsPerPage;
            ApiUrl += '&query=' + searchTerm;
            ApiUrl += '&myVideos=' + myVideos;
            ApiUrl += '&channelId=' + channelId;

            var req = {
                method: 'GET',
                url: ApiUrl,
                headers: {
                    'Content-Type': "json",

                },
                umbIgnoreErrors: true //Tell Umbraco to ignore the errors - http://issues.umbraco.org/issue/U4-5588
            }

            $http(req).then(function successCallback(response) {
                // this callback will be called asynchronously
                // when the response is available
                console.log(response);

                return callback(response.data)

            }, function errorCallback(response) {
                // called asynchronously if an error occurs
                // or server returns response with an error status.
                console.log("err", response);
                notificationsService.error("Search Error", response.data.error);

            });

		}




		this.GetVideoByUrl = function (url, callback) {


			var ApiUrl = "/Umbraco/BackOffice/Api/Search/GetVideoByUrl?url=" + url;

			var req = {
				method: 'GET',
				url: ApiUrl,
				headers: {
					'Content-Type': "json"
				},
				umbIgnoreErrors: true //Tell Umbraco to ignore the errors - http://issues.umbraco.org/issue/U4-5588
			};

			$http(req).then(function successCallback(response) {
				// this callback will be called asynchronously
				// when the response is available
				console.log(response);

				return callback(response.data);

			}, function errorCallback(response) {
				// called asynchronously if an error occurs
				// or server returns response with an error status.
				
				notificationsService.error("Search Error", response.data.error);

			});

		}

    });