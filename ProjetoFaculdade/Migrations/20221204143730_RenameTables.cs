using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjetoFaculdade.Migrations
{
    /// <inheritdoc />
    public partial class RenameTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_produtos_fornecedores_fornecedorId",
                table: "produtos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_produtos",
                table: "produtos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_fornecedores",
                table: "fornecedores");

            migrationBuilder.RenameTable(
                name: "produtos",
                newName: "Produto");

            migrationBuilder.RenameTable(
                name: "fornecedores",
                newName: "Forecedor");

            migrationBuilder.RenameIndex(
                name: "IX_produtos_fornecedorId",
                table: "Produto",
                newName: "IX_Produto_fornecedorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Produto",
                table: "Produto",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Forecedor",
                table: "Forecedor",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Produto_Forecedor_fornecedorId",
                table: "Produto",
                column: "fornecedorId",
                principalTable: "Forecedor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Produto_Forecedor_fornecedorId",
                table: "Produto");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Produto",
                table: "Produto");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Forecedor",
                table: "Forecedor");

            migrationBuilder.RenameTable(
                name: "Produto",
                newName: "produtos");

            migrationBuilder.RenameTable(
                name: "Forecedor",
                newName: "fornecedores");

            migrationBuilder.RenameIndex(
                name: "IX_Produto_fornecedorId",
                table: "produtos",
                newName: "IX_produtos_fornecedorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_produtos",
                table: "produtos",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_fornecedores",
                table: "fornecedores",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_produtos_fornecedores_fornecedorId",
                table: "produtos",
                column: "fornecedorId",
                principalTable: "fornecedores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
