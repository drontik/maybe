'use strict';
app.controller('profileController', ['ngAuthSettings','$anchorScroll','$http','$scope', '$location', 'authService','$timeout','Upload',
   function (ngAuthSettings,$anchorScroll,$http,$scope, $location, authService, $timeout,Upload) {
  
    var serviceBase = ngAuthSettings.apiServiceBaseUri;

    $scope.authentication = authService.authentication;
    $scope.userInfo = {};
    $scope.newUserInfo = {};
    $scope.flagHide = false;
    $scope.progressPercentage = 0;
    $scope.showUpload = "";
    $scope.isAdmin = false;
    $scope.showLoading = false;
    $scope.savedSuccessfully = false;
    $scope.message = '';

    $scope.email = {
        text: 'me@example.com'
      };

   var newData = {
      OldPassword:"",
      NewPassword:"",
      ConfirmPassword:"",
      userName:""
    };

    if($scope.authentication.isAuth) {
       authService.getProfileInfo().then(function(results){
          $scope.userInfo = results.data;       
            $scope.isAdmin = $scope.userInfo.isAdmin;
            console.log($scope.userInfo);        
        });
    }
    else {
      $location.path('/login')
    }
   
    $scope.saveUserInfo = function () {
        $scope.showLoading = true;  
        authService.saveUserInfo($scope.userInfo).then(function(results){  
            $scope.savedSuccessfully = true;
            $scope.showLoading = false; 
            $scope.message = $scope.translation.SAVE_SUCC;   
            $scope.userInfo = results.data;       
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

      $scope.changePassword = function(){
        $scope.showLoading = true;  
        newData.OldPassword = $scope.newUserInfo.oldPassword;
        newData.NewPassword = $scope.newUserInfo.newPassword;
        newData.ConfirmPassword = $scope.newUserInfo.passConfirm;
        newData.userName = $scope.userInfo.userName;

        authService.changePassword(newData).then(function(results){
            $scope.savedSuccessfully = true;
            $scope.showLoading = false; 
            $scope.message = $scope.translation.SAVE_SUCC; 
            $scope.userInfo = results.data;       
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

    $scope.updateAvatar = function(){
           $scope.upload($scope.file);
       
    };
    
    $scope.$watch('file', function () {      
       // console.log($scope.file);
    });
  

    $scope.log = '';

    $scope.upload = function (file) {               
              if (!file.$error) {
                $scope.showLoading = true;
                Upload.upload({
                    url: serviceBase + 'api/upload/',
                    data: {
                      username: $scope.authentication.userName,
                      file: file  
                    }
                }).then(function (resp) {                    
                    $timeout(function() {                      
                      $scope.userInfo = resp.data;
                      $scope.showLoading = false;
                      $('.global-modal').toggleClass('global-modal-show');                     
                    });
                }, null, function (evt) {
                    $scope.progressPercentage = parseInt(100.0 * evt.loaded / evt.total);    
                });
          }
    };


       var globalModal = $('.global-modal');
    $( ".trigger" ).on( "click", function(e) {
      e.preventDefault();
      $( globalModal ).toggleClass('global-modal-show');
    });
    $( ".overlay" ).on( "click", function() {
      $( globalModal ).toggleClass('global-modal-show');
    });



}]);