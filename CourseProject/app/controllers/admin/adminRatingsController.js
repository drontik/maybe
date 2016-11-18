'use strict';
app.controller('adminRatingsController',  ['$http','$scope', '$location', 'authService','adminService', 'creativeService', '$window',
 function ($http, $scope, $location, authService, adminService,creativeService, $window) {

 $scope.ratings = [];

 $scope.selectedRating = {};

 $scope.showLoading = false;

 $scope.savedSuccessfully = true;

 $scope.message = '';	
 
	$scope.editRating = function(id){
		$scope.message = '';
		console.log(id);
		for (var i = 0; i < $scope.ratings.length; i++) {
			if($scope.ratings[i].id === id){
				$scope.selectedRating = $scope.ratings[i];
				break;
			}
		}
		$( globalModal).toggleClass('global-modal-show');
	};

	$scope.deleteRating = function(){		
		var result = $window.confirm('Are you absolutely sure you want to delete?');
        if (result) {
        	$scope.showLoading = true;	
            adminService.deleteRating($scope.selectedRating.id).then(function(results){
            $scope.ratings = results.data;
			$scope.savedSuccessfully = true;
			$scope.showLoading = false;	
			$scope.message = "Deleted successfully"
		}, function(error){
			$scope.savedSuccessfully = false;
			$scope.message = error.data.message;
			$scope.showLoading = false;	
		});	
        }    
	};

	$scope.saveRating = function(){
		var result = $window.confirm('Are you sure ?');
        if (result) {
        	$scope.showLoading = true;	
            creativeService.saveRating($scope.selectedRating).then(function(results){
            $scope.ratings = results.data;
			$scope.savedSuccessfully = true;
			$scope.showLoading = false;	
			$scope.message = "Saved successfully"
			}, function(error){
				$scope.savedSuccessfully = false;
				$scope.message = error.data.message;
				$scope.showLoading = false;	
			});
        }        
	};

	adminService.getAllRatings().then(function(results){
		$scope.ratings = results.data;
		console.log(results.data);
	});
	

	$scope.close = function(){
		$( globalModal ).toggleClass('global-modal-show');
	}

	var globalModal = $('.global-modal');
    $( ".trigger" ).on( "click", function(e) {
      e.preventDefault();
      $( globalModal ).toggleClass('global-modal-show');
    });
    $( ".overlay" ).on( "click", function() {
      $( globalModal ).toggleClass('global-modal-show');
    });


}]);