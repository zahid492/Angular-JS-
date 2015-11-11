app.controller('MatchReviewController', ['$scope', 'MatchParticipantService', 'MatchDetailsService', '$routeParams', '$location', function ($scope, MatchParticipantService, MatchDetailsService, $routeParams, $location) {
    $scope.GID = parseInt($routeParams.GID);
    $scope.Overs = parseInt($routeParams.Overs);
    $scope.Ball = parseInt($routeParams.Ball);


    $scope.goto = function () {
        console.log($scope.GID);
        window.location = "#/cricupdate/" + $scope.GID;
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


    $scope.getreviewcommentatory = function () {
        console.log("Get Full Commentatory");
        var promiseGetSingle = MatchDetailsService.getreviewcommentatory($scope.GID, $scope.Overs, $scope.Ball);
        promiseGetSingle.then(function (response) {
            var unique_Id = response.data.value[0].Id;


            var promiseGetSingle = MatchDetailsService.getreviewdataindesc($scope.GID, unique_Id+1);
            promiseGetSingle.then(function (response) {
                var responseData = response.data.value;
                    console.log();
                console.log(responseData);
                $scope.listview = responseData;
                $scope.groups = _.groupBy($scope.listview, "Overs");


            },
                function (error) {
                    console.log('Failure loading Information', error);
                });

        },
            function (error) {
                console.log('Failure loading Information', error);
            });
    }

    $scope.getreviewcommentatory();


}]);