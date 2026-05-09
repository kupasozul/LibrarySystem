using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibrarySystem.Api.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSeedDataWithNewFieldNames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Loans_Books_BookNumber1",
                table: "Loans");

            migrationBuilder.RenameColumn(
                name: "BookNumber1",
                table: "Loans",
                newName: "BookInventoryNumber");

            migrationBuilder.RenameIndex(
                name: "IX_Loans_BookNumber1",
                table: "Loans",
                newName: "IX_Loans_BookInventoryNumber");

            migrationBuilder.RenameColumn(
                name: "PublicationYear",
                table: "Books",
                newName: "ReleaseYear");

            migrationBuilder.RenameColumn(
                name: "BookNumber",
                table: "Books",
                newName: "InventoryNumber");

            migrationBuilder.AddColumn<bool>(
                name: "IsBorrowed",
                table: "Books",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "InventoryNumber",
                keyValue: 1,
                column: "IsBorrowed",
                value: false);

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "InventoryNumber",
                keyValue: 2,
                column: "IsBorrowed",
                value: false);

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "InventoryNumber",
                keyValue: 3,
                column: "IsBorrowed",
                value: false);

            migrationBuilder.AddForeignKey(
                name: "FK_Loans_Books_BookInventoryNumber",
                table: "Loans",
                column: "BookInventoryNumber",
                principalTable: "Books",
                principalColumn: "InventoryNumber");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Loans_Books_BookInventoryNumber",
                table: "Loans");

            migrationBuilder.DropColumn(
                name: "IsBorrowed",
                table: "Books");

            migrationBuilder.RenameColumn(
                name: "BookInventoryNumber",
                table: "Loans",
                newName: "BookNumber1");

            migrationBuilder.RenameIndex(
                name: "IX_Loans_BookInventoryNumber",
                table: "Loans",
                newName: "IX_Loans_BookNumber1");

            migrationBuilder.RenameColumn(
                name: "ReleaseYear",
                table: "Books",
                newName: "PublicationYear");

            migrationBuilder.RenameColumn(
                name: "InventoryNumber",
                table: "Books",
                newName: "BookNumber");

            migrationBuilder.AddForeignKey(
                name: "FK_Loans_Books_BookNumber1",
                table: "Loans",
                column: "BookNumber1",
                principalTable: "Books",
                principalColumn: "BookNumber");
        }
    }
}
