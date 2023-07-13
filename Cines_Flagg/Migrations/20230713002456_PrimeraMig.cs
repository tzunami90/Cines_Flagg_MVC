using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cines_Flagg.Migrations
{
    /// <inheritdoc />
    public partial class PrimeraMig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Peliculas",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "varchar(50)", nullable: true),
                    Sinopsis = table.Column<string>(type: "varchar(200)", nullable: true),
                    Duracion = table.Column<int>(type: "int", nullable: false),
                    Poster = table.Column<string>(type: "varchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Peliculas", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Sala",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ubicacion = table.Column<string>(type: "varchar(100)", nullable: true),
                    Capacidad = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sala", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "varchar(50)", nullable: true),
                    Apellido = table.Column<string>(type: "varchar(50)", nullable: true),
                    DNI = table.Column<string>(type: "varchar(50)", nullable: false),
                    Mail = table.Column<string>(type: "varchar(50)", nullable: true),
                    Password = table.Column<string>(type: "varchar(50)", nullable: true),
                    IntentosFallidos = table.Column<int>(type: "int", nullable: false),
                    Bloqueado = table.Column<bool>(type: "bit", nullable: false),
                    Credito = table.Column<double>(type: "float", nullable: false),
                    FechaNacimiento = table.Column<DateTime>(type: "date", nullable: false),
                    EsAdmin = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Funciones",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idSala = table.Column<int>(type: "int", nullable: false),
                    idPelicula = table.Column<int>(type: "int", nullable: false),
                    Fecha = table.Column<DateTime>(type: "date", nullable: false),
                    CantClientes = table.Column<int>(type: "int", nullable: false),
                    Costo = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Funciones", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Funciones_Peliculas_idPelicula",
                        column: x => x.idPelicula,
                        principalTable: "Peliculas",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Funciones_Sala_idSala",
                        column: x => x.idSala,
                        principalTable: "Sala",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UF",
                columns: table => new
                {
                    idUsuario = table.Column<int>(type: "int", nullable: false),
                    idFuncion = table.Column<int>(type: "int", nullable: false),
                    cantidadCompra = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UF", x => new { x.idUsuario, x.idFuncion });
                    table.ForeignKey(
                        name: "FK_UF_Funciones_idFuncion",
                        column: x => x.idFuncion,
                        principalTable: "Funciones",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UF_Usuarios_idUsuario",
                        column: x => x.idUsuario,
                        principalTable: "Usuarios",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Usuarios",
                columns: new[] { "ID", "Apellido", "Bloqueado", "Credito", "DNI", "EsAdmin", "FechaNacimiento", "IntentosFallidos", "Mail", "Nombre", "Password" },
                values: new object[] { 1, "Gomez", false, 0.0, "45678964", true, new DateTime(1974, 5, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "m@mail", "Obdulio", "123" });

            migrationBuilder.CreateIndex(
                name: "IX_Funciones_idPelicula",
                table: "Funciones",
                column: "idPelicula");

            migrationBuilder.CreateIndex(
                name: "IX_Funciones_idSala",
                table: "Funciones",
                column: "idSala");

            migrationBuilder.CreateIndex(
                name: "IX_UF_idFuncion",
                table: "UF",
                column: "idFuncion");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UF");

            migrationBuilder.DropTable(
                name: "Funciones");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "Peliculas");

            migrationBuilder.DropTable(
                name: "Sala");
        }
    }
}
