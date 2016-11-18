'use strict';
app.controller('mainController', ['$showdown','$sce','$scope','creativeService','searchService','$location',
    function ($showdown,$sce,$scope,creativeService,searchService,$location) {

    $scope.creatives = [];
    $scope.tags = [];
    $scope.allCreatives = [];
    $scope.categories = [];

    $scope.mostPopularCreatives = [];
    $scope.mostRatedCreatives = [];
 
    $scope.sortType = 'id';
    $scope.sortReverse = false;

   var delimiter = 0;
   var init = true;

   var getInitialCreatives = function (){      
        creativeService.getPartialCreatives(0).then(function (results) {  
            $scope.allCreatives = results.data; 
            counter = 1;
        for(var i = 0; i< $scope.allCreatives.length; i++) {   
            $scope.allCreatives[i].popular = $scope.allCreatives[i].comments.length;      
            $scope.allCreatives[i].chapters = creativeService.sortChapters($scope.allCreatives[i]); 
        }   

          $scope.creatives =  results.data.slice(0,5);  
    
            
        }, function (error) {
            console.log(error);
        });
    };

    var getAdditionalCreatives = function (del){   
    console.log(del);   
        creativeService.getPartialCreatives(del).then(function (results) {          
            $scope.allCreatives = $scope.allCreatives.concat(results.data);  
            console.log($scope.allCreatives);         
        for(var i = 0; i< $scope.creatives.length; i++) {  
        
            $scope.creatives[i].popular = $scope.allCreatives[i].comments.length;      
            $scope.creatives[i].chapters = creativeService.sortChapters($scope.allCreatives[i]); 
        }   
            
        }, function (error) {
            console.log(error);
        });
    };
    
    var counter = 0;
    getInitialCreatives();

    creativeService.getMostPopularCreatives().then(function (results) {
        $scope.mostPopularCreatives = results.data;
        console.log(results.data);
        }, function (error) {
            console.log(error);
        });

    creativeService.getMostRatedCreatives().then(function (results) {
        $scope.mostRatedCreatives = results.data;
        }, function (error) {
            console.log(error);
        });

    creativeService.getCategories().then(function (results) {
        $scope.categories = results.data;
        }, function (error) {
            console.log(error);
        });

   

    $scope.loadMore = function() {   
        if($scope.creatives.length < $scope.allCreatives.length){
            $scope.creatives.push($scope.allCreatives[$scope.creatives.length]); 
       }   

       if ( $scope.creatives.length > $scope.allCreatives.length - 2 ){            
            if(counter < 6 && counter != 0) {
                getAdditionalCreatives(counter);
                counter++;     
            }   
        }
    }

    var x,i;
    creativeService.getAllTags().then(function (results) {
            var tags = results.data;          
             $scope.tags = tags.slice(0,19);            
        }, function (error) {
            console.log(error);
        });


    $scope.search = function(pattern){
         searchService.setSearchPattern(pattern);
        $location.path('/search/0');
    };

}]);