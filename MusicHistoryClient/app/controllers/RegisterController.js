"use strict";

MusicHistory.controller('RegisterController', [
	'$http', 
	'$scope',
	'AuthFactory',

	function ($http, $scope, authFactory) {

		// main OAuth function
		$scope.twitterOauth = function () {
			// OAuth / Twitter API integration key (accessed by TryAuth in OAuth.io MusicHistory Twitter Integrated APIs section)
			// 
			OAuth.initialize('oxGLgmZ8RJRqcuPi1WXHTDNAiwY');

			OAuth.popup('twitter').done(function(result) {
			    console.log(result)

				result.me().done(function(data) {
				    // do something with `data`, e.g. print data.name
				    console.log('DATA: ', data);

				    // POSTing resulting user info (new JSON stringified object) to database hooked to our API
				    $http({
				    	// designated API endpoint
				    	url: "http://localhost:5000/api/Customer",
				    	method: "POST",
				    	data: JSON.stringify({
				    		username: data.alias,
				    		location: data.location,
				    		emailAddress: data.email,
				    		createdDate: new Date()
				    	})
				    }).then(
				    response => {
				    	let customer = response.data[0];
				    	authFactory.setUser(customer);
				    	console.log("resolve fired", customer);
				    },
				    response => {
				    	console.log("reject fired", response);

				    	// Geek has already been created
				    	if (response.status === 409) {
				    		$http
				    			.get(`http://localhost:5000/api/Geek?username=${data.alias}`)
				    			.then(
				    				response => {
				    					let customer = response.data[0];
				    					console.log("Found the Geek", customer);
				    					authFactory.setUser(customer)
				    				},
				    				response => console.log("Could not find that Geek", response)
				    			)
				    	}

				    }
				    )
				})
			}).fail(function (a,b,c) {
				console.log(arguments);
			});
		};
	}
]);

