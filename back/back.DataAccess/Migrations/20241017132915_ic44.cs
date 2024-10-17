using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace back.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class ic44 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "UserEntityId",
                table: "Diploma",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Diploma_UserEntityId",
                table: "Diploma",
                column: "UserEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Diploma_Users_UserEntityId",
                table: "Diploma",
                column: "UserEntityId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Diploma_Users_UserEntityId",
                table: "Diploma");

            migrationBuilder.DropIndex(
                name: "IX_Diploma_UserEntityId",
                table: "Diploma");

            migrationBuilder.DropColumn(
                name: "UserEntityId",
                table: "Diploma");
        }
    }
}
