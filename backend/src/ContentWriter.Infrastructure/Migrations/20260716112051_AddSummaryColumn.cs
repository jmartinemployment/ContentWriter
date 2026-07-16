using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ContentWriter.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddSummaryColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Summary",
                schema: "content_writer",
                table: "GeneratedContents",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Summary",
                schema: "content_writer",
                table: "GeneratedContents");
        }
    }
}
