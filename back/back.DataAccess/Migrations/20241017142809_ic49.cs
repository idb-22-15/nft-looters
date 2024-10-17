using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace back.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class ic49 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Certificates_Users_UserEntityId",
                table: "Certificates");

            migrationBuilder.DropForeignKey(
                name: "FK_Diploma_Users_UserEntityId",
                table: "Diploma");

            migrationBuilder.DropIndex(
                name: "IX_Diploma_UserEntityId",
                table: "Diploma");

            migrationBuilder.DropIndex(
                name: "IX_Certificates_UserEntityId",
                table: "Certificates");

            migrationBuilder.DropColumn(
                name: "UserEntityId",
                table: "Diploma");

            migrationBuilder.DropColumn(
                name: "UserEntityId",
                table: "Certificates");

            migrationBuilder.CreateIndex(
                name: "IX_Diploma_UserId",
                table: "Diploma",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Certificates_UserId",
                table: "Certificates",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Certificates_Users_UserId",
                table: "Certificates",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Diploma_Users_UserId",
                table: "Diploma",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Certificates_Users_UserId",
                table: "Certificates");

            migrationBuilder.DropForeignKey(
                name: "FK_Diploma_Users_UserId",
                table: "Diploma");

            migrationBuilder.DropIndex(
                name: "IX_Diploma_UserId",
                table: "Diploma");

            migrationBuilder.DropIndex(
                name: "IX_Certificates_UserId",
                table: "Certificates");

            migrationBuilder.AddColumn<Guid>(
                name: "UserEntityId",
                table: "Diploma",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UserEntityId",
                table: "Certificates",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Diploma_UserEntityId",
                table: "Diploma",
                column: "UserEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_Certificates_UserEntityId",
                table: "Certificates",
                column: "UserEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Certificates_Users_UserEntityId",
                table: "Certificates",
                column: "UserEntityId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Diploma_Users_UserEntityId",
                table: "Diploma",
                column: "UserEntityId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
