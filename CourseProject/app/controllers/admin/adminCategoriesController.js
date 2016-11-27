'use strict';
app.controller('adminCategoriesController',  ['$http','$scope', '$location', 'authService','adminService', 'creativeService', '$window',
 function ($http, $scope, $location, authService, adminService,creativeService, $window) {

 $scope.categories = [];

 $scope.selectedCategory = {};

 $scope.newCategory = '';

 $scope.showLoading = false;

 $scope.savedSuccessfully = true;

 $scope.message = '';	

 $scope.message1 = '';	

   	if (!authService.authentication.isAuth){
  		$location.path('/main');
  	}
  	else {
  		authService.getProfileInfo().then(function(results){
	        $scope.userInfo = results.data;       
	        $scope.isAdmin = $scope.userInfo.isAdmin;
	        
	        if(!$scope.isAdmin){
	        	$location.path('/main');
	        }   
	        else{
	        	creativeService.getCategories().then(function(results){
				$scope.categories = results.data;
				console.log(results.data);
				});	
			}	    
			
	        
    	}, function(error){
    	console.log(error);
    	});	        
  	

    	    
  	}
 
	$scope.editCategory = function(id){
		console.log(id);
		$scope.message = '';
		for (var i = 0; i < $scope.categories.length; i++) {
			if($scope.categories[i].id === id){
				$scope.selectedCategory = $scope.categories[i];
				break;
			}
		}
		$( globalModal).toggleClass('global-modal-show');
	};

	$scope.addCategory = function(){
		if($scope.newCategory.length > 3){
			$scope.selectedCategory.name = $scope.newCategory;
			$scope.selectedCategory.id = 0;
			$scope.message = '';
			$scope.message1 = '';
			$scope.showLoading = true;
			
			adminService.saveCategory($scope.selectedCategory).then(function(results){
            $scope.categories = results.data;
			$scope.savedSuccessfully = true;
			$scope.showLoading = false;	
			$scope.message1 = "Added successfully"
			}, function(error){
				$scope.savedSuccessfully = false;
				$scope.message = error.data.message;
				$scope.showLoading = false;	
			});		
		}	
		else{
			 $scope.savedSuccessfully = false;
			 $scope.message1 = "Name must be more than 3 characters!"
		}
	};

	$scope.saveCategory = function(){
		var result = $window.confirm('Are you sure ?');
        if (result) {
        	$scope.showLoading = true;	
            adminService.saveCategory($scope.selectedCategory).then(function(results){
            $scope.categories = results.data;
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

	$scope.deleteCategory = function(id){		
		var result = $window.confirm('Are you absolutely sure you want to delete?');
        if (result) {
        	$scope.showLoading = true;	
            adminService.deleteCategory(id).then(function(results){
            $scope.categories = results.data;
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