using FluentMigrator;

namespace ExpenseTracker.Infrastructure.Database.Migrations;
[Migration(2024102902)]
public class CountriesTable : Migration
{
    public override void Up()
    {
        Create.Table("Countries")
            .WithColumn("Id").AsGuid().PrimaryKey().NotNullable()
            .WithColumn("Name").AsString().NotNullable()
            .WithColumn("ThreeLetterUAVTCode").AsString().NotNullable();

        Insert.IntoTable("Countries").Row(new
        {
            Id = Guid.NewGuid(),
            Name = "Turkiye",
            ThreeLetterUAVTCode = "TRY",
        });
    }

    public override void Down()
    {
        Delete.Table("Countries");
    }
}