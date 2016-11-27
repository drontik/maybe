'use strict';
app.controller('showCreativeController', ['$showdown','$sce','$window','$route','$scope', '$location','$timeout','$routeParams',
    'authService','creativeService','localStorageService', 'searchService','$anchorScroll',
    function ($showdown,$sce, $window, $route, $scope, $location, $timeout, $routeParams,
     authService, creativeService, localStorageService,searchService,$anchorScroll) {
    
    $scope.authentication = authService.authentication;
    $scope.creative = [];   
    $scope.chapters = []; 
    $scope.comments = [];
    $scope.tags = [];
    $scope.ratings = [];
    $scope.storedChapterId = 0;
    
    $scope.percentage1 = 0;
    $scope.percentage2 = 0;
    $scope.percentage3 = 0;
    $scope.percentage4 = 0;
    $scope.percentage5 = 0;

    $scope.newComment = undefined;
    $scope.showLoading = false;
    $scope.message = '';

    $scope.ratingError = '';

    var creativeId = $routeParams.Id; 
    var currentUserName = ''

    var savedChapter = 0;

     var rememberChapterModel = {
        chapterId:0,
        creativeId: creativeId,
        userName: ''
    }

    if( $scope.authentication.isAuth ){
        currentUserName = localStorageService.get('authorizationData').userName;
        rememberChapterModel.userName = currentUserName;
        creativeService.getRememberedChapter(rememberChapterModel).then(function(results){
            $scope.storedChapterId = results.data.chapterId;         
        });
    }   

    var newCommentModel = {
        Id:0,
        text: "",      
        creativeId:0,
        userName:""
    };
    
    var newLikeModel = {
        commentId:0,
        userName:"",
        id:0
    };

    var newRatingModel = {
        value:0,
        creativeId:0,
        userName:""
    };

    $scope.htmldata = 'Enter text here'; 

    if (authService.authentication.isAuth){
        authService.getProfileInfo().then(function(results){
                $scope.currentUserInfo = results.data;              
        });
    };

    $scope.saveCommentToDelete = function(id){
        $scope.commentToDeleteId = id; 
    }

    $scope.storeChapterId = function(id){
        rememberChapterModel.chapterId = id;
        rememberChapterModel.creativeId = creativeId;
        rememberChapterModel.userName = currentUserName;
        if($scope.authentication.isAuth){
            creativeService.rememberChapter(rememberChapterModel).then(function(results){
                console.log(results);
            });    
        }
    };    
    
    creativeService.getCreative(creativeId).then(function (results) {
        initCreative(results.data);       
    }, function (error) {
        console.log(error);
        $location.path('/NotFound')
    });

    // creativeService.getComments(creativeId).then(function(result){
    //     $scope.comments = result.data;
    // }, function(error){
    //     console.log(error);
    // });

    creativeService.getCreativeTags(creativeId).then(function(result){
        $scope.tags = result.data;      
    }, function(error){
        console.log(error);
    });

    

     $scope.showNewComment = function(){
        if(authService.authentication.isAuth) {
            $scope.newComment = {};
        }
        else {            
            $location.path("/login");
        }
    };

    $scope.editComment = function(comment){
        $scope.newComment.text = comment.text;
        $scope.newComment.id = comment.id;
        window.scrollTo(0,document.body.scrollHeight);
    };

    $scope.createComment = function(formData){  
        $scope.message = '';
        $scope.showLoading = true;
        initComment();   
        newCommentModel.id = $scope.newComment.id;
        creativeService.createComment(newCommentModel).then(function(results){
            $scope.savedSuccessfully = true;
            $scope.showLoading = false;            
            $scope.creative.comments = results.data;       
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

     var initComment = function(){
        newCommentModel.userName = currentUserName;  
        newCommentModel.text = $scope.newComment.text;
        newCommentModel.creativeId = creativeId;

   };   

   $scope.deleteComment = function(id){  
     $('#myModal').modal('hide');    
            creativeService.deleteComment($scope.commentToDeleteId).then(function(results) {
                $scope.creative.comments = results.data;

            });  
               
    }

    $scope.setLike = function(id){
        var commentId = id;
        newLikeModel.userName = currentUserName; 
        newLikeModel.commentId = commentId;

        if($scope.authentication.isAuth){
            creativeService.createLike(newLikeModel).then(function(result){
                    for (var i = 0; i < $scope.creative.comments.length; i++) {
                        if($scope.creative.comments[i].id === commentId){
                            $scope.creative.comments[i] = result.data;  
                            console.log(result.data);               
                        }
                    }      
                });
        }
        else {
            console.log("Not authorized");
        }      
   };

   $scope.setRating = function(id) {
        newRatingModel.creativeId = creativeId;
        newRatingModel.value = id;
        newRatingModel.userName = currentUserName;//localStorageService.get('authorizationData').userName;

        creativeService.createRating(newRatingModel).then(function(results){          
            $scope.ratings = results.data;
            $scope.ratingsAvg = calcAvg();
            setPercentage();
        }, function(error){
            $scope.ratingError = "Error! You can't rate creative more than once!"
        });     
   };

   $scope.search = function(pattern){
         searchService.setSearchPattern(pattern);
        $location.path('/search/0');
    };

    $scope.setInitChapter = function(){
        var tempNumber = 0;
        var tempArray = [];
        console.log("setInitChapter");
        console.log($scope.storedChapterId);
        for (var i = 0; i < $scope.chapters.length; i++) {
            if ($scope.chapters[i].id == $scope.storedChapterId){
                tempNumber = $scope.chapters[i].number;
                tempArray.push($scope.chapters[i]);  
                delete $scope.chapters[i]; 
                
                for (var i = 0; i < $scope.chapters.length; i++) {
                    if ( $scope.chapters[i] != undefined && $scope.chapters[i].number > tempNumber ){
                        console.log($scope.chapters[i].number)
                        tempArray.push($scope.chapters[i]);
                        delete $scope.chapters[i];  
                        }
                    }              
                }             
        }   
        var res = tempArray.concat($scope.chapters);
        var temp = [];
        for (var i = 0; i < res.length; i++) {
               if ( res[i] !== undefined ) {
                    temp.push(res[i]);
            }
        }
        $scope.chapters = temp;      
    };

    var calcAvg = function (){        
        var sum = 0;
        var defaultResult = 0;
        for (var i = 0; i < $scope.ratings.length; i++) {            
            sum = sum + $scope.ratings[i].value;          
        }
        var value = (sum/$scope.ratings.length).toPrecision(3);
        if ($.isNumeric(value)) {
            return value;
        }
        return defaultResult;
   };

    var initCreative = function (data) {
        console.log(data);
        if(data != null){
            $scope.creative = data;
            $scope.chapters = creativeService.sortChapters(data); 
            $scope.ratings = data.rating;
            $scope.ratingsAvg = calcAvg();
            setPercentage();

            for (var i = 0; i < $scope.chapters.length; i++) {
                var md = $showdown.makeHtml($scope.chapters[i].body);
                $scope.chapters[i].body = $sce.trustAsHtml(md);
            }      
        }        
   }

  

    var setPercentage = function () {
        $scope.percentage1 = 0;    
        $scope.percentage2 = 0;
        $scope.percentage3 = 0;    
        $scope.percentage4 = 0;    
        $scope.percentage5 = 0;   

        for (var i = 0; i < $scope.ratings.length; i++) {
        switch($scope.ratings[i].value){
            case 1: $scope.percentage1++;
                break;
            case 2: $scope.percentage2++;
                break;
            case 3: $scope.percentage3++;
                break;
            case 4: $scope.percentage4++;
                break;
            case 5: $scope.percentage5++;
            default: 
                break;
            }
        }
        
        //magic value
        var len =  $scope.ratings.length /420;

        $scope.percentage1 =  ($scope.percentage1/len).toPrecision(3);
        $scope.percentage2 =  ($scope.percentage2/len).toPrecision(3);
        $scope.percentage3 =  ($scope.percentage3/len).toPrecision(3);
        $scope.percentage4 =  ($scope.percentage4/len).toPrecision(3);
        $scope.percentage5 =  ($scope.percentage5/len).toPrecision(3);
   };


    $scope.targetClass = 'readerStyle3';
    
    $scope.targetFontClass = 'readerFont1';
  
  $scope.colorInspirationClasses = [
    'readerStyle1', 'readerStyle2', 'readerStyle3', 'readerFont1', 'readerFont2','readerFont3'
  ];
  
  $scope.highlight = function(a) {
     console.log(a);
    $scope.targetClass = $scope.colorInspirationClasses[a];
  }; 

  $scope.changeFont = function (a) {
      console.log(a);
      $scope.targetFontClass = $scope.colorInspirationClasses[a];
  }

}]);