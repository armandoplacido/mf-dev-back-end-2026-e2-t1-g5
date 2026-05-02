using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace mf_dev_back_end_2026_e2_t1_g5.Migrations
{
    /// <inheritdoc />
    public partial class M05ChangePublicIdConsumoToUnique : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Consumo_PublicId",
                table: "Consumo",
                column: "PublicId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Consumo_PublicId",
                table: "Consumo");
        }
    }
}
