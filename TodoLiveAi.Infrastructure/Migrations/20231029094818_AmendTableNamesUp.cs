using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TodoLiveAi.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AmendTableNamesUp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaskDB_AspNetUsers_OwnerId",
                table: "TaskDB");

            migrationBuilder.DropTable(
                name: "TestingDB");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TaskPriorityDB",
                table: "TaskPriorityDB");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TaskDB",
                table: "TaskDB");

            migrationBuilder.RenameTable(
                name: "TaskPriorityDB",
                newName: "TaskPriority");

            migrationBuilder.RenameTable(
                name: "TaskDB",
                newName: "TodoTask");

            migrationBuilder.RenameIndex(
                name: "IX_TaskDB_OwnerId",
                table: "TodoTask",
                newName: "IX_TodoTask_OwnerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TaskPriority",
                table: "TaskPriority",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TodoTask",
                table: "TodoTask",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TodoTask_AspNetUsers_OwnerId",
                table: "TodoTask",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TodoTask_AspNetUsers_OwnerId",
                table: "TodoTask");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TodoTask",
                table: "TodoTask");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TaskPriority",
                table: "TaskPriority");

            migrationBuilder.RenameTable(
                name: "TodoTask",
                newName: "TaskDB");

            migrationBuilder.RenameTable(
                name: "TaskPriority",
                newName: "TaskPriorityDB");

            migrationBuilder.RenameIndex(
                name: "IX_TodoTask_OwnerId",
                table: "TaskDB",
                newName: "IX_TaskDB_OwnerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TaskDB",
                table: "TaskDB",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TaskPriorityDB",
                table: "TaskPriorityDB",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "TestingDB",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Author = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email4 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email5 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TestingColumnt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestingDB", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_TaskDB_AspNetUsers_OwnerId",
                table: "TaskDB",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
