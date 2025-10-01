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
                name: "Groups",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    name = table.Column<string>(type: "nvarchar(64)", nullable: false),
                    owner_user_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    сreated_at = table.Column<DateTime>(type: "datetime", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Groups", x => x.id);
                    table.ForeignKey(
                        name: "FK_Groups_Users_owner_user_id",
                        column: x => x.owner_user_id,
                        principalTable: "Users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
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

            migrationBuilder.CreateTable(
                name: "Relations",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    relation_user_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    relation_group_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    сreated_at = table.Column<DateTime>(type: "datetime", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Relations", x => x.id);
                    table.ForeignKey(
                        name: "FK_Relations_Groups_relation_group_id",
                        column: x => x.relation_group_id,
                        principalTable: "Groups",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Relations_Users_relation_user_id",
                        column: x => x.relation_user_id,
                        principalTable: "Users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Groups_owner_user_id",
                table: "Groups",
                column: "owner_user_id");

            migrationBuilder.CreateIndex(
                name: "IX_Pins_owner_user_id",
                table: "Pins",
                column: "owner_user_id");

            migrationBuilder.CreateIndex(
                name: "IX_Relations_relation_group_id",
                table: "Relations",
                column: "relation_group_id");

            migrationBuilder.CreateIndex(
                name: "IX_Relations_relation_user_id",
                table: "Relations",
                column: "relation_user_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Pins");

            migrationBuilder.DropTable(
                name: "Relations");

            migrationBuilder.DropTable(
                name: "Groups");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
