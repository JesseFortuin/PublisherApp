using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Publisher.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddDeleteCoverProc : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                @"CREATE PROCEDURE DeleteCover
                    @coverid int
                  AS
                  DELETE from covers where CoverId = @coverid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP PROCEDURE DeleteCover");
        }
    }
}
