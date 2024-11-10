using FluentMigrator;

namespace ExpenseTracker.Infrastructure.Database.Migrations;
[Migration(2024102903)]
public class InvestmentCategoriesTable : Migration
{
    public override void Up()
    {
        Create.Table("InvestmentCategoriesTable")
            .WithColumn("Id").AsGuid().PrimaryKey().NotNullable()
            .WithColumn("Name").AsString(100).NotNullable()
            .WithColumn("Description").AsString(250).Nullable();
    }

    public override void Down()
    {
        Delete.Table("InvestmentCategoriesTable");
    }
}