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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
