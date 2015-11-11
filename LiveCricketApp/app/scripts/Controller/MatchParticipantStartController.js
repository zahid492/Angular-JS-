app.controller('MatchParticipantStartController', ['$scope', 'MatchParticipantService', '$routeParams', '$location', function ($scope, MatchParticipantService, $routeParams, $location) {
    $scope.GID = $routeParams.GID;
    console.log($scope.GID);
    console.log("MatchPartcipantStart");

    $scope.get = function (GID) {
        var promiseGetSingle = MatchParticipantService.get($scope.GID);
        promiseGetSingle.then(function (response) {
            var responseData = response.data;
            //  console.log(responseData);
            $scope.GId = responseData.GID;
            $scope.FirstTeam = responseData.FirstTeam;
            $scope.SecondTeam = responseData.SecondTeam;
            $scope.Bowling = responseData.Bowling;
            console.log($scope.GId);
            console.log($scope.FirstTeam);
            console.log($scope.SecondTeam);
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



    $scope.bowlingstart = function () {
        console.log($scope.GID);
        window.location = "#/cricupdate/" + $scope.GID;
    }

}]);



