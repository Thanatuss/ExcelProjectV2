using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Import_Excel.Infrastructore.Migrations
{
    /// <inheritdoc />
    public partial class changeidToId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Modifire",
                table: "AreaTypes",
                newName: "Modifier");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Areas",
                newName: "Id");

            migrationBuilder.AddColumn<Guid>(
                name: "Modifier",
                table: "Areas",
                type: "uniqueidentifier",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Modifier",
                table: "Areas");

            migrationBuilder.RenameColumn(
                name: "Modifier",
                table: "AreaTypes",
                newName: "Modifire");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Areas",
                newName: "id");
        }
    }
}
