'use strict';
app.controller('adminCommentsController',  ['$http','$scope', '$location', 'authService','adminService', 'creativeService', '$window',
 function ($http, $scope, $location, authService, adminService,creativeService, $window) {

 $scope.comments = [];

 $scope.selectedComment = {};

 $scope.showLoading = false;

 $scope.savedSuccessfully = true;

 $scope.message = '';	

 if (!authService.authentication.isAuth){
  		$location.path('/mian');
  	}
  	else {
  		authService.getProfileInfo().then(function(results){
	        $scope.userInfo = results.data;       
	        $scope.isAdmin = $scope.userInfo.isAdmin;
	        
	        if(!$scope.isAdmin){
	        	$location.path('/mian');
	        }   
	        else {
	        	adminService.getAllComments().then(function(results){
				$scope.comments = results.data;
				console.log(results.data);
				});
			}
    	}, function(error){
    	console.log(error);
    	});
  	}
 
	$scope.editComment = function(id){
		$scope.message = '';

		for (var i = 0; i < $scope.comments.length; i++) {
			if($scope.comments[i].id === id){
				$scope.selectedComment = $scope.comments[i];
				break;
			}
		}
		$( globalModal).toggleClass('global-modal-show');
	};

	$scope.deleteComment = function(){		
		var result = $window.confirm('Are you absolutely sure you want to delete?');
        if (result) {
        	$scope.showLoading = true;	
            adminService.deleteComment($scope.selectedComment.id).then(function(results){
            $scope.comments = results.data;
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

	$scope.saveComment = function(){
		var result = $window.confirm('Are you sure ?');
        if (result) {
        	$scope.showLoading = true;	
            creativeService.saveComment($scope.selectedComment).then(function(results){
            $scope.comments = results.data;
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