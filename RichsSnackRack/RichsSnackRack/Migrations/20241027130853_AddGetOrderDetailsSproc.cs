using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RichsSnackRack.Migrations
{
    /// <inheritdoc />
    public partial class AddGetOrderDetailsSproc : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql($@"
CREATE PROCEDURE GetOrderDetails()
	BEGIN
	        SELECT 
	        od.Id
	     , od.`Name`
	     , od.Price
	     , od.OrderTotal
	     , od.OrderStatus
	     , od.OrderDate 
	       FROM OrderDetails od ;
END;");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
