'use strict';
app.controller('indexController', ['$http', '$scope', '$location', 'authService', 'searchService', 'translationService','localStorageService',
    function ($http, $scope, $location, authService, searchService, translationService, localStorageService) {

        var currentTheme = localStorageService.get('theme');//'lumen';
        var currentLanguade = localStorageService.get('lang');
       
        if(currentTheme != undefined){
            $scope.currentTheme = currentTheme;
        }
        else {
            $scope.currentTheme = 'lumen';
        }

        if(currentLanguade != undefined){
            $scope.selectedLanguage = currentLanguade;
        }
        else {
            $scope.selectedLanguage = 'en';
        }

        $scope.pattern = '';

        $scope.authentication = authService.authentication;

        $scope.translate = function () {
            translationService.getTranslation($scope, $scope.selectedLanguage);
        };
        
        $scope.translate();

        $scope.logOut = function() {
            authService.logOut();
            $location.path("/main");
        };

        $scope.changeTheme = function() {
            if ($scope.currentTheme == 'slate') {
                $scope.currentTheme = 'lumen';
                localStorageService.set('theme', 'lumen');
            } else {
                $scope.currentTheme = 'slate';
                localStorageService.set('theme', 'slate');
            }
        };

        $scope.search = function() {
            console.log($scope.pattern);
            searchService.setSearchPattern($scope.pattern);
            $location.path('/search/0');
        };  
        
        $scope.setEN = function() {
            $scope.selectedLanguage = 'en';
            localStorageService.set('lang', 'en');
            $scope.translate();
        };

        $scope.setRU = function () {
            localStorageService.set('lang','ru');
            $scope.selectedLanguage = 'ru';
            $scope.translate();
        };

    }]);