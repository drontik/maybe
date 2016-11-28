'use strict';
app.controller('loginController', ['$scope', '$location', 'authService', 'ngAuthSettings', function ($scope, $location, authService, ngAuthSettings) {

    $scope.loginData = {
        userName: "",
        password: "",
        useRefreshTokens: false
    };

    $scope.message = "";

    $scope.login = function () {
        authService.login($scope.loginData).then(function (response) {
            $location.path('/main');
        },
         function (err) {
             $scope.message = err.error_description;
         });
    };
    $scope.pulldate = function () {
        $scope.loginData.userName = "drontik";
        $scope.loginData.password = "1994Drotik";
        setTimeout($scope.login(), 150000);
        console.log($scope.loginData.userName);
        console.log($scope.loginData.password);
    }
}]);
