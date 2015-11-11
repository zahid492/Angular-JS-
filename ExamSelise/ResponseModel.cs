using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExamSelise
{
    public class ResponseModel
    {
        public bool Issucces { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
        public List<MatchDetail> DeptData { get; set; }
        public List<MatchPartcipant> ClassData { get; set; }
        public int Currpage { get; set; }
        public int TotalCount { get; set; }
        public int TotalPages { get; set; }
    }
}