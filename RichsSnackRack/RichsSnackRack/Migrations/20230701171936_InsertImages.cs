using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RichsSnackRack.Migrations
{
    /// <inheritdoc />
    public partial class InsertImages : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"UPDATE Snacks snk SET snk.ImageUrl = 'pizza-slice.png' WHERE snk.`Name` = 'Pizza';
                    UPDATE Snacks snk SET snk.ImageUrl = 'pepsi-can.jpg' WHERE snk.`Name` = 'Pepsi';
                    UPDATE Snacks snk SET snk.ImageUrl = 'ham-burger.png' WHERE snk.`Name` = 'Hamburger';
                    UPDATE Snacks snk SET snk.ImageUrl = 'coke-can.jpg' WHERE snk.`Name` = 'Coke';
                    UPDATE Snacks snk SET snk.ImageUrl = 'yogurt-cup.jpg' WHERE snk.`Name` = 'Yogurt';
                    UPDATE Snacks snk SET snk.ImageUrl = 'apple.jpg' WHERE snk.`Name` = 'Apple';
                    UPDATE Snacks snk SET snk.ImageUrl = 'yogurt-cup.jpg' WHERE snk.`Name` = 'Yogurt';
                    UPDATE Snacks snk SET snk.ImageUrl = 'banana.jpg' WHERE snk.`Name` = 'Banana';
                    UPDATE Snacks snk SET snk.ImageUrl = 'turkey-sandwhich.jpg' WHERE snk.`Name` = 'Turkey Sandwhich';");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
