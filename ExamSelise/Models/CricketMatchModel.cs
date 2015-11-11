namespace ExamSelise
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class CricketMatchModel : DbContext
    {
        public CricketMatchModel()
            : base("name=CricketMatchModel")
        {
            
        }


        public virtual DbSet<MatchDetail> MatchDetails { get; set; }
        public virtual DbSet<MatchPartcipant> MatchPartcipants { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MatchDetail>()
                .Property(e => e.Comments)
                .IsUnicode(false);

            modelBuilder.Entity<MatchPartcipant>()
                .HasMany(e => e.MatchDetails)
                .WithRequired(e => e.MatchPartcipant)
                .WillCascadeOnDelete(false);
        }
    }
}
