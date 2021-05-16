namespace CaseStudy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Role_tbl",
                c => new
                    {
                        RoleID = c.Int(nullable: false, identity: true),
                        RoleName = c.String(),
                    })
                .PrimaryKey(t => t.RoleID);
            
            CreateTable(
                "dbo.tbl_user",
                c => new
                    {
                        u_id = c.Int(nullable: false, identity: true),
                        u_name = c.String(),
                        u_password = c.String(),
                        u_contact = c.String(),
                    })
                .PrimaryKey(t => t.u_id);
            
            CreateTable(
                "dbo.User_tbl",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserID = c.String(),
                        UserName = c.String(),
                        Password = c.String(),
                        ConfirmPassword = c.String(),
                        EmailAddress = c.String(),
                        PhoneNumber = c.String(),
                        Gender = c.String(),
                        Address = c.String(),
                        RoleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.OrderedProducts", "tbl_user_u_id", c => c.Int());
            CreateIndex("dbo.OrderedProducts", "tbl_user_u_id");
            AddForeignKey("dbo.OrderedProducts", "tbl_user_u_id", "dbo.tbl_user", "u_id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OrderedProducts", "tbl_user_u_id", "dbo.tbl_user");
            DropIndex("dbo.OrderedProducts", new[] { "tbl_user_u_id" });
            DropColumn("dbo.OrderedProducts", "tbl_user_u_id");
            DropTable("dbo.User_tbl");
            DropTable("dbo.tbl_user");
            DropTable("dbo.Role_tbl");
        }
    }
}
