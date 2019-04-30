using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectWebIV_Backend.Migrations
{
    public partial class InitalMigration : Migration
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
                    Description = table.Column<string>(nullable: true),
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
                    Name = table.Column<string>(maxLength: 50, nullable: false),
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
                columns: new[] { "Id", "Created", "Description", "Title" },
                values: new object[] { 1, new DateTime(2019, 4, 30, 11, 56, 46, 132, DateTimeKind.Local).AddTicks(3298), null, "Post 1" });

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "Id", "Created", "Description", "Title" },
                values: new object[] { 2, new DateTime(2019, 4, 30, 11, 56, 46, 132, DateTimeKind.Local).AddTicks(3342), null, "Post 2" });

            migrationBuilder.InsertData(
                table: "Comment",
                columns: new[] { "Id", "Created", "Name", "PostId", "Text" },
                values: new object[] { 1, new DateTime(2019, 4, 30, 11, 56, 46, 132, DateTimeKind.Local).AddTicks(5157), "Robbie Verdurme", 1, "Comment 1" });

            migrationBuilder.InsertData(
                table: "Comment",
                columns: new[] { "Id", "Created", "Name", "PostId", "Text" },
                values: new object[] { 2, new DateTime(2019, 4, 30, 11, 56, 46, 132, DateTimeKind.Local).AddTicks(6289), "Robbie Verdurme", 1, "Comment 2" });

            migrationBuilder.InsertData(
                table: "Comment",
                columns: new[] { "Id", "Created", "Name", "PostId", "Text" },
                values: new object[] { 3, new DateTime(2019, 4, 30, 11, 56, 46, 132, DateTimeKind.Local).AddTicks(7157), "Robbie Verdurme", 1, "Comment 3" });

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
