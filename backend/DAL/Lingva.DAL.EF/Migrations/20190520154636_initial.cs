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
                name: "EmailSendingOption",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Host = table.Column<string>(nullable: true),
                    Port = table.Column<int>(nullable: false),
                    UseSsl = table.Column<bool>(nullable: false),
                    UserName = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailSendingOption", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EmailTemplate",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Text = table.Column<string>(nullable: true),
                    ParametersString = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailTemplate", x => x.Id);
                });

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
                    Picture = table.Column<string>(nullable: true),
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
                table: "EmailSendingOption",
                columns: new[] { "Id", "Host", "Password", "Port", "UseSsl", "UserName" },
                values: new object[] { 1, "smtp.gmail.com", "worksoftserve_90", 587, false, "worksoftserve@gmail.com" });

            migrationBuilder.InsertData(
                table: "EmailTemplate",
                columns: new[] { "Id", "ParametersString", "Text" },
                values: new object[] { 1, "GroupName; GroupDate", "You will have meeting {{GroupName}} at {{GroupDate}}" });

            migrationBuilder.InsertData(
                table: "Languages",
                columns: new[] { "Id", "CreateDate", "ModifyDate", "Name" },
                values: new object[,]
                {
                    { 1, new DateTime(2019, 5, 20, 18, 46, 36, 573, DateTimeKind.Local).AddTicks(3341), new DateTime(2019, 5, 20, 18, 46, 36, 577, DateTimeKind.Local).AddTicks(1930), "en" },
                    { 2, new DateTime(2019, 5, 20, 18, 46, 36, 577, DateTimeKind.Local).AddTicks(3265), new DateTime(2019, 5, 20, 18, 46, 36, 577, DateTimeKind.Local).AddTicks(3273), "ru" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreateDate", "Email", "ModifyDate", "Name" },
                values: new object[,]
                {
                    { 1, new DateTime(2019, 5, 20, 18, 46, 36, 577, DateTimeKind.Local).AddTicks(6456), "veloceraptor89@gmail.com", new DateTime(2019, 5, 20, 18, 46, 36, 577, DateTimeKind.Local).AddTicks(6460), "Serhii" },
                    { 2, new DateTime(2019, 5, 20, 18, 46, 36, 577, DateTimeKind.Local).AddTicks(7520), "tucker_serega@mail.ru", new DateTime(2019, 5, 20, 18, 46, 36, 577, DateTimeKind.Local).AddTicks(7527), "Old" }
                });

            migrationBuilder.InsertData(
                table: "Groups",
                columns: new[] { "Id", "CreateDate", "Date", "Description", "LanguageId", "ModifyDate", "Name", "Picture" },
                values: new object[] { 1, new DateTime(2019, 5, 20, 18, 46, 36, 577, DateTimeKind.Local).AddTicks(4849), new DateTime(2019, 5, 20, 18, 46, 36, 577, DateTimeKind.Local).AddTicks(4859), "Good movie", 1, new DateTime(2019, 5, 20, 18, 46, 36, 577, DateTimeKind.Local).AddTicks(4857), "Harry Potter", "1" });

            migrationBuilder.InsertData(
                table: "Groups",
                columns: new[] { "Id", "CreateDate", "Date", "Description", "LanguageId", "ModifyDate", "Name", "Picture" },
                values: new object[] { 2, new DateTime(2019, 5, 20, 18, 46, 36, 577, DateTimeKind.Local).AddTicks(6163), new DateTime(2019, 5, 20, 18, 46, 36, 577, DateTimeKind.Local).AddTicks(6174), "Eq", 1, new DateTime(2019, 5, 20, 18, 46, 36, 577, DateTimeKind.Local).AddTicks(6171), "Librium", "2" });

            migrationBuilder.InsertData(
                table: "Groups",
                columns: new[] { "Id", "CreateDate", "Date", "Description", "LanguageId", "ModifyDate", "Name", "Picture" },
                values: new object[] { 3, new DateTime(2019, 5, 20, 18, 46, 36, 577, DateTimeKind.Local).AddTicks(6188), new DateTime(2019, 5, 20, 18, 46, 36, 577, DateTimeKind.Local).AddTicks(6193), "stuff", 2, new DateTime(2019, 5, 20, 18, 46, 36, 577, DateTimeKind.Local).AddTicks(6190), "2Guns", "3" });

            migrationBuilder.InsertData(
                table: "GroupUser",
                columns: new[] { "GroupId", "UserId", "CreateDate", "Id", "ModifyDate" },
                values: new object[] { 1, 1, new DateTime(2019, 5, 20, 18, 46, 36, 577, DateTimeKind.Local).AddTicks(7740), 1, new DateTime(2019, 5, 20, 18, 46, 36, 577, DateTimeKind.Local).AddTicks(7744) });

            migrationBuilder.InsertData(
                table: "GroupUser",
                columns: new[] { "GroupId", "UserId", "CreateDate", "Id", "ModifyDate" },
                values: new object[] { 1, 2, new DateTime(2019, 5, 20, 18, 46, 36, 577, DateTimeKind.Local).AddTicks(8636), 2, new DateTime(2019, 5, 20, 18, 46, 36, 577, DateTimeKind.Local).AddTicks(8643) });

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
                name: "EmailSendingOption");

            migrationBuilder.DropTable(
                name: "EmailTemplate");

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
