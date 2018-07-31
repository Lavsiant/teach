using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace TeachMe.Data.Migrations
{
    public partial class s : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId1",
                table: "CString",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId2",
                table: "CString",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CString_ApplicationUserId1",
                table: "CString",
                column: "ApplicationUserId1");

            migrationBuilder.CreateIndex(
                name: "IX_CString_ApplicationUserId2",
                table: "CString",
                column: "ApplicationUserId2");

            migrationBuilder.AddForeignKey(
                name: "FK_CString_AspNetUsers_ApplicationUserId1",
                table: "CString",
                column: "ApplicationUserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CString_AspNetUsers_ApplicationUserId2",
                table: "CString",
                column: "ApplicationUserId2",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CString_AspNetUsers_ApplicationUserId1",
                table: "CString");

            migrationBuilder.DropForeignKey(
                name: "FK_CString_AspNetUsers_ApplicationUserId2",
                table: "CString");

            migrationBuilder.DropIndex(
                name: "IX_CString_ApplicationUserId1",
                table: "CString");

            migrationBuilder.DropIndex(
                name: "IX_CString_ApplicationUserId2",
                table: "CString");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId1",
                table: "CString");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId2",
                table: "CString");
        }
    }
}
