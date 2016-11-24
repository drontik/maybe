'use strict';
app.controller('homeController',
 ['$http','$scope','creativeService', 'localStorageService', '$location','authService','$window','$uibModal','$route','searchService',
  function ($http, $scope, creativeService, localStorageService, $location, authService, $window,$uibModal, $route,searchService) {

 	$scope.authentication = authService.authentication;
    $scope.userInfo = [];
    $scope.creatives = [];
    $scope.chapters = [];
    $scope.message = '';

    $scope.sortType = 'created';
    $scope.sortReverse = true;
    $scope.showLoading = false;

    $scope.searchCreatives = '';

    $scope.categories = [];
    $scope.currentCategory = "";
    $scope.creativeName = "";
    $scope.creativeDescription = ""
    $scope.tags = [];

    var newCreative = {
        name:"",
        categoryId: 0,      
        userName:"",
        Description:"",
        tags:{}   
    };


    if (authService.authentication.isAuth){
        authService.getProfileInfo().then(function(results){
                $scope.userInfo = results.data;              
        });

    	creativeService.getCreatives().then(function (results) {        
            $scope.creatives = results.data;           
            for(var i = 0; i< $scope.creatives.length; i++) {       
                $scope.creatives[i].popular = $scope.creatives[i].comments.length;           
                $scope.creatives[i].chapters = creativeService.sortChapters($scope.creatives[i]); 
            }   
        }, function (error) {
            console.log(error);            
        });
    }
    
    $scope.search = function(name){
    	   searchService.setSearchPattern(name);
        $location.path('/search/0');
    };
    

    $scope.deleteCreative = function (id) {     
        var result = $window.confirm('are you absolutely sure you want to delete?');
        if (result) {
            creativeService.deleteCreative(id).then(function(results){
                $scope.creatives = results.data;
            });
        }        
    };

    $scope.loadTags = function($query) {
        return $scope.recievedTags.filter(function(tag) {
        return tag.name.toLowerCase().indexOf($query.toLowerCase()) != -1;
      });;
    };

    creativeService.getCategories().then(function (results) {
        $scope.categories = results.data;
        $scope.currentCategory = results.data[0];
    }, function (error) {
        console.log(error);
        $scope.savedSuccessfully = false;
        $scope.showLoading = false; 
        $scope.message = error.data.message;
    });

    creativeService.getAllTags().then(function(results){
        $scope.recievedTags = results.data;
    }, function(error){
        console.log(error);
        $scope.savedSuccessfully = false;
        $scope.showLoading = false; 
        $scope.message = error.data.message;
    });


    
    $scope.createCreative = function(formData){
        $scope.showLoading = true;
        newCreative.userName = localStorageService.get('authorizationData').userName;
        newCreative.categoryId = $scope.currentCategory.id;
        newCreative.name = $scope.creativeName;
        newCreative.Description = $scope.creativeDescription;
        newCreative.tags = $scope.tags;
        console.log(newCreative);      
        creativeService.createCreative(newCreative).then(function(results){
            $scope.creatives = results.data;
            $scope.savedSuccessfully = true;
            $scope.showLoading = false; 
            $scope.message = $scope.translation.SAVE_SUCC;
            $( globalModal ).toggleClass('global-modal-show');
        }, function(response){
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
   }; 

   $scope.showCreative = function(id){
        $location.path('/show/' + id);
   }

     var globalModal = $('.global-modal');
     $(".btn-trigger").on("click", function (e) {
      e.preventDefault();
      $( globalModal ).toggleClass('global-modal-show');
    });
    $( ".overlay" ).on( "click", function() {
      $( globalModal ).toggleClass('global-modal-show');
    });
    $( ".global-modal_close" ).on( "click", function() {
      $( globalModal ).toggleClass('global-modal-show');
    });
    $(".mobile-close").on("click", function(){
      $( globalModal ).toggleClass('global-modal-show');
    });

    
}]);