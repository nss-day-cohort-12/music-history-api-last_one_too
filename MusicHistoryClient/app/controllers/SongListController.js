"use strict";

MusicHistory.controller('SongListController', [
	'$http', 
	'songFactory',
	'$scope',

	function ($http, songFactory, $scope) {

		$scope.songsList = [];

		$http
			.get('http://localhost:5000/api/Customers')
			.success(songs => $scope.songsList = songs);

		$scope.deleteToy = function (id) {
			$http({
				method: "DELETE",
				url: `http://localhost:5000/api/Inventory/${id}`
			})
			.then(
				// success
				() => console.log(`Toy deleted`),
				// error
				() => console.log(`Toy not deleted`)
			);
		}
	}

]);