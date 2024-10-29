using FluentMigrator;

namespace ExpenseTracker.Infrastructure.Database.Migrations;
[Migration(2024102903)]
public class InvestmentsTable : Migration
{
    public override void Up()
    {
        Create.Table("Investments")
            .WithColumn("Id").AsGuid().PrimaryKey()
            .WithColumn("Amount").AsDecimal().NotNullable()
            .WithColumn("CreatedAt").AsDateTime().NotNullable()
            .WithColumn("UpdatedAt").AsDateTime().NotNullable()
            .WithColumn("Description").AsString().NotNullable()
            .WithColumn("Category").AsString().NotNullable() // ValueObject için uygun şekilde
            .WithColumn("UserId").AsGuid().NotNullable(); // UserId, User tablosuyla ilişkilendirilecek

        Create.ForeignKey("FK_Investments_Users")
        .FromTable("Investments").ForeignColumn("UserId")
        .ToTable("Users").PrimaryColumn("Id");
    }

    public override void Down()
    {
        Delete.Table("Investments");
    }
}