using FluentMigrator;
namespace ExpenseTracker.Infrastructure.Database.Migrations;
[Migration(2024102901)]
public class SubscriptionsTable : Migration
{
    public override void Up()
    {
        Create.Table("Subscriptions")
            .WithColumn("Id").AsGuid().PrimaryKey().NotNullable()
            .WithColumn("Name").AsString(100).NotNullable()
            .WithColumn("Description").AsString().Nullable()
            .WithColumn("MonthlyCost").AsDecimal().NotNullable();


        Insert.IntoTable("Subscriptions").Row(new
        {
            Id = Guid.NewGuid(),
            Name = "Basic Plan",
            Description = "Basic subscription plan",
            MonthlyCost = 9.99m,
        });

        Insert.IntoTable("Subscriptions").Row(new
        {
            Id = Guid.NewGuid(),
            Name = "Premium Plan",
            Description = "Premium subscription plan",
            MonthlyCost = 19.99m,
        });
    }

    public override void Down()
    {
        Delete.Table("Subscriptions");
    }
}
