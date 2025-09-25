using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ResolveIQ.Web.Migrations
{
    /// <inheritdoc />
    public partial class CompletionDate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CompletionDate",
                table: "Tasks",
                type: "datetime(6)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CompletionDate",
                table: "Tasks");
        }
    }
}
