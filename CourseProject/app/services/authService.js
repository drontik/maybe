'use strict';
app.factory('authService', ['$http', '$q', 'localStorageService', 'ngAuthSettings', function ($http, $q, localStorageService, ngAuthSettings) {

    var serviceBase = ngAuthSettings.apiServiceBaseUri;
    var authServiceFactory = {};

    var _authentication = {
        isAuth: false,
        userName: ""       
    };

    var _saveRegistration = function (registration) {
        _logOut();
        return $http.post(serviceBase + 'api/account/register', registration).then(function (response) {
            return response;
        });

    };

    var _login = function (loginData) {
        var data = "grant_type=password&username=" + loginData.userName + "&password=" + loginData.password;

        var deferred = $q.defer();

        $http.post(serviceBase + 'token', data, {
         headers: { 'Content-Type': 'application/x-www-form-urlencoded' } }).success(function (response) {
      
                localStorageService.set('authorizationData', {
                    token: response.access_token, 
                    userName: loginData.userName  });
            
            _authentication.isAuth = true;
            _authentication.userName = loginData.userName;     

            deferred.resolve(response);

        }).error(function (err, status) {
            _logOut();
            deferred.reject(err);
        });

        return deferred.promise;

    };

    var _logOut = function () {

        localStorageService.remove('authorizationData');
        localStorageService.remove('lang');
        localStorageService.remove('theme');
        _authentication.isAuth = false;
        _authentication.userName = "";   
    };

    var _fillAuthData = function () {

        var authData = localStorageService.get('authorizationData');
        if (authData) {
            _authentication.isAuth = true;
            _authentication.userName = authData.userName;      
        }

    };

    var _getProfileInfo = function () {
        var currentUserName = localStorageService.get('authorizationData').userName;
        return $http.get(serviceBase + 'api/Account/info/' + currentUserName)
            .then(function (results) { return results; });
    };

    var _getProfile = function (username) {      
        return $http.get(serviceBase + 'api/Account/info/' + username)
            .then(function (results) { return results; });
    };

    

    var _saveUserInfo = function(data) {
        return $http.post(serviceBase + '/api/account/saveInfo', JSON.stringify(data), {
            headers: { contentType: 'application/json; charset=utf-8', dataType: "json" } })
            .then(function (results) { return results; });
    };

    var _changePassword = function(data){
        return $http.post(serviceBase + '/api/account/changePassword', JSON.stringify(data),{
            headers: { contentType: 'application/json; charset=utf-8', dataType: "json" } 
        }).then(function(results) { return results});
    };

    authServiceFactory.saveRegistration = _saveRegistration;
    authServiceFactory.login = _login;
    authServiceFactory.logOut = _logOut;
    authServiceFactory.fillAuthData = _fillAuthData;
    authServiceFactory.authentication = _authentication;
    authServiceFactory.getProfileInfo = _getProfileInfo;
    authServiceFactory.getProfile = _getProfile;
    authServiceFactory.saveUserInfo = _saveUserInfo;
    authServiceFactory.changePassword = _changePassword;


    return authServiceFactory;
}]);