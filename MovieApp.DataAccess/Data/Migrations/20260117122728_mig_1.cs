using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MovieApp.DataAccess.Data.Migrations
{
    /// <inheritdoc />
    public partial class mig_1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Directors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    City = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Age = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Directors", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Directors",
                columns: new[] { "Id", "Address", "Age", "City", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "123 Hollywood Blvd, Los Angeles, CA", 74, "Los Angeles", "An American filmmaker known for his work in the adventure and science fiction genres.", "Steven Spielberg" },
                    { 2, "456 Sunset St, Los Angeles, CA", 50, "Los Angeles", "A British-American filmmaker known for his complex storytelling and innovative techniques.", "Christopher Nolan" },
                    { 3, "789 Vine St, Los Angeles, CA", 58, "Los Angeles", "An American filmmaker known for his stylized violence and sharp dialogue.", "Quentin Tarantino" },
                    { 4, "321 Hollywood Blvd, New York, NY", 78, "New York", "An American filmmaker known for his work on crime films and character studies.", "Martin Scorsese" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Directors");
        }
    }
}
