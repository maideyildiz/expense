namespace ExpenseTracker.Infrastructure.Database.Migrations;

using FluentMigrator;

[Migration(2024102906)]
public class CitiesTable : Migration
{
    public override void Up()
    {
        Create.Table("Cities")
            .WithColumn("Id").AsGuid().PrimaryKey().NotNullable()
            .WithColumn("Name").AsString().NotNullable()
            .WithColumn("CountryId").AsGuid().NotNullable();

        Create.ForeignKey("FK_Cities_Countries")
            .FromTable("Cities").ForeignColumn("CountryId")
            .ToTable("Countries").PrimaryColumn("Id");
    }

    public override void Down()
    {
        Delete.Table("Cities");
    }
}