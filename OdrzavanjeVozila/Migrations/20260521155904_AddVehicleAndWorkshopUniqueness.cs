using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OdrzavanjeVozila.Migrations
{
    /// <inheritdoc />
    public partial class AddVehicleAndWorkshopUniqueness : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Naziv",
                table: "Radionice",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Adresa",
                table: "Radionice",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Specijalizacija",
                table: "Mehanicari",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Prezime",
                table: "Mehanicari",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Ime",
                table: "Mehanicari",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "RegistracijskiBroj",
                table: "Automobili",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "BrojSasije",
                table: "Automobili",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.UpdateData(
                table: "Automobili",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DatumPrvogServisa", "Model" },
                values: new object[] { new DateTime(2023, 11, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "320" });

            migrationBuilder.UpdateData(
                table: "Automobili",
                keyColumn: "Id",
                keyValue: 3,
                column: "DatumPrvogServisa",
                value: new DateTime(2024, 8, 12, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_Automobili_BrojSasije",
                table: "Automobili",
                column: "BrojSasije",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Automobili_RegistracijskiBroj",
                table: "Automobili",
                column: "RegistracijskiBroj",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Mehanicari_Ime_Prezime_Specijalizacija_RadionicaId",
                table: "Mehanicari",
                columns: new[] { "Ime", "Prezime", "Specijalizacija", "RadionicaId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Radionice_Naziv_Adresa",
                table: "Radionice",
                columns: new[] { "Naziv", "Adresa" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Naziv",
                table: "Radionice",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "Adresa",
                table: "Radionice",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "Specijalizacija",
                table: "Mehanicari",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "Prezime",
                table: "Mehanicari",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "Ime",
                table: "Mehanicari",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "RegistracijskiBroj",
                table: "Automobili",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "BrojSasije",
                table: "Automobili",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.UpdateData(
                table: "Automobili",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DatumPrvogServisa", "Model" },
                values: new object[] { new DateTime(2024, 9, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "320d" });

            migrationBuilder.UpdateData(
                table: "Automobili",
                keyColumn: "Id",
                keyValue: 3,
                column: "DatumPrvogServisa",
                value: new DateTime(2024, 11, 5, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.DropIndex(
                name: "IX_Automobili_BrojSasije",
                table: "Automobili");

            migrationBuilder.DropIndex(
                name: "IX_Automobili_RegistracijskiBroj",
                table: "Automobili");

            migrationBuilder.DropIndex(
                name: "IX_Mehanicari_Ime_Prezime_Specijalizacija_RadionicaId",
                table: "Mehanicari");

            migrationBuilder.DropIndex(
                name: "IX_Radionice_Naziv_Adresa",
                table: "Radionice");
        }
    }
}
