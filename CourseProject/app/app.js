
var app = angular.module('AngularAuthApp',
    ['ui.bootstrap','ng-showdown','ngSanitize','ui.sortable','ngRoute','angular-markdown-editable',
     'ngTagsInput', 'LocalStorageModule', 'angular-loading-bar', 'dndLists', 'ngFileUpload', 'ngResource','infinite-scroll']);

var serviceBase = 'https://localhost:44351/';
//var serviceBase = 'http://sweexxik-001-site1.anytempurl.com/';

app.config(function ($routeProvider) {

    $routeProvider.when("/home", {
        controller: "homeController",
        templateUrl: "/app/views/home.html"
    });

    $routeProvider.when("/login", {
        controller: "loginController",
        templateUrl: "/app/views/login.html"
    });

    $routeProvider.when("/signup", {
        controller: "signupController",
        templateUrl: "/app/views/signup.html"
    });

    $routeProvider.when("/main", {
        controller: "mainController",
        templateUrl: "/app/views/main.html"
    });    

    $routeProvider.when("/edit/:Id", {
        controller: "editCreativeController",
        templateUrl: "/app/views/editCreative.html"
    });

    $routeProvider.when("/show/:Id", {
        controller: "showCreativeController",
        templateUrl: "/app/views/showCreative.html"
    });

    $routeProvider.when("/profile/", {
        controller: "profileController",
        templateUrl: "/app/views/profile.html"
    });

    $routeProvider.when("/admin", {
        controller: "adminController",
        templateUrl: "/app/views/admin/admin.html"
    });

    $routeProvider.when("/admin/creatives", {
        controller: "adminCreativesController",
        templateUrl: "/app/views/admin/creatives.html"
    });

    $routeProvider.when("/admin/chapters", {
        controller: "adminChaptersController",
        templateUrl: "/app/views/admin/chapters.html"
    });

    $routeProvider.when("/admin/tags", {
        controller: "adminTagsController",
        templateUrl: "/app/views/admin/tags.html"
    });

    $routeProvider.when("/admin/comments", {
        controller: "adminCommentsController",
        templateUrl: "/app/views/admin/comments.html"
    });

    $routeProvider.when("/admin/ratings", {
        controller: "adminRatingsController",
        templateUrl: "/app/views/admin/ratings.html"
    });

     $routeProvider.when("/admin/categories", {
        controller: "adminCategoriesController",
        templateUrl: "/app/views/admin/categories.html"
    });

    $routeProvider.when("/chapters/:creativeId/:chapterId", {
        controller: "chaptersController",
        templateUrl: "/app/views/chapters.html"
    });

    $routeProvider.when("/search/:categoryId", {
        controller: "searchController",
        templateUrl: "/app/views/search.html"
    });  

    $routeProvider.when("/user/:username", {
        controller: "userPageController",
        templateUrl: "/app/views/userPage.html"
    }); 

    $routeProvider.when("/NotFound", {
        controller: "indexController",
        templateUrl: "/app/views/NotFound.html"
    });   

    $routeProvider.otherwise({ redirectTo: "/home" });

});


app.constant('ngAuthSettings', {
    apiServiceBaseUri: serviceBase,
    clientId: 'ngAuthApp'
});

app.directive("colorInspiration", function() {
  return {
    restrict: 'EA',
    scope: {val: '=ngModel'},
    link: function(scope, element, attrs, ngModel) {      
      scope.$watch('val', function(newValue, oldValue) {
        console.log("watched it", scope.val);
        if (element.hasClass(oldValue)) {
          element.removeClass(oldValue);
        }
        element.addClass(newValue);
      });
    }
  }
  });  

app.directive('ckEditor', function () {
    return {
        require: '?ngModel',
        link: function (scope, elm, attr, ngModel) {
            var ck = CKEDITOR.replace(elm[0]);
            if (!ngModel) return;
            ck.on('instanceReady', function () {
                ck.setData(ngModel.$viewValue);
            });
            function updateModel() {
                scope.$apply(function () {
                ngModel.$setViewValue(ck.getData());
            });
        }
        ck.on('change', updateModel);
        ck.on('key', updateModel);
        ck.on('dataReady', updateModel);

        ngModel.$render = function (value) {
            ck.setData(ngModel.$viewValue);
        };
    }
};
});

app.directive(
            "mAppLoading",
            function( $animate ) {            
                return({
                    link: link,
                    restrict: "C"
                });        
                function link( scope, element, attributes ) {                   
                    $animate.leave( element.children().eq( 1 ) ).then(
                        function cleanupAfterAnimation() {                          
                            element.remove();                          
                            scope = element = attributes = null;
                        }
                    );
                    
                }
            }
        );  


app.config(function ($httpProvider,cfpLoadingBarProvider) {
    $httpProvider.interceptors.push('authInterceptorService');
    cfpLoadingBarProvider.includeSpinner = false;
});

app.run(['authService','$rootScope','$location','$anchorScroll','$routeParams', function (authService,$rootScope,$location,$anchorScroll,$routeParams) {
    authService.fillAuthData();
   $rootScope.$on('$routeChangeSuccess', function(newRoute, oldRoute) {
    $location.hash($routeParams.scrollTo);
    console.log($routeParams.scrollTo);
    $anchorScroll();  
  });

}]);


