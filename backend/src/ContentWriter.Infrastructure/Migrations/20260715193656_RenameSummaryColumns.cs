using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ContentWriter.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RenameSummaryColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("""
                DO $$
                BEGIN
                    IF EXISTS (
                        SELECT 1 FROM information_schema.columns
                        WHERE table_schema = 'content_writer' AND table_name = 'GeneratedContents' AND column_name = 'HeroExcerpt'
                    ) THEN
                        ALTER TABLE content_writer."GeneratedContents" RENAME COLUMN "HeroExcerpt" TO "HeroSummary";
                    END IF;

                    IF EXISTS (
                        SELECT 1 FROM information_schema.columns
                        WHERE table_schema = 'content_writer' AND table_name = 'GeneratedContents' AND column_name = 'NewspaperExcerpt'
                    ) THEN
                        ALTER TABLE content_writer."GeneratedContents" RENAME COLUMN "NewspaperExcerpt" TO "BlogSummary";
                    END IF;
                END $$;

                ALTER TABLE content_writer."GeneratedContents" ADD COLUMN IF NOT EXISTS "MainSummary" text NOT NULL DEFAULT '';
                ALTER TABLE content_writer."GeneratedContents" ADD COLUMN IF NOT EXISTS "AdvertisingSummary" text NOT NULL DEFAULT '';

                UPDATE content_writer."GeneratedContents" SET "AdvertisingSummary" = "Advertisement"
                WHERE "Advertisement" IS NOT NULL;

                ALTER TABLE content_writer."GeneratedContents" DROP COLUMN IF EXISTS "Advertisement";
                """);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("""
                ALTER TABLE content_writer."GeneratedContents" ADD COLUMN IF NOT EXISTS "Advertisement" text;
                UPDATE content_writer."GeneratedContents" SET "Advertisement" = "AdvertisingSummary";

                ALTER TABLE content_writer."GeneratedContents" DROP COLUMN IF EXISTS "MainSummary";
                ALTER TABLE content_writer."GeneratedContents" DROP COLUMN IF EXISTS "AdvertisingSummary";

                DO $$
                BEGIN
                    IF EXISTS (
                        SELECT 1 FROM information_schema.columns
                        WHERE table_schema = 'content_writer' AND table_name = 'GeneratedContents' AND column_name = 'HeroSummary'
                    ) THEN
                        ALTER TABLE content_writer."GeneratedContents" RENAME COLUMN "HeroSummary" TO "HeroExcerpt";
                    END IF;

                    IF EXISTS (
                        SELECT 1 FROM information_schema.columns
                        WHERE table_schema = 'content_writer' AND table_name = 'GeneratedContents' AND column_name = 'BlogSummary'
                    ) THEN
                        ALTER TABLE content_writer."GeneratedContents" RENAME COLUMN "BlogSummary" TO "NewspaperExcerpt";
                    END IF;
                END $$;
                """);
        }
    }
}
