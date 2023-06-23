using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RichsSnackRack.Migrations
{
    /// <inheritdoc />
    public partial class InsertOrderStatusEnumData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql($@"DROP TABLE IF EXISTS SnackRack.OrderStatus;

                            CREATE TABLE IF NOT EXISTS SnackRack.OrderStatus
                            (
                            Id INT NOT NULL PRIMARY KEY,
                            Name VARCHAR(100) NOT NULL,
                            Description VARCHAR(200) NOT NULL
                            );

                            INSERT INTO SnackRack.OrderStatus(Id, `Name`, `Description`)
                            VALUES
                            (1,'Completed','Snack was sold'),
                            (2,'Refunded','Customer wanted there money back for snack after it was given'),
                            (3,'Cancelled','Customer asked for money back before item was prepared');");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
