using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RichsSnackRack.Migrations
{
    /// <inheritdoc />
    public partial class AlterExistingOrdersToBeCompleted : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("UPDATE SnackRack.Orders ord SET ord.OrderStatus = 1;");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
