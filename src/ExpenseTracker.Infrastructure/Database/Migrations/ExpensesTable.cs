using FluentMigrator;

namespace ExpenseTracker.Infrastructure.Database.Migrations;
[Migration(2024102904)]
public class ExpensesTable : Migration
{
    public override void Up()
    {
        Create.Table("Expenses")
        .WithColumn("Id").AsGuid().PrimaryKey()
        .WithColumn("Amount").AsDecimal().NotNullable()
        .WithColumn("CreatedAt").AsDateTime().NotNullable()
        .WithColumn("UpdatedAt").AsDateTime().NotNullable()
        .WithColumn("Description").AsString().NotNullable()
        .WithColumn("Category").AsString().NotNullable()
        .WithColumn("UserId").AsGuid().NotNullable();

        Create.ForeignKey("FK_Expenses_Users")
            .FromTable("Expenses").ForeignColumn("UserId")
            .ToTable("Users").PrimaryColumn("Id");
    }

    public override void Down()
    {
        Delete.Table("Expenses");
    }
}