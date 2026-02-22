using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Torneos.Api.Migrations
{
    /// <inheritdoc />
    public partial class LigaV1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TorneosEquipos_Equipos_EquipoId",
                table: "TorneosEquipos");

            migrationBuilder.AddColumn<string>(
                name: "Grupo",
                table: "TorneosEquipos",
                type: "character varying(2)",
                unicode: false,
                maxLength: 2,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CantidadRuedas",
                table: "Torneos",
                type: "integer",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.AddColumn<int>(
                name: "Estado",
                table: "Torneos",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PuntosDerrota",
                table: "Torneos",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PuntosEmpate",
                table: "Torneos",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PuntosVictoria",
                table: "Torneos",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Tipo",
                table: "Torneos",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Estado",
                table: "Partidos",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<long>(
                name: "GanadorEquipoId",
                table: "Partidos",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "GolesLocalAlargue",
                table: "Partidos",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "GolesVisitanteAlargue",
                table: "Partidos",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Llave",
                table: "Partidos",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PenalesLocal",
                table: "Partidos",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PenalesVisitante",
                table: "Partidos",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Ronda",
                table: "Partidos",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TipoDefinicion",
                table: "Partidos",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Partidos_TorneoId_Estado",
                table: "Partidos",
                columns: new[] { "TorneoId", "Estado" });

            migrationBuilder.CreateIndex(
                name: "IX_Partidos_TorneoId_Ronda",
                table: "Partidos",
                columns: new[] { "TorneoId", "Ronda" });

            migrationBuilder.CreateIndex(
                name: "IX_Partidos_TorneoId_Ronda_LocalEquipoId_VisitanteEquipoId",
                table: "Partidos",
                columns: new[] { "TorneoId", "Ronda", "LocalEquipoId", "VisitanteEquipoId" },
                unique: true);

            migrationBuilder.AddCheckConstraint(
                name: "CK_Partidos_Local_Distinto_Visitante",
                table: "Partidos",
                sql: "\"LocalEquipoId\" <> \"VisitanteEquipoId\"");

            migrationBuilder.AddForeignKey(
                name: "FK_TorneosEquipos_Equipos_EquipoId",
                table: "TorneosEquipos",
                column: "EquipoId",
                principalTable: "Equipos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TorneosEquipos_Equipos_EquipoId",
                table: "TorneosEquipos");

            migrationBuilder.DropIndex(
                name: "IX_Partidos_TorneoId_Estado",
                table: "Partidos");

            migrationBuilder.DropIndex(
                name: "IX_Partidos_TorneoId_Ronda",
                table: "Partidos");

            migrationBuilder.DropIndex(
                name: "IX_Partidos_TorneoId_Ronda_LocalEquipoId_VisitanteEquipoId",
                table: "Partidos");

            migrationBuilder.DropCheckConstraint(
                name: "CK_Partidos_Local_Distinto_Visitante",
                table: "Partidos");

            migrationBuilder.DropColumn(
                name: "Grupo",
                table: "TorneosEquipos");

            migrationBuilder.DropColumn(
                name: "CantidadRuedas",
                table: "Torneos");

            migrationBuilder.DropColumn(
                name: "Estado",
                table: "Torneos");

            migrationBuilder.DropColumn(
                name: "PuntosDerrota",
                table: "Torneos");

            migrationBuilder.DropColumn(
                name: "PuntosEmpate",
                table: "Torneos");

            migrationBuilder.DropColumn(
                name: "PuntosVictoria",
                table: "Torneos");

            migrationBuilder.DropColumn(
                name: "Tipo",
                table: "Torneos");

            migrationBuilder.DropColumn(
                name: "Estado",
                table: "Partidos");

            migrationBuilder.DropColumn(
                name: "GanadorEquipoId",
                table: "Partidos");

            migrationBuilder.DropColumn(
                name: "GolesLocalAlargue",
                table: "Partidos");

            migrationBuilder.DropColumn(
                name: "GolesVisitanteAlargue",
                table: "Partidos");

            migrationBuilder.DropColumn(
                name: "Llave",
                table: "Partidos");

            migrationBuilder.DropColumn(
                name: "PenalesLocal",
                table: "Partidos");

            migrationBuilder.DropColumn(
                name: "PenalesVisitante",
                table: "Partidos");

            migrationBuilder.DropColumn(
                name: "Ronda",
                table: "Partidos");

            migrationBuilder.DropColumn(
                name: "TipoDefinicion",
                table: "Partidos");

            migrationBuilder.AddForeignKey(
                name: "FK_TorneosEquipos_Equipos_EquipoId",
                table: "TorneosEquipos",
                column: "EquipoId",
                principalTable: "Equipos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
