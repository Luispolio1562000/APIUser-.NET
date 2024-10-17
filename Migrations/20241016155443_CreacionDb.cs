using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace APIDEMO_.Migrations
{
    /// <inheritdoc />
    public partial class CreacionDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "UserRole",
                columns: table => new
                {
                    UserRoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRole", x => x.UserRoleId);
                    table.ForeignKey(
                        name: "FK_UserRole_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "UserId", "Active", "DateCreated", "LastName", "Name" },
                values: new object[,]
                {
                    { new Guid("23a04364-9f8b-4354-95fc-18169a4b3eda"), true, new DateTime(2024, 10, 16, 9, 54, 42, 721, DateTimeKind.Local).AddTicks(3436), "Fernandez", "Luis" },
                    { new Guid("56b61cc2-f6ae-41ce-b228-f497f4a406a4"), true, new DateTime(2024, 10, 16, 9, 54, 42, 721, DateTimeKind.Local).AddTicks(3455), "Polio", "Gustavo" }
                });

            migrationBuilder.InsertData(
                table: "UserRole",
                columns: new[] { "UserRoleId", "Active", "Role", "UserId" },
                values: new object[,]
                {
                    { new Guid("0d348d01-85da-404b-aaf1-219a892b4439"), true, "User", new Guid("23a04364-9f8b-4354-95fc-18169a4b3eda") },
                    { new Guid("56e1fcf6-a832-49b8-accb-c138b307533e"), true, "Support", new Guid("23a04364-9f8b-4354-95fc-18169a4b3eda") },
                    { new Guid("57b4a0c1-4937-42d3-80d5-93ca31df0725"), true, "Admin", new Guid("23a04364-9f8b-4354-95fc-18169a4b3eda") },
                    { new Guid("6868a947-cd59-4714-b382-d44bff29e62e"), true, "Admin", new Guid("56b61cc2-f6ae-41ce-b228-f497f4a406a4") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserRole_UserId",
                table: "UserRole",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserRole");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
