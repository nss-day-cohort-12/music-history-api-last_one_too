'use strict';

MusicHistory.factory("SongFactory", ($q, $http) => {

	// should attempt to store songs here

  return {

    getSongs(userId) { 
	    $q((resolve, reject) => // Return a promise for our async XHR
	      $http
	        // .get(firebaseURL + "/songs/.json")
	        .get(MHAPI + `songs/${userId}/.json`)
	        .success(
	          songCollection => resolve(songCollection),
	          error => reject(error)
	        )
    )},
    getSong(userId, songId) {
	    $q((resolve, reject) => // Return a promise for our async XHR
	      $http
	        .get(MHAPI + `songs/${userId}/${songId}/.json`)
	        .success(
	          song => resolve(song),
	          error => reject(error)
	        )
    )},


  } 

});