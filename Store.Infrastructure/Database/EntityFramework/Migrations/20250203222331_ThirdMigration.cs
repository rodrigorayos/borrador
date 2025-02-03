using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Store.Infrastructure.Migrations
{
    public partial class ThirdMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Authorization",
                schema: "CTR",
                table: "Authorization");

            migrationBuilder.RenameTable(
                name: "Authorization",
                schema: "CTR",
                newName: "Authorizationssssss",
                newSchema: "CTR");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Authorizationssssss",
                schema: "CTR",
                table: "Authorizationssssss",
                column: "id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Authorizationssssss",
                schema: "CTR",
                table: "Authorizationssssss");

            migrationBuilder.RenameTable(
                name: "Authorizationssssss",
                schema: "CTR",
                newName: "Authorization",
                newSchema: "CTR");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Authorization",
                schema: "CTR",
                table: "Authorization",
                column: "id");
        }
    }
}
