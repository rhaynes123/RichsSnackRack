using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RichsSnackRack.Migrations
{
    /// <inheritdoc />
    public partial class InsertSnacks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string insertql = @"INSERT INTO Snacks(`Name`,Price)
                                VALUES
                                ('Pizza',2.99),
                                ('Pepsi',1.99),
                                ('Hamburger',3.99),
                                ('Coke',2.99),
                                ('Yogurt',1.99),
                                ('Apple',0.00),
                                ('Bannana',0.00),
                                ('Turkey Sandwhich',3.99);";
            migrationBuilder.Sql(insertql);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
