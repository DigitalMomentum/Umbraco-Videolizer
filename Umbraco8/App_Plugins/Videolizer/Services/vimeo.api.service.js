angular.module('umbraco')
	.service('vimeoApi', function ($http, notificationsService) {
		var authToken = null;
		var clientId = null;
		var clientSecret = null;
		var userId = null;
		this.ApiUrl = 'https://api.vimeo.com/';


		this.init = function (ClientId, ClientSecret, UserId) {
			//console.log("UserId", UserId)
			clientId = ClientId;
			clientSecret = ClientSecret;
			userId = UserId;
			//console.log(clientId)
		}

		this.GetAuthToken = function (callback) {
			//console.log(clientId)
			if (authToken != null) {
				return callback(authToken); 
			}
			var req = {
				method: 'POST',
				url: this.ApiUrl + "/oauth/authorize/client?grant_type=client_credentials",
				headers: {
					'Content-Type': "json",
					'Authorization': "basic " + btoa(clientId + ":" + clientSecret)
				},
				umbIgnoreErrors: true, //Tell Umbraco to ignore the errors - http://issues.umbraco.org/issue/U4-5588
				data: {
					//"grant_type": 'client_credentials'
				}
			}

			$http(req).then(function successCallback(response) {
				//console.log(response)
				authToken = response.data.access_token;
				return callback(authToken);
			});


			
		}
		///SearchType = 'vimeoChannel', 'vimeoAll'
		this.search = function(searchTerm, searchType, callback) {
			vimeoApi = this;


			this.GetAuthToken(function (AuthToken) {

				var ApiUrl = vimeoApi.ApiUrl;// 'https://api.vimeo.com/';
				if (searchType == "vimeoChannel") {
					//Get from my channel
					//ApiUrl += "me/";
					ApiUrl += "users/" + userId + "/"
					hasASearch = true; //Auto load my videos
				}
				ApiUrl += "videos?per_page=8&access_token=" + AuthToken;


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
					req.url += '&query=' + searchTerm;
					hasASearch = true;
				}


				$http(req).then(function successCallback(response) {
					// this callback will be called asynchronously
					// when the response is available
					//console.log(response);

					var regExp = /\/(\d+)($|\/)/;

					angular.forEach(response.data.data, function (video, key) {
						var match = video.uri.match(regExp);
						if (match) {
							video.kind = "Vimeo";
							video.id = match[1];
						}
					});


					return callback(response.data.data)

				}, function errorCallback(response) {
					// called asynchronously if an error occurs
					// or server returns response with an error status.
					console.log("err", response);
					notificationsService.error("Vimeo Search Error", response.data.error);

				});

			})

		}
});