using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplicationBackEnd.Migrations
{
    public partial class Datos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Datos",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    genero = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    desarrolladores = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    plataformas = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    primerlanzamiento = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Videojuegoid = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Datos", x => x.id);
                    table.ForeignKey(
                        name: "FK_Datos_Videojuegos_Videojuegoid",
                        column: x => x.Videojuegoid,
                        principalTable: "Videojuegos",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Datos_Videojuegoid",
                table: "Datos",
                column: "Videojuegoid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Datos");
        }
    }
}
