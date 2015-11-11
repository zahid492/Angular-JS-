var app = angular.module('app', ['ngResource', 'ngRoute', 'angular.filter']).constant('domain', 'http://localhost:1704/odata');

app.config([
    '$routeProvider', function ($routeProvider) {
        $routeProvider
         .when('/create',
            { templateUrl: 'app/views/create.html', controller: 'MatchParticipantController' })
         .when('/start/:GID',
            { templateUrl: 'app/views/start.html', controller: 'MatchParticipantStartController' })
         .when('/cricupdate/:GID',
            { templateUrl: 'app/views/playing.html', controller: 'MatchPlayingController' })
         .when('/review/:GID/:Overs/:Ball',
            { templateUrl: 'app/views/review.html', controller: 'MatchReviewController' })
         .otherwise({
             redirectTo: '/home',
             templateUrl: 'app/views/create.html', controller: 'MatchParticipantController'
         });


    }

]);