app.controller('MatchParticipantController', ['$scope', 'MatchParticipantService', '$routeParams', '$location', function ($scope, MatchParticipantService, $routeParams, $location) {
    $scope.profileid = $routeParams.Id;
    $scope.save = function() {
        var basicInformations = {
            GID: 1,
            FirstTeam: $scope.FirstTeam,
            SecondTeam: $scope.SecondTeam,
            Bowling: $scope.Bowling
        };
            var promisePost = MatchParticipantService.post(basicInformations);
            console.log(basicInformations);
            promisePost.then(function(response) {
                $scope.Id = response.data.GID;
                console.log(response.data);
                console.log($scope.Id);
                $scope.Message = "Save Succesfully";
                console.log($scope.Message);
                window.location = "#/start/" + $scope.Id;

            }, function(err) {

                $scope.ErrorMessage = err.data['odata.error'].innererror.message.replace('basic_Information.', '').replace('basic_Information.', '');
                console.log($scope.ErrorMessage);
                console.log(err);
                //console.log(err.data['odata.error'].innererror.message['basic_Information.Phone']);
            });
        }



    


}]);

