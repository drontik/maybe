'use strict';
app.factory('creativeService', ['$http', 'ngAuthSettings','localStorageService', function ($http, ngAuthSettings,localStorageService) {

 	var serviceBase = ngAuthSettings.apiServiceBaseUri;
   
    var creativesServiceFactory = {};

    var _getCreatives = function () {
    	var currentUserName = localStorageService.get('authorizationData').userName;
        return $http.get(serviceBase + 'api/creatives/getall/' + currentUserName)
            .then(function (results) { return results; });
    };

    var _getCreativesByName = function (username) {     
        return $http.get(serviceBase + 'api/creatives/getall/' + username)
            .then(function (results) { return results; });
    };

    var _getAllCreatives = function () {      
        return $http.get(serviceBase + 'api/creatives/getall')
            .then(function (results) { return results; });
    };

     var _getPartialCreatives = function (id) {      
        return $http.get(serviceBase + 'api/creatives/getPartial/' + id)
            .then(function (results) { return results; });
    };

    var _getMostPopularCreatives = function () {       
        return $http.get(serviceBase + 'api/creatives/popular/')
            .then(function (results) { return results; });
    };

    var _getMostRatedCreatives = function () {       
        return $http.get(serviceBase + 'api/creatives/rated/')
            .then(function (results) { return results; });
    };

    var _getCreative = function (id) {        
        return $http.get(serviceBase + 'api/creatives/' +id)
            .then(function (results) { return results; });
    };

    var _getCategories = function () {
        return $http.get(serviceBase + 'api/categories')
            .then(function (results) { return results; });
    };

    var _getChapter = function (chapterId) {
        return $http.get(serviceBase + 'api/chapters/' + chapterId)
            .then(function (results) { return results; });
    };

    var _getAllTags = function () {
        return $http.get(serviceBase + 'api/tags')
            .then(function (results) { return results; });
    };

    var _getCreativeTags = function (creativeId) {
        return $http.get(serviceBase + 'api/tags/' + creativeId )
            .then(function (results) { return results; });
    };

    var _getComments = function(id){
        return $http.get(serviceBase + 'api/comments/' + id)
        .then(function(results){ return results;});
    };

    var _createCreative = function(data) {
        return $http.post(serviceBase + '/api/creatives', JSON.stringify(data), {
            headers: { contentType: 'application/json; charset=utf-8', dataType: "json" } })
            .then(function (results) { return results; });
    };

     var _createComment = function(data) {       
        return $http.post(serviceBase + '/api/comments', JSON.stringify(data), {
            headers: { contentType: 'application/json; charset=utf-8', dataType: "json" } })
            .then(function (results) { return results; });
    };

    var _createLike = function(data) {       
        return $http.post(serviceBase + '/api/likes', JSON.stringify(data), {
            headers: { contentType: 'application/json; charset=utf-8', dataType: "json" } })
            .then(function (results) { return results; });
    };

    var _createRating = function(data) {       
        return $http.post(serviceBase + '/api/rating', JSON.stringify(data), {
            headers: { contentType: 'application/json; charset=utf-8', dataType: "json" } })
            .then(function (results) { return results; });
    };

    var _setTags = function(id, data){
        return $http.post(serviceBase + '/api/tags/'+ id, JSON.stringify(data),{
        headers: { contentType: 'application/json; charset=utf-8', dataType: "json" }    
        }).then(function(results){
            return results;
        });
    };

    var _saveTag = function(data){
        return $http.post(serviceBase + '/api/admin/saveTag/', JSON.stringify(data),{
        headers: { contentType: 'application/json; charset=utf-8', dataType: "json" }    
        }).then(function(results){
            return results;
        });
    };

    var _saveRating = function(data){
        return $http.post(serviceBase + '/api/admin/saveRating/', JSON.stringify(data),{
        headers: { contentType: 'application/json; charset=utf-8', dataType: "json" }    
        }).then(function(results){
            return results;
        });
    };

    var _saveComment = function(data){
        return $http.post(serviceBase + '/api/admin/saveComment/', JSON.stringify(data),{
        headers: { contentType: 'application/json; charset=utf-8', dataType: "json" }    
        }).then(function(results){
            return results;
        });
    };

    var _saveChapter = function(data){
        return $http.post(serviceBase + '/api/chapters/', JSON.stringify(data),{
        headers: { contentType: 'application/json; charset=utf-8', dataType: "json" }    
        }).then(function(results){
            return results;
        });
    };
    
    var _deleteCreative = function (id) {   
        return $http.post(serviceBase + 'api/creatives/delete/' + id)
            .then(function (results) { return results; });
    };

    var _deleteCreativeByAdmin = function (id) {   
        return $http.post(serviceBase + 'api/admin/deleteCreative/' + id)
            .then(function (results) { return results; });
    };

    var _deleteComment = function (id) {   
        return $http.post(serviceBase + 'api/comments/delete/' + id)
            .then(function (results) { return results; });
    };

    var _deleteChapter = function (id) {   
        return $http.post(serviceBase + 'api/chapters/delete/' + id)
            .then(function (results) { return results; });
    };

     var _deleteTag = function (id) {   
        return $http.post(serviceBase + 'api/admin/deleteTag/' + id)
            .then(function (results) { return results; });
    };

    var _updateCreativeByAdmin = function (data) {   
         return $http.post(serviceBase + '/api/admin/updateCreative', JSON.stringify(data),{
        headers: { contentType: 'application/json; charset=utf-8', dataType: "json" }    
        }).then(function(results){
            return results;
        });
    };

    var _updateCreative = function (data) {   
         return $http.post(serviceBase + '/api/creatives/update', JSON.stringify(data),{
        headers: { contentType: 'application/json; charset=utf-8', dataType: "json" }    
        }).then(function(results){
            return results;
        });
    };

    var _rememberChapter = function (data) {   
         return $http.post(serviceBase + '/api/chapters/remember', JSON.stringify(data),{
        headers: { contentType: 'application/json; charset=utf-8', dataType: "json" }    
        }).then(function(results){
            return results;
        });
    };

    var _getRememberedChapter = function (data) {   
         return $http.post(serviceBase + '/api/chapters/getRememberedChapter', JSON.stringify(data),{
        headers: { contentType: 'application/json; charset=utf-8', dataType: "json" }    
        }).then(function(results){
            return results;
        });
    };

    var _sortChapters = function(creatives) {
            var sorted = {},
            a = [];     
            for (var chapter in creatives.chapters) {               
                if (creatives.chapters.hasOwnProperty(chapter)) {
                    a.push(creatives.chapters[chapter]);            
                }
            }          
            a.sort(function(x,y){               
                return x.number - y.number;
            });   
            return a;}

    creativesServiceFactory.getAllCreatives = _getAllCreatives;
    creativesServiceFactory.getCreativesByName = _getCreativesByName;
    creativesServiceFactory.getCreatives = _getCreatives;
    creativesServiceFactory.getCreative = _getCreative;
    creativesServiceFactory.getCategories = _getCategories;
    creativesServiceFactory.getComments = _getComments;
    creativesServiceFactory.getAllTags = _getAllTags;
    creativesServiceFactory.getCreativeTags = _getCreativeTags
    creativesServiceFactory.getChapter = _getChapter;
    creativesServiceFactory.setTags = _setTags;

    creativesServiceFactory.getPartialCreatives = _getPartialCreatives;

    creativesServiceFactory.saveTag = _saveTag;
    creativesServiceFactory.saveChapter = _saveChapter;
    creativesServiceFactory.saveRating = _saveRating;
    creativesServiceFactory.saveComment = _saveComment

    creativesServiceFactory.createCreative = _createCreative;
    creativesServiceFactory.createComment = _createComment;
    creativesServiceFactory.createLike = _createLike;
    creativesServiceFactory.createRating = _createRating;

    creativesServiceFactory.updateCreative = _updateCreative;
    creativesServiceFactory.updateCreativeByAdmin = _updateCreativeByAdmin;

    creativesServiceFactory.deleteCreative = _deleteCreative;
    creativesServiceFactory.deleteCreativeByAdmin = _deleteCreativeByAdmin;
    creativesServiceFactory.deleteComment = _deleteComment;
    creativesServiceFactory.deleteChapter = _deleteChapter;
    creativesServiceFactory.deleteTag = _deleteTag;

    creativesServiceFactory.sortChapters = _sortChapters;

    creativesServiceFactory.getMostPopularCreatives = _getMostPopularCreatives;
    creativesServiceFactory.getMostRatedCreatives = _getMostRatedCreatives;

    creativesServiceFactory.rememberChapter = _rememberChapter;
    creativesServiceFactory.getRememberedChapter = _getRememberedChapter

    return creativesServiceFactory; 

}]);