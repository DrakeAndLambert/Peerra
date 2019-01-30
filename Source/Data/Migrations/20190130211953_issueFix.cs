using Microsoft.EntityFrameworkCore.Migrations;

namespace Peerra.Data.Migrations
{
    public partial class issueFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Issues_Topics_TopicId",
                table: "Issues");

            migrationBuilder.AddForeignKey(
                name: "FK_Issues_Topics_TopicId",
                table: "Issues",
                column: "TopicId",
                principalTable: "Topics",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Issues_Topics_TopicId",
                table: "Issues");

            migrationBuilder.AddForeignKey(
                name: "FK_Issues_Topics_TopicId",
                table: "Issues",
                column: "TopicId",
                principalTable: "Topics",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
