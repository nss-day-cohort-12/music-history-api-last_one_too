'use strict';

let MusicHistory = angular.module('MusicHistory', [
	'ngRoute'
])
.constant('MHAPI', "http://localhost:5000/api/");

MusicHistory.config(['$routeProvider', 
  function ($routeProvider) {
	$routeProvider
		.when('/', {
			templateUrl: 'partials/songList.html',
			controller: 'SongListController'
		})
		.when('/register', {
			templateUrl: 'partials/register.html',
			controller: 'RegisterController'
		})
    .when("/songs/:songId", {
      templateUrl: "partials/song-brief.html",
      controller: "SongDetailController",
      // resolve: { isAuth }
    })
		.otherwise('/');
  }
]);