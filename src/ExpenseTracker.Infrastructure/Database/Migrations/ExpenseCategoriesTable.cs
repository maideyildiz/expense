using FluentMigrator;

namespace ExpenseTracker.Infrastructure.Database.Migrations;
[Migration(2024102903)]
public class ExpenseCategoriesTable : Migration
{
    public override void Up()
    {
        Create.Table("ExpenseCategories")
            .WithColumn("Id").AsGuid().PrimaryKey().NotNullable()
            .WithColumn("Name").AsString(100).NotNullable()
            .WithColumn("Description").AsString(250).Nullable();
    }

    public override void Down()
    {
        Delete.Table("ExpenseCategories");
    }
}