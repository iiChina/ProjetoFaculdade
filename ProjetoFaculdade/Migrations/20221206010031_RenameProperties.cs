using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjetoFaculdade.Migrations
{
    /// <inheritdoc />
    public partial class RenameProperties : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Produto_Forecedor_fornecedorId",
                table: "Produto");

            migrationBuilder.RenameColumn(
                name: "fornecedorId",
                table: "Produto",
                newName: "FornecedorId");

            migrationBuilder.RenameIndex(
                name: "IX_Produto_fornecedorId",
                table: "Produto",
                newName: "IX_Produto_FornecedorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Produto_Forecedor_FornecedorId",
                table: "Produto",
                column: "FornecedorId",
                principalTable: "Forecedor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Produto_Forecedor_FornecedorId",
                table: "Produto");

            migrationBuilder.RenameColumn(
                name: "FornecedorId",
                table: "Produto",
                newName: "fornecedorId");

            migrationBuilder.RenameIndex(
                name: "IX_Produto_FornecedorId",
                table: "Produto",
                newName: "IX_Produto_fornecedorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Produto_Forecedor_fornecedorId",
                table: "Produto",
                column: "fornecedorId",
                principalTable: "Forecedor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
