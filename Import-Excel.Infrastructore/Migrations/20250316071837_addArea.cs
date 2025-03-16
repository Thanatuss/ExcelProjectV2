using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Import_Excel.Infrastructore.Migrations
{
    /// <inheritdoc />
    public partial class addArea : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Area_AreaTypes_AreaTypeId",
                table: "Area");

            migrationBuilder.DropForeignKey(
                name: "FK_Area_Area_ParentId",
                table: "Area");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Area",
                table: "Area");

            migrationBuilder.RenameTable(
                name: "Area",
                newName: "Areas");

            migrationBuilder.RenameIndex(
                name: "IX_Area_ParentId",
                table: "Areas",
                newName: "IX_Areas_ParentId");

            migrationBuilder.RenameIndex(
                name: "IX_Area_AreaTypeId",
                table: "Areas",
                newName: "IX_Areas_AreaTypeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Areas",
                table: "Areas",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_Areas_AreaTypes_AreaTypeId",
                table: "Areas",
                column: "AreaTypeId",
                principalTable: "AreaTypes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Areas_Areas_ParentId",
                table: "Areas",
                column: "ParentId",
                principalTable: "Areas",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Areas_AreaTypes_AreaTypeId",
                table: "Areas");

            migrationBuilder.DropForeignKey(
                name: "FK_Areas_Areas_ParentId",
                table: "Areas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Areas",
                table: "Areas");

            migrationBuilder.RenameTable(
                name: "Areas",
                newName: "Area");

            migrationBuilder.RenameIndex(
                name: "IX_Areas_ParentId",
                table: "Area",
                newName: "IX_Area_ParentId");

            migrationBuilder.RenameIndex(
                name: "IX_Areas_AreaTypeId",
                table: "Area",
                newName: "IX_Area_AreaTypeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Area",
                table: "Area",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_Area_AreaTypes_AreaTypeId",
                table: "Area",
                column: "AreaTypeId",
                principalTable: "AreaTypes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Area_Area_ParentId",
                table: "Area",
                column: "ParentId",
                principalTable: "Area",
                principalColumn: "id");
        }
    }
}
