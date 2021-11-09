using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Project.DAL.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase(
                collation: "Hebrew_CI_AS");

            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Subject",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    subName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    parent = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subject", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    userPassword = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    cityId = table.Column<int>(type: "int", nullable: false),
                    userType = table.Column<int>(type: "int", nullable: false),
                    imgUrl = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.id);
                    table.ForeignKey(
                        name: "FK__User__cityId__5DCAEF64",
                        column: x => x.cityId,
                        principalTable: "Cities",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Expert",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    userPassword = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    businessName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    cityId = table.Column<int>(type: "int", nullable: false),
                    imgUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    firstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    lastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    proSubject = table.Column<int>(type: "int", nullable: false),
                    description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    enable = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Expert", x => x.id);
                    table.ForeignKey(
                        name: "FK__Expert__cityId__619B8048",
                        column: x => x.cityId,
                        principalTable: "Cities",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__Expert__proSubject__619B8048",
                        column: x => x.proSubject,
                        principalTable: "Subject",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Meetings",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idProf = table.Column<int>(type: "int", nullable: false),
                    idUser = table.Column<int>(type: "int", nullable: false),
                    title = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    date = table.Column<DateTime>(type: "date", nullable: false),
                    time = table.Column<TimeSpan>(type: "time", nullable: false),
                    content = table.Column<string>(type: "nvarchar(1500)", maxLength: 1500, nullable: false),
                    isApproved = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Meetings", x => x.id);
                    table.ForeignKey(
                        name: "FK__Meetings__idProf__5812160E",
                        column: x => x.idProf,
                        principalTable: "Expert",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__Meetings__idUser__59063A47",
                        column: x => x.idUser,
                        principalTable: "User",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Recommend",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    profId = table.Column<int>(type: "int", nullable: false),
                    userId = table.Column<int>(type: "int", nullable: false),
                    title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    content = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    isApproved = table.Column<bool>(type: "bit", nullable: false),
                    date_posted = table.Column<DateTime>(type: "datetime2", nullable: false),
                    stars = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recommend", x => x.id);
                    table.ForeignKey(
                        name: "FK__Recommend__profId__5AEE82B9",
                        column: x => x.profId,
                        principalTable: "Expert",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__Recommend__userId__5BE2A6F2",
                        column: x => x.userId,
                        principalTable: "User",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Expert_cityId",
                table: "Expert",
                column: "cityId");

            migrationBuilder.CreateIndex(
                name: "IX_Expert_proSubject",
                table: "Expert",
                column: "proSubject");

            migrationBuilder.CreateIndex(
                name: "IX_Meetings_idProf",
                table: "Meetings",
                column: "idProf");

            migrationBuilder.CreateIndex(
                name: "IX_Meetings_idUser",
                table: "Meetings",
                column: "idUser");

            migrationBuilder.CreateIndex(
                name: "IX_Recommend_profId",
                table: "Recommend",
                column: "profId");

            migrationBuilder.CreateIndex(
                name: "IX_Recommend_userId",
                table: "Recommend",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_User_cityId",
                table: "User",
                column: "cityId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Meetings");

            migrationBuilder.DropTable(
                name: "Recommend");

            migrationBuilder.DropTable(
                name: "Expert");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Subject");

            migrationBuilder.DropTable(
                name: "Cities");

            migrationBuilder.AlterDatabase(
                oldCollation: "Hebrew_CI_AS");
        }
    }
}
