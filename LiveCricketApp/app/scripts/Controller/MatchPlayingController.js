app.controller('MatchPlayingController', ['$scope', 'MatchParticipantService', 'MatchDetailsService', '$routeParams', '$location', function ($scope, MatchParticipantService, MatchDetailsService, $routeParams, $location) {
    $scope.GID = parseInt($routeParams.GID,10);
  //  console.log($scope.GID);
   // console.log("MatchPartcipantStart");

    $scope.cricket_update = function () {

      

   

        if ($scope.lastball >= 6) {
                $scope.ball = 1;
                $scope.over = $scope.lastover + 1;
        } else {

                if ($scope.lastover) {
                     $scope.over = $scope.lastover;
                 } else {
                $scope.over = 0;
            }
            
                if ($scope.lastball) {
                    $scope.ball = $scope.lastball + 1;
                } else {
                    $scope.ball = 1;

                }
            

            }


       
        if ($scope.over === 6) {
            alert("Game Over");
            $q.reject();

        }
       

        var myArray = ['0', '1', '2', '3', '4', '5', '6', 'WIDE'];
        var score = myArray[Math.floor(Math.random() * myArray.length)];


        if (score != 'WIDE') {
            $scope.run = parseInt(score);
            $scope.comment = $scope.run + ' run taken';

        } else {
            $scope.run = 1;
            $scope.comment = "WIDE";
            $scope.ball = $scope.lastball;
            $scope.over = $scope.lastover;


        }

        console.log("Last  update...");
        console.log("Last Ball"+$scope.ball);
        console.log("Last Over:"+$scope.over);
        console.log("Run:"+$scope.run);
        console.log("Comment"+$scope.comment);

        var basicInformations = {
            Id: 1,
            GID: $scope.GID,
            Overs: $scope.over,
            Ball: $scope.ball,
            Score: $scope.run,
            Comments: $scope.comment

        };

        console.log(basicInformations);


        var promisePost = MatchDetailsService.post(basicInformations);
        
        promisePost.then(function (response) {
            $scope.Id = response.data.GID;
           // console.log(response.data);
           // console.log($scope.Id);
            $scope.Message = "Save Succesfully";
            console.log($scope.Message);
            $scope.getallrun();
            $scope.getfullcommentatory();

            //  window.location = "#/start/" + $scope.Id;

        }, function (err) {

            $scope.ErrorMessage = err.data['odata.error'].innererror.message.replace('basic_Information.', '').replace('basic_Information.', '');
            //console.log($scope.ErrorMessage);
            //console.log(err);
            //console.log(err.data['odata.error'].innererror.message['basic_Information.Phone']);
        });

    }




    $scope.getallrun = function (GID) {
        var promiseGetSingle = MatchParticipantService.getallrun($scope.GID);
        promiseGetSingle.then(function (response) {
            var responseData = response.data;
            //  console.log(responseData);
            $scope.totalrun = responseData.Data;
        //    console.log($scope.run);



        },
            function (error) {
                console.log('Failure loading Information', error);
            });
    }

    $scope.getallrun();



    $scope.get = function (GID) {
        var promiseGetSingle = MatchParticipantService.get($scope.GID);
        promiseGetSingle.then(function (response) {
            var responseData = response.data;
            //  console.log(responseData);
           
            $scope.FirstTeam = responseData.FirstTeam;
            $scope.SecondTeam = responseData.SecondTeam;
            $scope.Bowling = responseData.Bowling;
//            console.log($scope.GId);
//            console.log($scope.FirstTeam);
//            console.log($scope.SecondTeam);
            if ($scope.Bowling === $scope.SecondTeam) {
                $scope.Batting = $scope.FirstTeam;
            } else {
                $scope.Batting = $scope.SecondTeam;
            }


        },
            function (error) {
                console.log('Failure loading Information', error);
            });
    }
    $scope.get();


    $scope.getfullcommentatory = function (GID) {
        console.log("Get Full Commentatory");
        var promiseGetSingle = MatchDetailsService.getfullcommentatory($scope.GID);
        promiseGetSingle.then(function (response) {
            var responseData = response.data.value;
            //console.log(responseData);
            $scope.listview = responseData;
            $scope.groups = _.groupBy($scope.listview, "Overs");
           // console.log("Scope Commentatory" + $scope.listview);



        },
            function (error) {
                console.log('Failure loading Information', error);
            });



        var promiseGetSingle = MatchDetailsService.getlastball($scope.GID);
        promiseGetSingle.then(function (response) {
            var responseData = response.data.value[0];
            console.log("Get Last Ball");
            console.log(responseData);
            $scope.lastover = responseData.Overs;
            $scope.lastball = responseData.Ball;



            //  $scope.last = $scope.lastover + 1;

            //$scope.listview = responseData;
            //  console.log($scope.lastball);
            // console.log($scope.lastover);
            //console.log($scope.last);
        },
            function (error) {
                console.log('Failure loading Information', error);
            });
    }

    $scope.getfullcommentatory();


}]);