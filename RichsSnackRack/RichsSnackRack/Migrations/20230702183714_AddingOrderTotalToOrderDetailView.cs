using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RichsSnackRack.Migrations
{
    /// <inheritdoc />
    public partial class AddingOrderTotalToOrderDetailView : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"CREATE OR REPLACE VIEW OrderDetails AS
                                    SELECT ord.Id, snk.`Name`, snk.Price, ord.OrderTotal, ord.OrderStatus, ord.OrderDate FROM Orders ord
                                    LEFT JOIN Snacks snk ON ord.SnackId = snk.Id;");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            
        }
    }
}
