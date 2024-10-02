using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LolTurnBase.Migrations
{
    /// <inheritdoc />
    public partial class CreatedFKRelationItemToChampion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ChampionID",
                table: "Items",
                type: "integer",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 1,
                column: "ChampionID",
                value: 1);

            migrationBuilder.CreateIndex(
                name: "IX_Items_ChampionID",
                table: "Items",
                column: "ChampionID");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Champion_ChampionID",
                table: "Items",
                column: "ChampionID",
                principalTable: "Champion",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_Champion_ChampionID",
                table: "Items");

            migrationBuilder.DropIndex(
                name: "IX_Items_ChampionID",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "ChampionID",
                table: "Items");
        }
    }
}
