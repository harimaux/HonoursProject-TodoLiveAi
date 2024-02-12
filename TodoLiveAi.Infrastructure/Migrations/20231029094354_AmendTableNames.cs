using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TodoLiveAi.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AmendTableNames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TestingColumnt",
                table: "TestingDB",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TestingColumnt",
                table: "TestingDB");
        }
    }
}
