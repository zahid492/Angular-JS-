namespace ExamSelise
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class MatchDetail
    {
        public int Id { get; set; }

        public int GID { get; set; }

        [Required]
        
        public int Overs { get; set; }

        [Required]
        
        public int Ball { get; set; }

        public int? Score { get; set; }

        [StringLength(50)]
        public string Comments { get; set; }

        public virtual MatchPartcipant MatchPartcipant { get; set; }
    }
}
