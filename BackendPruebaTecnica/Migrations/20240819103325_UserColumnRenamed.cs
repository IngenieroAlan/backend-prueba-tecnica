using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackendPruebaTecnica.Migrations
{
    /// <inheritdoc />
    public partial class UserColumnRenamed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FechaRegistro",
                table: "Users",
                newName: "RegisterDate");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RegisterDate",
                table: "Users",
                newName: "FechaRegistro");
        }
    }
}
