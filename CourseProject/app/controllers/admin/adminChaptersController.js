'use strict';
app.controller('adminChaptersController',  ['$http','$scope', '$location', 'authService','adminService', 'creativeService', '$window',
 function ($http, $scope, $location, authService, adminService,creativeService, $window) {

	$scope.chapters = [];

 	$scope.selectedChapter = {};

	$scope.showLoading = false;

 	$scope.savedSuccessfully = true;

 	$scope.message = '';	

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
	        	adminService.getAllChapters().then(function(results){
				$scope.chapters = results.data;
				console.log(results.data);
				});	
			}	    
			
	        
    	}, function(error){
    	console.log(error);
    	});	        
  	

    	    
  	}
 
	$scope.editChapter = function(id){
		console.log(id);
		$scope.message = '';
		for (var i = 0; i < $scope.chapters.length; i++) {
			if($scope.chapters[i].id === id){
				$scope.selectedChapter = $scope.chapters[i];
				break;
			}
		}
		$( globalModal).toggleClass('global-modal-show');
	};
	
	$scope.saveChapter = function(){
		var result = $window.confirm('Are you sure ?');
        if (result) {
        	$scope.showLoading = true;	
        	$scope.selectedChapter.text = $scope.selectedChapter.body;
            creativeService.saveChapter($scope.selectedChapter).then(function(results){	          
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

	$scope.deleteChapter = function(){		
		var result = $window.confirm('Are you absolutely sure you want to delete?');
        if (result) {
        	$scope.showLoading = true;	
            adminService.deleteChapter($scope.selectedChapter.id).then(function(results){
            $scope.chapters = results.data;
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