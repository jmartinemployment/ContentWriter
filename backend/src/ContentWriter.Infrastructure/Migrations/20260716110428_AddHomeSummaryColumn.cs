using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ContentWriter.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddHomeSummaryColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "HomeSummary",
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
                name: "HomeSummary",
                schema: "content_writer",
                table: "GeneratedContents");
        }
    }
}
