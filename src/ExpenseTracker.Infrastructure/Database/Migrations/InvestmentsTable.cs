using FluentMigrator;

namespace ExpenseTracker.Infrastructure.Database.Migrations;
[Migration(2024102906)]
public class InvestmentsTable : Migration
{
    public override void Up()
    {
        Create.Table("Investments")
            .WithColumn("Id").AsGuid().PrimaryKey().NotNullable()
            .WithColumn("Amount").AsDecimal().NotNullable()
            .WithColumn("CreatedAt").AsDateTime().NotNullable()
            .WithColumn("UpdatedAt").AsDateTime().NotNullable()
            .WithColumn("Description").AsString(250).NotNullable()
            .WithColumn("UserId").AsGuid().NotNullable()
            .WithColumn("CategoryId").AsGuid().NotNullable();

        Create.ForeignKey("FK_Investments_Users")
            .FromTable("Investments").ForeignColumn("UserId")
            .ToTable("Users").PrimaryColumn("Id");

        Create.ForeignKey("FK_Investments_Categories")
            .FromTable("Investments").ForeignColumn("CategoryId")
            .ToTable("Categories").PrimaryColumn("Id");
    }

    public override void Down()
    {
        Delete.ForeignKey("FK_Investments_Users").OnTable("Investments");
        Delete.ForeignKey("FK_Investments_Categories").OnTable("Investments");

        Delete.Table("Investments");
    }
}