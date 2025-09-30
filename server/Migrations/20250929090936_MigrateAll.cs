using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Rodnie.API.Migrations
{
    /// <inheritdoc />
    public partial class MigrateAll : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    username = table.Column<string>(type: "nvarchar(16)", nullable: false),
                    password = table.Column<string>(type: "nvarchar(255)", nullable: false),
                    phone = table.Column<string>(type: "nvarchar(13)", nullable: false),
                    is_verified = table.Column<bool>(type: "bit", nullable: false),
                    is_restricted = table.Column<bool>(type: "bit", nullable: false),
                    role = table.Column<int>(type: "int", nullable: false),
                    сreated_at = table.Column<DateTime>(type: "datetime", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Pins",
                columns: table => new
                {
                    pin_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    owner_user_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pins", x => x.pin_id);
                    table.ForeignKey(
                        name: "FK_Pins_Users_owner_user_id",
                        column: x => x.owner_user_id,
                        principalTable: "Users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Pins_owner_user_id",
                table: "Pins",
                column: "owner_user_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Pins");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
