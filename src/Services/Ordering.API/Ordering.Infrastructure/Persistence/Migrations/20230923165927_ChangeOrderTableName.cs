using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ordering.Infrastructure.Persistence.Migrations
{
    public partial class ChangeOrderTableName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Ordrers",
                table: "Ordrers");

            migrationBuilder.RenameTable(
                name: "Ordrers",
                newName: "Orders");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Orders",
                table: "Orders",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Orders",
                table: "Orders");

            migrationBuilder.RenameTable(
                name: "Orders",
                newName: "Ordrers");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Ordrers",
                table: "Ordrers",
                column: "Id");
        }
    }
}
