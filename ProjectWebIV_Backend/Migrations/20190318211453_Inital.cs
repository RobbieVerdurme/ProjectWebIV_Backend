using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectWebIV_Backend.Migrations
{
    public partial class Inital : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Posts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(maxLength: 50, nullable: false),
                    Created = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Comment",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Text = table.Column<string>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    Autheur = table.Column<string>(maxLength: 50, nullable: false),
                    PostId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comment_Posts_PostId",
                        column: x => x.PostId,
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "Id", "Created", "Title" },
                values: new object[] { 1, new DateTime(2019, 3, 18, 22, 14, 53, 187, DateTimeKind.Local).AddTicks(2965), "Post 1" });

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "Id", "Created", "Title" },
                values: new object[] { 2, new DateTime(2019, 3, 18, 22, 14, 53, 187, DateTimeKind.Local).AddTicks(2995), "Post 2" });

            migrationBuilder.InsertData(
                table: "Comment",
                columns: new[] { "Id", "Autheur", "Created", "PostId", "Text" },
                values: new object[] { 1, "Robbie Verdurme", new DateTime(2019, 3, 18, 22, 14, 53, 187, DateTimeKind.Local).AddTicks(4494), 1, "Comment 1" });

            migrationBuilder.InsertData(
                table: "Comment",
                columns: new[] { "Id", "Autheur", "Created", "PostId", "Text" },
                values: new object[] { 2, "Robbie Verdurme", new DateTime(2019, 3, 18, 22, 14, 53, 187, DateTimeKind.Local).AddTicks(5536), 1, "Comment 2" });

            migrationBuilder.InsertData(
                table: "Comment",
                columns: new[] { "Id", "Autheur", "Created", "PostId", "Text" },
                values: new object[] { 3, "Robbie Verdurme", new DateTime(2019, 3, 18, 22, 14, 53, 187, DateTimeKind.Local).AddTicks(5548), 1, "Comment 3" });

            migrationBuilder.CreateIndex(
                name: "IX_Comment_PostId",
                table: "Comment",
                column: "PostId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comment");

            migrationBuilder.DropTable(
                name: "Posts");
        }
    }
}
