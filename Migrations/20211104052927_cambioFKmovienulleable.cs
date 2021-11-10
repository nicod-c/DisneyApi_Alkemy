using Microsoft.EntityFrameworkCore.Migrations;

namespace AlkemyDisney.Migrations
{
    public partial class cambioFKmovienulleable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PeliculasYSeries_Generos_GeneroId",
                table: "PeliculasYSeries");

            migrationBuilder.AlterColumn<int>(
                name: "GeneroId",
                table: "PeliculasYSeries",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_PeliculasYSeries_Generos_GeneroId",
                table: "PeliculasYSeries",
                column: "GeneroId",
                principalTable: "Generos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PeliculasYSeries_Generos_GeneroId",
                table: "PeliculasYSeries");

            migrationBuilder.AlterColumn<int>(
                name: "GeneroId",
                table: "PeliculasYSeries",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PeliculasYSeries_Generos_GeneroId",
                table: "PeliculasYSeries",
                column: "GeneroId",
                principalTable: "Generos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
