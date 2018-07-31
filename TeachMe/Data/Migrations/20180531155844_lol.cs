using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace TeachMe.Data.Migrations
{
    public partial class lol : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserShortInfoId",
                table: "UserCourse",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "UserShortInfo",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Email = table.Column<string>(nullable: true),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserShortInfo", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserCourse_UserShortInfoId",
                table: "UserCourse",
                column: "UserShortInfoId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserCourse_UserShortInfo_UserShortInfoId",
                table: "UserCourse",
                column: "UserShortInfoId",
                principalTable: "UserShortInfo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserCourse_UserShortInfo_UserShortInfoId",
                table: "UserCourse");

            migrationBuilder.DropTable(
                name: "UserShortInfo");

            migrationBuilder.DropIndex(
                name: "IX_UserCourse_UserShortInfoId",
                table: "UserCourse");

            migrationBuilder.DropColumn(
                name: "UserShortInfoId",
                table: "UserCourse");
        }
    }
}
