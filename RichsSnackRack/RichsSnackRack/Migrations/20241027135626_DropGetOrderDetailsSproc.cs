using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RichsSnackRack.Migrations
{
    /// <inheritdoc />
    public partial class DropGetOrderDetailsSproc : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP PROCEDURE if EXISTS GetOrderDetails;");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
