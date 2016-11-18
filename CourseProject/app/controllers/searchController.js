'use strict';
app.controller('searchController', ['$routeParams','$scope', '$location','searchService',
    function ($routeParams,$scope, $location, searchService) {
  
    $scope.results = [];
    $scope.loading = false;
    $scope.message = '';
    $scope.savedSuccessfully = false;  

    var categoryId = $routeParams.categoryId;
    
    if(categoryId != 0){
        searchService.searchByCategory(categoryId).then(function(results){
            $scope.results = results.data;
            $scope.loading = false;
            $scope.savedSuccessfully = true;          
            $scope.message = $scope.translation.CREATIVES_FOUND + results.data.length;
        }, function(response){
            console.log(response);
            $scope.savedSuccessfully = false;      
            $scope.loading = false;    
             var errors = [];
             for (var key in response.data.modelState) {
                 for (var i = 0; i < response.data.modelState[key].length; i++) {
                     errors.push(response.data.modelState[key][i]);
                 }
             }
             $scope.message = $scope.translation.REG_ERR + errors.join(' ');
         
        });
    }

    $scope.pattern = searchService.getSearchPattern();

    $scope.searchModel = {
        pattern:"",
        chapterName:true,
        chapterText:true,
        creativeName:true,
        creativeDescription:true,
        tagName:true,
        commentText:true,
        commentAuthor:true,
        creativeAuthor:true
    }; 

    $scope.conditionList = [
    { name: 'Chapter Names',    selected: true },
    { name: 'Chapter Texts',   selected: true },
    { name: 'Creative Names',     selected: true },
    { name: 'Creative Desc.', selected: true },
    { name: 'Tags',    selected: true },
    { name: 'Comments',   selected: true },
    { name: 'Comment Authors',     selected: true },
    { name: 'Creative Authors', selected: true }
  ]; 

   	var serachPattern = searchService.getSearchPattern();
   
   	if (serachPattern) {
   		$scope.searchModel.pattern = serachPattern;
   		$scope.loading = true;
   		searchService.search($scope.searchModel).then(function(results){
   			$scope.results = results.data;
   			$scope.loading = false;
            $scope.savedSuccessfully = true;          
            $scope.message = $scope.translation.CREATIVES_FOUND + results.data.length;
   		}, function(response){
            console.log(response);
            $scope.savedSuccessfully = false;      
            $scope.loading = false;    
             var errors = [];
             for (var key in response.data.modelState) {
                 for (var i = 0; i < response.data.modelState[key].length; i++) {
                     errors.push(response.data.modelState[key][i]);
                 }
             }
             $scope.message = $scope.translation.REG_ERR + errors.join(' ');
        });
   	}

    $scope.search = function(){    
    	$scope.loading = true;
	    initSearchModel();      
	    searchService.search($scope.searchModel).then(function(results){	    
	      $scope.results = results.data;
            $scope.loading = false;
            $scope.savedSuccessfully = true;          
            $scope.message = $scope.translation.CREATIVES_FOUND + results.data.length;
    	}, function(response){
    		 console.log(response);
            $scope.savedSuccessfully = false;  
            $scope.loading = false;        
             var errors = [];
             for (var key in response.data.modelState) {
                 for (var i = 0; i < response.data.modelState[key].length; i++) {
                     errors.push(response.data.modelState[key][i]);
                 }
             }
             $scope.message = $scope.translation.REG_ERR + errors.join(' ');
    	}); 
    };

    $scope.searchFromTag = function(tag){
        $scope.loading = true;
        $scope.pattern = tag;
        initSearchModel();      
        searchService.search($scope.searchModel).then(function(results){        
          $scope.results = results.data;
            $scope.loading = false;
            $scope.savedSuccessfully = true;          
            $scope.message = $scope.translation.CREATIVES_FOUND + results.data.length;
        }, function(response){
             console.log(response);
            $scope.savedSuccessfully = false;  
            $scope.loading = false;        
             var errors = [];
             for (var key in response.data.modelState) {
                 for (var i = 0; i < response.data.modelState[key].length; i++) {
                     errors.push(response.data.modelState[key][i]);
                 }
             }
             $scope.message = $scope.translation.REG_ERR + errors.join(' ');
        }); 
    };

   

    $scope.$watch('conditionList|filter:{selected:true}', function (nv) {
        console.log($scope.conditionList);       
      }, true);

    var initSearchModel = function(){
        $scope.searchModel.pattern = $scope.pattern;
        $scope.searchModel.chapterName = $scope.conditionList[0].selected;
        $scope.searchModel.chapterText = $scope.conditionList[1].selected;
        $scope.searchModel.creativeName = $scope.conditionList[2].selected;
        $scope.searchModel.creativeDescription = $scope.conditionList[3].selected;
        $scope.searchModel.tagName = $scope.conditionList[4].selected;
        $scope.searchModel.commentText = $scope.conditionList[5].selected;
        $scope.searchModel.commentAuthor = $scope.conditionList[6].selected;
        $scope.searchModel.creativeAuthor = $scope.conditionList[7].selected;
    };
}]);