"use strict";

MusicHistory.controller('SongListController', [
	'$http', 
	'SongFactory',
	'$scope',

	function ($http, SongFactory, $scope) {

		$scope.songsList = [];
		$scope.test = "test variable";

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