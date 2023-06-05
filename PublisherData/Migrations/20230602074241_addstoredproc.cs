using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Publisher.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addstoredproc : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                @"CREATE PROCEDURE dbo.AuthorPublishedInYearRange
                    @yearstart int,
	                @Yearend int
                AS
                SELECT Distinct Authors.* FROM authors
                LEFT JOIN Books ON Authors.AuthorId=books.AuthorId
                WHERE YEAR(books.PublishDate) >=@yearstart AND Year(books.PublishDate)<=@Yearend
                ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                @"DROP PROCEDURE dbo.AuthorPublishedInYearRange");
        }
    }
}
