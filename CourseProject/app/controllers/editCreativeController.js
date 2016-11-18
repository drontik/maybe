'use strict';
app.controller('editCreativeController',
 ['$scope','$route','$routeParams','creativeService','$window','$location','localStorageService','authService','$timeout',
	function ($scope, $route, $routeParams,creativeService, $window, $location,localStorageService, authService,$timeout) {
 		
 		$scope.confirm = false;
		var creativeId = $routeParams.Id; 	
		var errors = [];
		var currentUserInfo = {};
		$scope.creativeId = creativeId;
 		$scope.creative = {}; 	
 		$scope.chapters = [];  	
		$scope.recievedTags = [];
		$scope.tags = [];

		$scope.savedSuccessfully = false;
		$scope.showLoading = true;	
		$scope.message = '';

		var currentUserName = '';

	    if ($scope.authentication.isAuth) {
	        currentUserName = localStorageService.get('authorizationData').userName;
	        authService.getProfile(currentUserName).then(function(results) {
	            currentUserInfo = results.data;
	            creativeService.getCreative(creativeId).then(function(results) {
	                $scope.showLoading = false;
	                $scope.creative = results.data;
	                $scope.chapters = creativeService.sortChapters(results.data);
	                $scope.tags = $scope.creative.tags;
	                console.log($scope.creative.userName);
	                console.log(currentUserName);
	                if (currentUserInfo.isAdmin == false) {
	                    console.log("inIF")
	                    if ($scope.creative.userName.toLowerCase() !== currentUserName.toLowerCase()) {
	                        $location.path('/home');
	                    }
	                }
	            }, function(response) {
	                $scope.savedSuccessfully = false;
	                for (var key in response.data.modelState) {
	                    for (var i = 0; i < response.data.modelState[key].length; i++) {
	                        errors.push(response.data.modelState[key][i]);
	                    }
	                }
	                $scope.message = $scope.translation.REG_ERR + errors.join(' ');

	                $location.path('/NotFound');
	            });

	        });
	    }

	    $scope.deleteCreative = function() {
	        $scope.showLoading = false;
	        $('#myModal').modal('hide');

	        creativeService.deleteCreative(creativeId).then(function(results) {

	            $scope.chapters = results.data;
	            $scope.savedSuccessfully = true;
	            $scope.showLoading = false;
	            $scope.message = $scope.translation.DEL_SUCC;
	            startTimer();

	            console.log(results.data);
	            //	$location.path("/home");
	        }, function(response) {
	            $scope.savedSuccessfully = false;
	            $scope.showLoading = false;
	            var errors = [];
	            for (var key in response.data.modelState) {
	                for (var i = 0; i < response.data.modelState[key].length; i++) {
	                    errors.push(response.data.modelState[key][i]);
	                }
	            }
	            $scope.message = $scope.translation.REG_ERR + errors.join(' ');
	        });
	    }

	    $scope.saveCreative = function () {
    		$scope.showLoading = true;
    		setChpatersPositions();
    		$scope.creative.chapters = $scope.chapters;    
    		creativeService.updateCreative($scope.creative).then(function(results){
	    		$scope.savedSuccessfully = true;
				$scope.showLoading = false;	
				$scope.message = $scope.translation.SAVE_SUCC;
		        console.log(results);

			}, function(response){
			    $scope.savedSuccessfully = false;
				$scope.showLoading = false;	
				  for (var key in response.data.modelState) {
                 for (var i = 0; i < response.data.modelState[key].length; i++) {
                     errors.push(response.data.modelState[key][i]);
                 }
             }
             $scope.message = $scope.translation.REG_ERR + errors.join(' ');
			});
    	}

		creativeService.getAllTags().then(function(results){
	        $scope.recievedTags = results.data;
		    }, function(response){
		        $scope.savedSuccessfully = false;
				$scope.showLoading = false;	

				for (var key in response.data.modelState) {
                 for (var i = 0; i < response.data.modelState[key].length; i++) {
                     errors.push(response.data.modelState[key][i]);
                 }
             }
             $scope.message = $scope.translation.REG_ERR + errors.join(' ');
		  	});

		$scope.loadTags = function($query) {
       		return $scope.recievedTags.filter(function(tag) {
	        	return tag.name.toLowerCase().indexOf($query.toLowerCase()) != -1;
	      		});
    	};    	



		var setChpatersPositions = function(){
			for (var i = 0; i < $scope.chapters.length; i++) {
				$scope.chapters[i].number = i + 1;
			}
		};
  
	 $scope.sortableOptions = {
	  	update: function(e, ui) {
		    var logEntry = $scope.chapters.map(function(i){
		    	return i.number;
		    }).join(', ');
	
	    },
	    stop: function(e, ui) {	   
	    	console.log($scope.chapters);
	      var logEntry = $scope.chapters.map(function(i){
	        return i.number;
	      }).join(', ');	 
	    }
	  };


	$scope.close = function(){
		$( globalModal ).toggleClass('global-modal-show');
	};

   var startTimer = function () {
        var timer = $timeout(function () {
            $timeout.cancel(timer);
            $location.path('/home');
        }, 500);
    };

}]);