using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ContentWriter.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddToolPostFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Advertisement",
                schema: "content_writer",
                table: "GeneratedContents",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DepartmentListExcerpt",
                schema: "content_writer",
                table: "GeneratedContents",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DisplayTitle",
                schema: "content_writer",
                table: "GeneratedContents",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HeroExcerpt",
                schema: "content_writer",
                table: "GeneratedContents",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NewspaperExcerpt",
                schema: "content_writer",
                table: "GeneratedContents",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SourceAppName",
                schema: "content_writer",
                table: "GeneratedContents",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SourceAppOrder",
                schema: "content_writer",
                table: "GeneratedContents",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ToolPageExcerpt",
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
                name: "Advertisement",
                schema: "content_writer",
                table: "GeneratedContents");

            migrationBuilder.DropColumn(
                name: "DepartmentListExcerpt",
                schema: "content_writer",
                table: "GeneratedContents");

            migrationBuilder.DropColumn(
                name: "DisplayTitle",
                schema: "content_writer",
                table: "GeneratedContents");

            migrationBuilder.DropColumn(
                name: "HeroExcerpt",
                schema: "content_writer",
                table: "GeneratedContents");

            migrationBuilder.DropColumn(
                name: "NewspaperExcerpt",
                schema: "content_writer",
                table: "GeneratedContents");

            migrationBuilder.DropColumn(
                name: "SourceAppName",
                schema: "content_writer",
                table: "GeneratedContents");

            migrationBuilder.DropColumn(
                name: "SourceAppOrder",
                schema: "content_writer",
                table: "GeneratedContents");

            migrationBuilder.DropColumn(
                name: "ToolPageExcerpt",
                schema: "content_writer",
                table: "GeneratedContents");
        }
    }
}
