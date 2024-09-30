using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LolTurnBase.Migrations
{
    /// <inheritdoc />
    public partial class AddingModelsToDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Champion",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Health = table.Column<int>(type: "integer", nullable: false),
                    Mana = table.Column<int>(type: "integer", nullable: false),
                    Armor = table.Column<int>(type: "integer", nullable: false),
                    MagicResist = table.Column<int>(type: "integer", nullable: false),
                    AttackDamage = table.Column<int>(type: "integer", nullable: false),
                    AbillityPower = table.Column<int>(type: "integer", nullable: false),
                    Level = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Champion", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    HealthGained = table.Column<int>(type: "integer", nullable: false),
                    ManaGained = table.Column<int>(type: "integer", nullable: false),
                    ArmorGained = table.Column<int>(type: "integer", nullable: false),
                    MagicResitGained = table.Column<int>(type: "integer", nullable: false),
                    AttackDamageGained = table.Column<int>(type: "integer", nullable: false),
                    AbillityPowerGained = table.Column<int>(type: "integer", nullable: false),
                    Cost = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Champion",
                columns: new[] { "Id", "AbillityPower", "Armor", "AttackDamage", "Health", "Level", "MagicResist", "Mana", "Name" },
                values: new object[,]
                {
                    { 1, 15, 10, 30, 300, 1, 11, 100, "Gragas" },
                    { 2, 20, 10, 30, 200, 1, 12, 200, "Ryze" }
                });

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "Id", "AbillityPowerGained", "ArmorGained", "AttackDamageGained", "Cost", "Description", "HealthGained", "MagicResitGained", "ManaGained", "Name" },
                values: new object[] { 1, 20, 15, 30, 999999, "Provides Random Stuff", 100, 100, 100, "Allmighty" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Champion");

            migrationBuilder.DropTable(
                name: "Items");
        }
    }
}
