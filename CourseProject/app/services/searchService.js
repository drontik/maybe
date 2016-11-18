'use strict';
app.factory('searchService', ['$http',
 function ($http) {

  var searchServiceFactory = {};

  var searchPattern = '';

  var _setSearchPattern = function(value){
  	searchPattern = value;
  };

  var _getSearchPattern = function(){
  	return searchPattern;
  };

  var _search = function(data) {
      return $http.post(serviceBase + 'api/creatives/search/', JSON.stringify(data), {
            headers: { contentType: 'application/json; charset=utf-8', dataType: "json" } })
            .then(function (results) { return results; });
  };

  var _searchByCategory = function (id) {      
      return $http.get(serviceBase + 'api/creatives/searchByCategory/' + id)
          .then(function (results) { return results; });
  };

    searchServiceFactory.search = _search;
    searchServiceFactory.setSearchPattern = _setSearchPattern;
    searchServiceFactory.getSearchPattern = _getSearchPattern;
    searchServiceFactory.searchByCategory = _searchByCategory;

    return searchServiceFactory;
}]);