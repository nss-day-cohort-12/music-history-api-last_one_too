"use strict";

MusicHistory.factory('AuthFactory', [

function () {
	
	let currentUser = null;

	return {
		getUser () {
			return currentUser;
		},
		setUser (user) {
			currentUser = user;
			console.log(`logged in as `, currentUser.data[0].CustomerName);
			console.log(`currentUser: `, currentUser);
			// console.log(`logged in as `, currentUser);
		}
	}
}


]);