using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Lingva.DAL.EF.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Languages",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreateDate = table.Column<DateTime>(nullable: true),
                    ModifyDate = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Languages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreateDate = table.Column<DateTime>(nullable: true),
                    ModifyDate = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Groups",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreateDate = table.Column<DateTime>(nullable: true),
                    ModifyDate = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    LanguageId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Groups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Groups_Languages_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Languages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GroupUser",
                columns: table => new
                {
                    GroupId = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false),
                    Id = table.Column<int>(nullable: false),
                    CreateDate = table.Column<DateTime>(nullable: true),
                    ModifyDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupUser", x => new { x.GroupId, x.UserId });
                    table.ForeignKey(
                        name: "FK_GroupUser_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GroupUser_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Languages",
                columns: new[] { "Id", "CreateDate", "ModifyDate", "Name" },
                values: new object[,]
                {
                    { 1, new DateTime(2019, 6, 12, 13, 35, 20, 972, DateTimeKind.Local).AddTicks(8661), new DateTime(2019, 6, 12, 13, 35, 20, 977, DateTimeKind.Local).AddTicks(3697), "en" },
                    { 2, new DateTime(2019, 6, 12, 13, 35, 20, 977, DateTimeKind.Local).AddTicks(5101), new DateTime(2019, 6, 12, 13, 35, 20, 977, DateTimeKind.Local).AddTicks(5110), "ru" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreateDate", "Email", "ModifyDate", "Name" },
                values: new object[,]
                {
                    { 1, new DateTime(2019, 6, 12, 13, 35, 20, 977, DateTimeKind.Local).AddTicks(8880), "veloceraptor89@gmail.com", new DateTime(2019, 6, 12, 13, 35, 20, 977, DateTimeKind.Local).AddTicks(8885), "Serhii" },
                    { 2, new DateTime(2019, 6, 12, 13, 35, 20, 978, DateTimeKind.Local).AddTicks(324), "tucker_serega@mail.ru", new DateTime(2019, 6, 12, 13, 35, 20, 978, DateTimeKind.Local).AddTicks(333), "Old" }
                });

            migrationBuilder.InsertData(
                table: "Groups",
                columns: new[] { "Id", "CreateDate", "Date", "Description", "LanguageId", "ModifyDate", "Name" },
                values: new object[] { 1, new DateTime(2019, 6, 12, 13, 35, 20, 977, DateTimeKind.Local).AddTicks(6867), new DateTime(2019, 6, 12, 13, 35, 20, 977, DateTimeKind.Local).AddTicks(6878), "Good movie", 1, new DateTime(2019, 6, 12, 13, 35, 20, 977, DateTimeKind.Local).AddTicks(6874), "Harry Potter" });

            migrationBuilder.InsertData(
                table: "Groups",
                columns: new[] { "Id", "CreateDate", "Date", "Description", "LanguageId", "ModifyDate", "Name" },
                values: new object[] { 2, new DateTime(2019, 6, 12, 13, 35, 20, 977, DateTimeKind.Local).AddTicks(8180), new DateTime(2019, 6, 12, 13, 35, 20, 977, DateTimeKind.Local).AddTicks(8192), "Eq", 1, new DateTime(2019, 6, 12, 13, 35, 20, 977, DateTimeKind.Local).AddTicks(8189), "Librium" });

            migrationBuilder.InsertData(
                table: "Groups",
                columns: new[] { "Id", "CreateDate", "Date", "Description", "LanguageId", "ModifyDate", "Name" },
                values: new object[] { 3, new DateTime(2019, 6, 12, 13, 35, 20, 977, DateTimeKind.Local).AddTicks(8206), new DateTime(2019, 6, 12, 13, 35, 20, 977, DateTimeKind.Local).AddTicks(8210), "stuff", 2, new DateTime(2019, 6, 12, 13, 35, 20, 977, DateTimeKind.Local).AddTicks(8208), "2Guns" });

            migrationBuilder.InsertData(
                table: "GroupUser",
                columns: new[] { "GroupId", "UserId", "CreateDate", "Id", "ModifyDate" },
                values: new object[] { 1, 1, new DateTime(2019, 6, 12, 13, 35, 20, 978, DateTimeKind.Local).AddTicks(645), 1, new DateTime(2019, 6, 12, 13, 35, 20, 978, DateTimeKind.Local).AddTicks(649) });

            migrationBuilder.InsertData(
                table: "GroupUser",
                columns: new[] { "GroupId", "UserId", "CreateDate", "Id", "ModifyDate" },
                values: new object[] { 1, 2, new DateTime(2019, 6, 12, 13, 35, 20, 978, DateTimeKind.Local).AddTicks(1564), 2, new DateTime(2019, 6, 12, 13, 35, 20, 978, DateTimeKind.Local).AddTicks(1572) });

            migrationBuilder.CreateIndex(
                name: "IX_Groups_LanguageId",
                table: "Groups",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupUser_UserId",
                table: "GroupUser",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GroupUser");

            migrationBuilder.DropTable(
                name: "Groups");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Languages");
        }
    }
}
