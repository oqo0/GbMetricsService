using FluentMigrator;

namespace MetricsAgent.Migrations
{
    [Migration(1)]
    public class FirstMigration : Migration
    {
        public override void Up()
        {
            CreateTable("cpumetrics");
            CreateTable("dotnetmetrics");
            CreateTable("hddmetrics");
            CreateTable("networkmetrics");
            CreateTable("rammetrics");
        }

        public override void Down()
        {
            Delete.Table("cpumetrics");
            Delete.Table("dotnetmetrics");
            Delete.Table("hddmetrics");
            Delete.Table("networkmetrics");
            Delete.Table("rammetrics");
        }

        private void CreateTable(string name)
        {
            Create.Table(name)
                .WithColumn("Id").AsInt32().PrimaryKey().Identity()
                .WithColumn("Value").AsInt32()
                .WithColumn("Time").AsInt64();
        }
    }
}
