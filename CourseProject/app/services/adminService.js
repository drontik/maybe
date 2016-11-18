'use strict';
app.factory('adminService', ['$http',
 function ($http) {

  var adminServiceFactory = {};  

    var _getAllUsers = function () {    
        return $http.get(serviceBase + 'api/admin/users')
            .then(function (results) { return results; });
    };

     var _getAllMedals = function () {    
        return $http.get(serviceBase + 'api/admin/medals')
            .then(function (results) { return results; });
    };

    var _getAllComments = function () {    
        return $http.get(serviceBase + 'api/admin/comments')
            .then(function (results) { return results; });
    };

    var _getAllTags = function () {    
        return $http.get(serviceBase + 'api/admin/tags')
            .then(function (results) { return results; });
    };

     var _getAllChapters = function () {    
        return $http.get(serviceBase + 'api/admin/chapters')
            .then(function (results) { return results; });
    };
    

    var _saveUserData = function(data){
        return $http.post(serviceBase + 'api/admin/save', JSON.stringify(data), {
            headers:{contentType:'application/json; charset=utf-8', dataType:"json"}
        }).then(function(results){
            return results;
        });
    };   

    var _deleteUser = function(data){
        return $http.post(serviceBase + 'api/admin/delete', JSON.stringify(data), {
            headers:{contentType:'application/json; charset=utf-8', dataType:"json"}
        }).then(function(results){
            return results;
        });
    };

    var _resetPasword = function(data){
        return $http.post(serviceBase + 'api/admin/reset', JSON.stringify(data), {
            headers:{contentType:'application/json; charset=utf-8', dataType:"json"}
        }).then(function(results){
            return results;
        });
    };

    var _registerNewUser = function (registration) {     
        return $http.post(serviceBase + 'api/admin/register', registration).then(function (response) {
            return response;
        });
    };

    var _getAllRatings = function () {    
        return $http.get(serviceBase + 'api/admin/ratings')
            .then(function (results) { return results; });
    };

      var _deleteRating = function (id) {   
        return $http.post(serviceBase + 'api/admin/deleteRating/' + id)
            .then(function (results) { return results; });
    };

     var _deleteComment = function (id) {   
        return $http.post(serviceBase + 'api/admin/deleteComment/' + id)
            .then(function (results) { return results; });
    };

     var _deleteChapter = function (id) {   
        return $http.post(serviceBase + 'api/admin/chapters/delete/' + id)
            .then(function (results) { return results; });
    };

    var _saveCategory = function(data){
        return $http.post(serviceBase + 'api/admin/category', JSON.stringify(data), {
            headers:{contentType:'application/json; charset=utf-8', dataType:"json"}
        }).then(function(results){
            return results;
        });
    };  

    var _deleteCategory = function(id){
        return $http.post(serviceBase + 'api/admin/category/'+ id)
            .then(function(results){ return results;});
    };  

    adminServiceFactory.getAllUsers = _getAllUsers;
    adminServiceFactory.getAllMedals = _getAllMedals;
    adminServiceFactory.getAllRatings = _getAllRatings
    adminServiceFactory.getAllComments = _getAllComments
    adminServiceFactory.getAllTags = _getAllTags;
    adminServiceFactory.getAllChapters = _getAllChapters   

    adminServiceFactory.deleteUser = _deleteUser;
    adminServiceFactory.deleteRating = _deleteRating;
    adminServiceFactory.deleteComment = _deleteComment;
    adminServiceFactory.deleteChapter = _deleteChapter;

    adminServiceFactory.saveUserData = _saveUserData;
    adminServiceFactory.resetPassword = _resetPasword;
    adminServiceFactory.registerNewUser  = _registerNewUser

    adminServiceFactory.saveCategory = _saveCategory;
    adminServiceFactory.deleteCategory = _deleteCategory

    return adminServiceFactory;    
    
}]);