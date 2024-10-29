using FluentMigrator;
namespace ExpenseTracker.Infrastructure.Database.Migrations;
[Migration(2024102902)]
public class SubscriptionsTable : Migration
{
    public override void Up()
    {
        Create.Table("Subscriptions")
            .WithColumn("Id").AsGuid().PrimaryKey().NotNullable()
            .WithColumn("Name").AsString(100).NotNullable()
            .WithColumn("Description").AsString().Nullable()
            .WithColumn("MonthlyCost").AsDecimal().NotNullable();
    }

    public override void Down()
    {
        Delete.Table("Subscriptions");
    }
}
