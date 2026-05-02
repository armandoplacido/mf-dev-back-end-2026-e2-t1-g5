using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace mf_dev_back_end_2026_e2_t1_g5.Migrations
{
    /// <inheritdoc />
    public partial class M07AlterColumnEmailToTableUsuario : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "email",
                table: "Usuarios",
                newName: "Email");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Email",
                table: "Usuarios",
                newName: "email");
        }
    }
}
