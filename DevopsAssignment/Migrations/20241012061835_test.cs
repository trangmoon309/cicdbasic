using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DevopsAssignment.Migrations
{
    /// <inheritdoc />
    public partial class test : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
            IF NOT EXISTS (
                SELECT * 
                FROM INFORMATION_SCHEMA.TABLES 
                WHERE TABLE_NAME = 'Products'
            )
            BEGIN
                CREATE TABLE Products (
                    Id INT IDENTITY(1,1) PRIMARY KEY,
                    Name NVARCHAR(MAX),
                    ImageUrl NVARCHAR(MAX)
                );
            END;
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
