"use strict";

MusicHistory.controller('SongListController', [
	'$http', 
	'SongFactory',
	'AuthFactory',
	'$scope',

	function ($http, SongFactory, AuthFactory, $scope) {

		$scope.songsList = [];
		$scope.test = "test variable";

		let user = AuthFactory.getUser().data[0];
		console.log(`user: `, user);

		$http
			.get(`http://localhost:5000/api/Customer?CustomerName=${user.CustomerName}`)
			.success(customer => {
				console.log(`customer: `, customer[0]);
				$scope.songsList = customer[0].FavoriteTracks});
			// $scope.$apply();

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