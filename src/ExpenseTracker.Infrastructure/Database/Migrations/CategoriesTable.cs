using FluentMigrator;

namespace ExpenseTracker.Infrastructure.Database.Migrations;
[Migration(2024102907)]
public class CategoriesTable : Migration
{
    public override void Up()
    {
        Create.Table("Categories")
            .WithColumn("Id").AsGuid().PrimaryKey().NotNullable()
            .WithColumn("Name").AsString(100).NotNullable()
            .WithColumn("Description").AsString(250).Nullable()
            .WithColumn("CategoryType").AsInt32().NotNullable();
    }

    public override void Down()
    {
        Delete.Table("Categories");
    }
}