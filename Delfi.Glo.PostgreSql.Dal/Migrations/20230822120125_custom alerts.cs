using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Delfi.Glo.PostgreSql.Dal.Migrations
{
    /// <inheritdoc />
    public partial class customalerts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Alerts");

            migrationBuilder.CreateTable(
                name: "CustomAlert",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    WellName = table.Column<string>(type: "text", nullable: false),
                    NotificationType = table.Column<string>(type: "text", nullable: false),
                    CustomAlertName = table.Column<string>(type: "text", nullable: false),
                    WellId = table.Column<string>(type: "text", nullable: false),
                    TimeandDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Priority = table.Column<string>(type: "text", nullable: false),
                    Category = table.Column<string>(type: "text", nullable: false),
                    Operator = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    UpdatedBy = table.Column<string>(type: "text", nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdatedDateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    entitytype = table.Column<string>(type: "text", nullable: false),
                    timestamp = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomAlert", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CustomAlert");

            migrationBuilder.CreateTable(
                name: "Alerts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AlertDescription = table.Column<string>(type: "text", nullable: false),
                    AlertLevel = table.Column<string>(type: "text", nullable: false),
                    AlertStatus = table.Column<string>(type: "text", nullable: false),
                    AlertType = table.Column<string>(type: "text", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    entitytype = table.Column<string>(type: "text", nullable: false),
                    TimeandDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    timestamp = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedBy = table.Column<string>(type: "text", nullable: false),
                    UpdatedDateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    WellName = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Alerts", x => x.Id);
                });
        }
    }
}
