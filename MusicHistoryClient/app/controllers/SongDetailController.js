"use strict";

MusicHistory.controller("SongDetailCtrl",
[
  "$scope",
  "$routeParams",
  "$http",
  "$location",
  "songFactory",

  function ($scope, $routeParams, $http, $location, songFactory) {

    // Default properties for bound variables
    $scope.songs = [];
    $scope.selectedSong = {};

    // Invoke the promise that reads from Firebase
    songFactory().then(

      // Handle resolve() from the promise
      songCollection => {
        Object.keys(songCollection).forEach(key => {
          songCollection[key].id = key;
          $scope.songs.push(songCollection[key]);
        });

        $scope.selectedSong = $scope.songs.filter(song => song.id === $routeParams.songId)[0];
        console.log(`$scope.selectedSong: `, $scope.selectedSong);
      },

      // Handle reject() from the promise
      err => console.log(err)
    );

    /*
      This function is bound to an ng-click directive
      on the button in the view
    */
    $scope.deleteSong = () => $http
        // using firebaseURL variable to declare path, using the variable declared on app.js ($routeParams.songId) to specify song
        .delete(`${firebaseURL}/songs/${$routeParams.songId}.json`)
        .then(() => $location.url("/"));
  }
]);
