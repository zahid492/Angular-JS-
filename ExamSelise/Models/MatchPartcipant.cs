namespace ExamSelise
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("MatchPartcipant")]
    public partial class MatchPartcipant
    {
        public MatchPartcipant()
        {
            MatchDetails = new HashSet<MatchDetail>();
        }

        [Key]
        public int GID { get; set; }

        [Required]
        [StringLength(50)]
        public string FirstTeam { get; set; }

        [Required]
        [StringLength(50)]
        public string SecondTeam { get; set; }

        [Required]
        [StringLength(50)]
        public string Bowling { get; set; }

        public virtual ICollection<MatchDetail> MatchDetails { get; set; }
    }
}
