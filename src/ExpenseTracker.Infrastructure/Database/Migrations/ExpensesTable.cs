using FluentMigrator;

namespace ExpenseTracker.Infrastructure.Database.Migrations;
[Migration(2024102907)]
public class ExpensesTable : Migration
{
    public override void Up()
    {
        Create.Table("Expenses")
        .WithColumn("Id").AsGuid().PrimaryKey().NotNullable()
        .WithColumn("Amount").AsDecimal().NotNullable()
        .WithColumn("CreatedAt").AsDateTime().NotNullable()
        .WithColumn("UpdatedAt").AsDateTime().NotNullable()
        .WithColumn("Description").AsString().NotNullable()
        .WithColumn("CategoryId").AsGuid().NotNullable()
        .WithColumn("UserId").AsGuid().NotNullable();

        Create.ForeignKey("FK_Expenses_Categories")
            .FromTable("Expenses").ForeignColumn("CategoryId")
            .ToTable("ExpenseCategories").PrimaryColumn("Id");

        Create.ForeignKey("FK_Expenses_Users")
            .FromTable("Expenses").ForeignColumn("UserId")
            .ToTable("Users").PrimaryColumn("Id");
    }

    public override void Down()
    {
        Delete.ForeignKey("FK_Expenses_Users").OnTable("Expenses");
        Delete.ForeignKey("FK_Expenses_Categories").OnTable("Expenses");
        Delete.Table("Expenses");
    }
}