﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Delfi.Glo.PostgreSql.Dal.Migrations
{
    /// <inheritdoc />
    public partial class CustomAlert : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "TimeandDate",
                table: "Alerts",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "TimeandDate",
                table: "Alerts",
                type: "text",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");
        }
    }
}
