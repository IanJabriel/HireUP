using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HireUP.Infra.Migrations
{
    /// <inheritdoc />
    public partial class EnterpriseRelationsWithEventAndHackton : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // 1. Criar uma Enterprise padrão para registros órfãos
            migrationBuilder.Sql(@"
                IF NOT EXISTS (SELECT 1 FROM Enterprises WHERE Id = 1)
                BEGIN
                    SET IDENTITY_INSERT Enterprises ON;
                    INSERT INTO Enterprises (Id, Name, Phone, Email, Password, Document, CreatedAt)
                    VALUES (1, 'Empresa Padrão', '0000000000', 'default@empresa.com', 'temp', '00000000000000', GETDATE());
                    SET IDENTITY_INSERT Enterprises OFF;
                END
            ");

            // 2. Adicionar coluna EnterpriseId na tabela Events
            migrationBuilder.AddColumn<int>(
                name: "EnterpriseId",
                table: "Events",
                type: "int",
                nullable: false,
                defaultValue: 1); // Valor padrão temporário

            // 3. Adicionar coluna EnterpriseId na tabela Hackatons
            migrationBuilder.AddColumn<int>(
                name: "EnterpriseId",
                table: "Hackatons",
                type: "int",
                nullable: false,
                defaultValue: 1); // Valor padrão temporário

            // 4. Criar índices
            migrationBuilder.CreateIndex(
                name: "IX_Events_EnterpriseId",
                table: "Events",
                column: "EnterpriseId");

            migrationBuilder.CreateIndex(
                name: "IX_Hackatons_EnterpriseId",
                table: "Hackatons",
                column: "EnterpriseId");

            // 5. Criar as Foreign Keys
            migrationBuilder.AddForeignKey(
                name: "FK_Events_Enterprises_EnterpriseId",
                table: "Events",
                column: "EnterpriseId",
                principalTable: "Enterprises",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Hackatons_Enterprises_EnterpriseId",
                table: "Hackatons",
                column: "EnterpriseId",
                principalTable: "Enterprises",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_Enterprises_EnterpriseId",
                table: "Events");

            migrationBuilder.DropForeignKey(
                name: "FK_Hackatons_Enterprises_EnterpriseId",
                table: "Hackatons");

            migrationBuilder.DropIndex(
                name: "IX_Events_EnterpriseId",
                table: "Events");

            migrationBuilder.DropIndex(
                name: "IX_Hackatons_EnterpriseId",
                table: "Hackatons");

            migrationBuilder.DropColumn(
                name: "EnterpriseId",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "EnterpriseId",
                table: "Hackatons");
        }
    }
}
