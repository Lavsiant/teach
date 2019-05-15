using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace TeachMe.Data.Migrations
{
    public partial class xxx : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserCourse_Course_CourseID",
                table: "UserCourse");

            migrationBuilder.DropIndex(
                name: "IX_UserCourse_CourseID",
                table: "UserCourse");

            migrationBuilder.DropColumn(
                name: "CourseID",
                table: "UserCourse");

            migrationBuilder.AddColumn<string>(
                name: "CourseTittle",
                table: "UserCourse",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CourseTittle",
                table: "UserCourse");

            migrationBuilder.AddColumn<int>(
                name: "CourseID",
                table: "UserCourse",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserCourse_CourseID",
                table: "UserCourse",
                column: "CourseID");

            migrationBuilder.AddForeignKey(
                name: "FK_UserCourse_Course_CourseID",
                table: "UserCourse",
                column: "CourseID",
                principalTable: "Course",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
