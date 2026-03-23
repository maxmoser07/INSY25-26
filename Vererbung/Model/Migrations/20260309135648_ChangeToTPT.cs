using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Model.Migrations
{
    /// <inheritdoc />
    public partial class ChangeToTPT : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Dogs");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Birds");

            migrationBuilder.AlterColumn<string>(
                name: "Breed",
                table: "Dogs",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext");

            migrationBuilder.AlterColumn<string>(
                name: "Wingspan",
                table: "Birds",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext");

            migrationBuilder.CreateTable(
                name: "Animals",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Animals", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.AddForeignKey(
                name: "FK_Birds_Animals_Id",
                table: "Birds",
                column: "Id",
                principalTable: "Animals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Dogs_Animals_Id",
                table: "Dogs",
                column: "Id",
                principalTable: "Animals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Birds_Animals_Id",
                table: "Birds");

            migrationBuilder.DropForeignKey(
                name: "FK_Dogs_Animals_Id",
                table: "Dogs");

            migrationBuilder.DropTable(
                name: "Animals");

            migrationBuilder.AlterColumn<string>(
                name: "Breed",
                table: "Dogs",
                type: "longtext",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Dogs",
                type: "longtext",
                nullable: false);

            migrationBuilder.AlterColumn<string>(
                name: "Wingspan",
                table: "Birds",
                type: "longtext",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Birds",
                type: "longtext",
                nullable: false);
        }
    }
}
