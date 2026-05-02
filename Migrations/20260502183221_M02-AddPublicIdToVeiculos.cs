using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace mf_dev_back_end_2026_e2_t1_g5.Migrations
{
    /// <inheritdoc />
    public partial class M02AddPublicIdToVeiculos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "PublicId",
                table: "Veiculos",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Veiculos_PublicId",
                table: "Veiculos",
                column: "PublicId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Veiculos_PublicId",
                table: "Veiculos");

            migrationBuilder.DropColumn(
                name: "PublicId",
                table: "Veiculos");
        }
    }
}
