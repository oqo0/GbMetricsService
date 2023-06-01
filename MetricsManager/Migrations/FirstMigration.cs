using FluentMigrator;

namespace MetricsManager.Migrations
{
    [Migration(1)]
    public class FirstMigration : Migration
    {
        public override void Up()
        {
            Create.Table("agentInfo")
                .WithColumn("agentId").AsInt32().PrimaryKey().Identity()
                .WithColumn("agentAddress").AsString()
                .WithColumn("enable").AsBoolean();
        }

        public override void Down()
        {
            Delete.Table("agentInfo");
        }
    }
}
