using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ExamSelise.Controllers
{
    public class ScoreCountController : ApiController
    {

        [HttpGet]
        public ResponseModel Get(int? GID)
        {
            //DomainModel db = new DomainModel();
            List<MatchDetail> matchdeatils;
            using (var db = new CricketMatchModel())
            {
                var count=db.MatchDetails.Where(x=>x.GID==GID).Sum(x=>x.Score);
                ResponseModel response = new ResponseModel()
                {
                    Issucces = true,
                    Message = "Data",
                    Data = count
                };


                return response;
            }


        }
    }
}
