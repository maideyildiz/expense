using FluentMigrator;
using FluentMigrator.Builders.Create.Table;

using Microsoft.Extensions.Logging;
namespace ExpenseTracker.Infrastructure.Database.Migrations;
[Migration(2024102905)]
public class UsersTable : Migration
{
    public override void Up()
    {
        Create.Table("Users")
            .WithColumn("Id").AsGuid().PrimaryKey().NotNullable()
            .WithColumn("FirstName").AsString(50).NotNullable()
            .WithColumn("LastName").AsString(50).NotNullable()
            .WithColumn("Email").AsString(100).NotNullable().Unique()
            .WithColumn("PasswordHash").AsString().NotNullable()
            .WithColumn("MonthlySalary").AsDecimal().NotNullable()
            .WithColumn("YearlySalary").AsDecimal().NotNullable()
            .WithColumn("CreatedAt").AsDateTime().NotNullable()
            .WithColumn("UpdatedAt").AsDateTime().NotNullable()
            .WithColumn("LastLoginAt").AsDateTime().Nullable()
            .WithColumn("IsActive").AsBoolean().NotNullable()
            .WithColumn("CityId").AsGuid().NotNullable();

        Create.ForeignKey("FK_Users_Cities")
        .FromTable("Users").ForeignColumn("CityId")
        .ToTable("Cities").PrimaryColumn("Id");
    }

    public override void Down()
    {
        Delete.ForeignKey("FK_Users_Cities").OnTable("Users");
        Delete.Table("Users");
    }
}
