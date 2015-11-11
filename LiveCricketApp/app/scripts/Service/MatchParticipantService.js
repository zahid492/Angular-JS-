app.service('MatchParticipantService', function ($http, domain) {
    //Create new record
    this.post = function (basicInformations) {
        var request = $http({
            method: "post",
            url: domain + "/MatchPartcipants/",
            data: basicInformations
        });
        return request;
    }



    //Get Single Records
    this.get = function (Id) {
        return $http.get(domain + "/MatchPartcipants("+Id +")");
    }

    //Get All Student
//    this.getallrun= function () {
//        return $http.get(domain + "/Basic_Information1/?$expand=Class,Department&$filter=Name eq '" + Name + "'");
//    }

    this.getallrun = function (Id) {
        return $http.get("http://localhost:1704/api/Scorecount?GID="+Id);
    }



});

app.service('MatchDetailsService', function ($http, domain) {
    //Create new record
    this.post = function (basicInformations) {
        var request = $http({
            method: "post",
            url: domain + "/MatchDetails/",
            data: basicInformations
        });
        return request;
    }

    this.getfullcommentatory = function (Id) {
        return $http.get(domain + "/MatchDetails?$filter=GID eq " + Id + "&$orderby=Id desc");
    }
    this.getreviewcommentatory = function (Id,Overs,Ball) {
        return $http.get(domain + "/MatchDetails?$filter=GID eq " + Id + " and Overs eq "+Overs+" and Ball eq "+Ball);
    }


    this.getreviewdataindesc = function (Id,uniqueId) {
        return $http.get(domain + "/MatchDetails?$filter=GID eq " + Id + " and Id lt "+uniqueId);
    }

    this.getlastball = function (Id) {
        return $http.get(domain + "/MatchDetails?$filter=GID eq "+Id+"&$orderby=Id desc&$top=1");
    }

});
