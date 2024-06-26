using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Assessment.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "sqlModels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    date = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    trade_code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    high = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    low = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    open = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    close = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    volume = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sqlModels", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "sqlModels");
        }
    }
}
