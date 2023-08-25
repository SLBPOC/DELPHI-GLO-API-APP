using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Delfi.Glo.PostgreSql.Dal.Migrations
{
    /// <inheritdoc />
    public partial class dummy : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Alerts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    WellName = table.Column<string>(type: "text", nullable: false),
                    AlertLevel = table.Column<string>(type: "text", nullable: false),
                    TimeandDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    AlertDescription = table.Column<string>(type: "text", nullable: false),
                    AlertType = table.Column<string>(type: "text", nullable: false),
                    AlertStatus = table.Column<string>(type: "text", nullable: false),
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
                    table.PrimaryKey("PK_Alerts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Crew",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CrewName = table.Column<string>(type: "text", nullable: true),
                    entitytype = table.Column<string>(type: "text", nullable: false),
                    timestamp = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Crew", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Event",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    WellName = table.Column<string>(type: "text", nullable: false),
                    EventType = table.Column<string>(type: "text", nullable: false),
                    EventStatus = table.Column<string>(type: "text", nullable: false),
                    EventDescription = table.Column<string>(type: "text", nullable: false),
                    Priority = table.Column<string>(type: "text", nullable: false),
                    CreationDateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
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
                    table.PrimaryKey("PK_Event", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GeneralInfo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Qo = table.Column<decimal>(type: "numeric", nullable: false),
                    Ql = table.Column<decimal>(type: "numeric", nullable: false),
                    Qw = table.Column<decimal>(type: "numeric", nullable: false),
                    Qg = table.Column<decimal>(type: "numeric", nullable: false),
                    Wc = table.Column<decimal>(type: "numeric", nullable: false),
                    GlInjectionSetPoint = table.Column<decimal>(type: "numeric", nullable: false),
                    CompressorUpTime = table.Column<decimal>(type: "numeric", nullable: false),
                    DeviceUpTime = table.Column<decimal>(type: "numeric", nullable: false),
                    ProcessorState = table.Column<string>(type: "text", nullable: true),
                    ApprovalMode = table.Column<string>(type: "text", nullable: true),
                    WellViewComment1 = table.Column<string>(type: "text", nullable: true),
                    WellViewComment2 = table.Column<string>(type: "text", nullable: true),
                    WellViewComment3 = table.Column<string>(type: "text", nullable: true),
                    WellViewComment4 = table.Column<string>(type: "text", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    UpdatedBy = table.Column<string>(type: "text", nullable: true),
                    CreatedDateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedDateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GeneralInfo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Well",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    WellName = table.Column<string>(type: "text", nullable: false),
                    PumpStatus = table.Column<string>(type: "text", nullable: false),
                    Wc = table.Column<decimal>(type: "numeric", nullable: false),
                    GlInjectionSetPoint = table.Column<decimal>(type: "numeric", nullable: false),
                    CompressorUpTime = table.Column<decimal>(type: "numeric", nullable: false),
                    DeviceUpTime = table.Column<decimal>(type: "numeric", nullable: false),
                    TimeStamp = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    GLISetPoint = table.Column<int>(type: "integer", nullable: false),
                    OLiq = table.Column<int>(type: "integer", nullable: false),
                    QOil = table.Column<int>(type: "integer", nullable: false),
                    LastCycleStatus = table.Column<string>(type: "text", nullable: false),
                    CurrentGLISetpoint = table.Column<int>(type: "integer", nullable: false),
                    CycleStatus = table.Column<string>(type: "text", nullable: false),
                    ApprovalMode = table.Column<string>(type: "text", nullable: false),
                    ApprovalStatus = table.Column<string>(type: "text", nullable: false),
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
                    table.PrimaryKey("PK_Well", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Alerts");

            migrationBuilder.DropTable(
                name: "Crew");

            migrationBuilder.DropTable(
                name: "Event");

            migrationBuilder.DropTable(
                name: "GeneralInfo");

            migrationBuilder.DropTable(
                name: "Well");
        }
    }
}
