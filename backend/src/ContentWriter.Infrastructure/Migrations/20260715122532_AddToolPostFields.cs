using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ContentWriter.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddToolPostFields : Migration
    {
        /// <inheritdoc />
        /// <remarks>
        /// Uses raw ADD COLUMN IF NOT EXISTS because the deployed content_writer schema was
        /// originally created by the retired Geek-SEO content-writer, which already had these
        /// columns. Migrations only run on the PostgreSQL path (SQLite uses EnsureCreated).
        /// </remarks>
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("""
                ALTER TABLE content_writer."GeneratedContents" ADD COLUMN IF NOT EXISTS "Advertisement" text;
                ALTER TABLE content_writer."GeneratedContents" ADD COLUMN IF NOT EXISTS "DepartmentListExcerpt" text NOT NULL DEFAULT '';
                ALTER TABLE content_writer."GeneratedContents" ADD COLUMN IF NOT EXISTS "DisplayTitle" text;
                ALTER TABLE content_writer."GeneratedContents" ADD COLUMN IF NOT EXISTS "HeroExcerpt" text NOT NULL DEFAULT '';
                ALTER TABLE content_writer."GeneratedContents" ADD COLUMN IF NOT EXISTS "NewspaperExcerpt" text NOT NULL DEFAULT '';
                ALTER TABLE content_writer."GeneratedContents" ADD COLUMN IF NOT EXISTS "SourceAppName" text;
                ALTER TABLE content_writer."GeneratedContents" ADD COLUMN IF NOT EXISTS "SourceAppOrder" integer;
                ALTER TABLE content_writer."GeneratedContents" ADD COLUMN IF NOT EXISTS "ToolPageExcerpt" text NOT NULL DEFAULT '';
                """);
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
