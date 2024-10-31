using FluentMigrator;

namespace ExpenseTracker.Infrastructure.Logging.Migrations;

[Migration(2024102908)]
public class LogsTable : Migration
{
    public override void Up()
    {
        Create.Table("Logs")
            .WithColumn("Id").AsGuid().PrimaryKey().NotNullable()
            .WithColumn("Message").AsString().NotNullable()
            .WithColumn("Timestamp").AsDateTime().NotNullable()
            .WithColumn("LogType").AsString().NotNullable()
            .WithColumn("UserId").AsString().Nullable()
            .WithColumn("Exception").AsString().Nullable();
    }

    public override void Down()
    {
        Delete.Table("Logs");
    }
}