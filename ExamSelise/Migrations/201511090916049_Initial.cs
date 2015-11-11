namespace ExamSelise.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MatchDetails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        GID = c.Int(nullable: false),
                        Overs = c.Int(nullable: false),
                        Ball = c.Int(nullable: false),
                        Score = c.Int(),
                        Comments = c.String(maxLength: 50, unicode: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.MatchPartcipant", t => t.GID)
                .Index(t => t.GID);
            
            CreateTable(
                "dbo.MatchPartcipant",
                c => new
                    {
                        GID = c.Int(nullable: false, identity: true),
                        FirstTeam = c.String(nullable: false, maxLength: 50),
                        SecondTeam = c.String(nullable: false, maxLength: 50),
                        Bowling = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.GID);
            
            CreateTable(
                "dbo.sysdiagrams",
                c => new
                    {
                        diagram_id = c.Int(nullable: false, identity: true),
                        name = c.String(nullable: false, maxLength: 128),
                        principal_id = c.Int(nullable: false),
                        version = c.Int(),
                        definition = c.Binary(),
                    })
                .PrimaryKey(t => t.diagram_id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MatchDetails", "GID", "dbo.MatchPartcipant");
            DropIndex("dbo.MatchDetails", new[] { "GID" });
            DropTable("dbo.sysdiagrams");
            DropTable("dbo.MatchPartcipant");
            DropTable("dbo.MatchDetails");
        }
    }
}
