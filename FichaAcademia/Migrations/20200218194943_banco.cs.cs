using Microsoft.EntityFrameworkCore.Migrations;

namespace FichaAcademia.Migrations
{
    public partial class bancocs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CategoriaExercicioID",
                table: "CategoriasExercicios",
                newName: "CategoriaExercicioId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CategoriaExercicioId",
                table: "CategoriasExercicios",
                newName: "CategoriaExercicioID");
        }
    }
}
