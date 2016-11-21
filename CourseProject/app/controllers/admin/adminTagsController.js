'use strict';
app.controller('adminTagsController',  ['$http','$scope', '$location', 'authService','adminService', 'creativeService', '$window',
 function ($http, $scope, $location, authService, adminService,creativeService, $window) {

 $scope.tags = [];

 $scope.selectedTag = {};

 $scope.newTag = '';

 $scope.showLoading = false;

 $scope.savedSuccessfully = true;

 $scope.message = '';	
  $scope.message1 = '';	

   	if (!authService.authentication.isAuth){
  		$location.path('/home');
  	}
  	else {
  		authService.getProfileInfo().then(function(results){
	        $scope.userInfo = results.data;       
	        $scope.isAdmin = $scope.userInfo.isAdmin;
	        
	        if(!$scope.isAdmin){
	        	$location.path('/home');
	        }   
	        else{
	        	adminService.getAllTags().then(function(results){
				$scope.tags = results.data;
				console.log(results.data);
				});	
			}
    	}, function(error){
    	console.log(error);
    	});
  	}
 
	$scope.editTag = function(id){
		console.log(id);
		$scope.message = '';
		$scope.message1 = '';
		for (var i = 0; i < $scope.tags.length; i++) {
			if($scope.tags[i].id === id){
				$scope.selectedTag = $scope.tags[i];
				break;
			}
		}
		$( globalModal).toggleClass('global-modal-show');
	};

	$scope.addTag = function(){
		if($scope.newTag.length > 0){
			$scope.selectedTag.name = $scope.newTag;
			$scope.selectedTag.id = 0;
			$scope.message1 = '';
			
			creativeService.saveTag($scope.selectedTag).then(function(results){

            $scope.tags = results.data;
			$scope.savedSuccessfully = true;
			$scope.showLoading = false;	
			$scope.message1 = "Saved successfully"
			}, function(error){
				$scope.savedSuccessfully = false;
				$scope.message1 = error.data.message;
				$scope.showLoading = false;	
			});		
		}	
		else{
			 $scope.savedSuccessfully = false;
			 $scope.message1 = "Name must be more than 0 characters!"
		}
	};

	$scope.saveTag = function(){
		var result = $window.confirm('Are you sure ?');
        if (result) {
        	$scope.showLoading = true;	
            creativeService.saveTag($scope.selectedTag).then(function(results){
            $scope.tags = results.data;
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

	$scope.deleteTag = function(id){		
		var result = $window.confirm('Are you absolutely sure you want to delete?');
        if (result) {
        	$scope.showLoading = true;	
            creativeService.deleteTag(id).then(function(results){
            $scope.tags = results.data;
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