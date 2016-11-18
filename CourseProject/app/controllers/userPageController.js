'use strict';
app.controller('userPageController',
 ['$http','$scope','creativeService', 'localStorageService', '$location','authService','$window','$uibModal','$route','$routeParams','searchService',
  function ($http, $scope, creativeService, localStorageService, $location, authService, $window,$uibModal, $route, $routeParams,searchService) {

    var userName = $routeParams.username; 

    var authData = localStorageService.get('authorizationData');     
    
    if( authData != undefined){
        authService.getProfile(authData.userName).then(function(results){
            $scope.currentUserInfo = results.data;
        }); 
    }

 	$scope.authentication = authService.authentication;
    $scope.userInfo = [];
    $scope.creatives = [];
    $scope.sortType = 'created';
    $scope.sortReverse = true;
    $scope.searchCreatives = '';
    $scope.tags = [];

    authService.getProfile(userName).then(function(results){
            $scope.userInfo = results.data;
            console.log()              
        });

    creativeService.getCreativesByName(userName).then(function (results) {        
            $scope.creatives = results.data;
        }, function (error) {
            $location.path('/NotFound')
            console.log(error);            
        });
    
   
    $scope.showCreative = function(id) {
          $location.path('/show/' + id);
      }

      $scope.search = function(pattern){
         searchService.setSearchPattern(pattern);
        $location.path('/search/0');
    };
    
}]);