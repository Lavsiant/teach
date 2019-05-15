using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace TeachMe.Data.Migrations
{
    public partial class BigNew : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
          

            migrationBuilder.AddColumn<int>(
                name: "Category",
                table: "Course",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DurationID",
                table: "Course",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Subject",
                table: "Course",
                maxLength: 60,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TeacherInfoID",
                table: "Course",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Biography",
                table: "AspNetUsers",
                maxLength: 300,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Skype",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "CDayOfWeak",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CourseID = table.Column<int>(nullable: true),
                    EndTime = table.Column<int>(nullable: false),
                    IsWorkDay = table.Column<bool>(nullable: false),
                    StartTime = table.Column<int>(nullable: false),
                    WeekDay = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CDayOfWeak", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CDayOfWeak_Course_CourseID",
                        column: x => x.CourseID,
                        principalTable: "Course",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CourseLesson",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CourseID = table.Column<int>(nullable: true),
                    EndLessonTime = table.Column<DateTime>(nullable: false),
                    StartLessonTime = table.Column<DateTime>(nullable: false),
                    WeekDay = table.Column<int>(nullable: false),
                    isBusy = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseLesson", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CourseLesson_Course_CourseID",
                        column: x => x.CourseID,
                        principalTable: "Course",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CourseMark",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CourseID = table.Column<int>(nullable: true),
                    Mark = table.Column<double>(nullable: false),
                    RaterId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseMark", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CourseMark_Course_CourseID",
                        column: x => x.CourseID,
                        principalTable: "Course",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CourseTeacherInfo",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Email = table.Column<string>(nullable: true),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Skype = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseTeacherInfo", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "LessonTime",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Hours = table.Column<int>(nullable: false),
                    Minutes = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LessonTime", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TeacherRating",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ApplicationUserId = table.Column<string>(nullable: true),
                    Mark = table.Column<double>(nullable: false),
                    RaterId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeacherRating", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TeacherRating_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Course_DurationID",
                table: "Course",
                column: "DurationID");

            migrationBuilder.CreateIndex(
                name: "IX_Course_TeacherInfoID",
                table: "Course",
                column: "TeacherInfoID");

            migrationBuilder.CreateIndex(
                name: "IX_CDayOfWeak_CourseID",
                table: "CDayOfWeak",
                column: "CourseID");

            migrationBuilder.CreateIndex(
                name: "IX_CourseLesson_CourseID",
                table: "CourseLesson",
                column: "CourseID");

            migrationBuilder.CreateIndex(
                name: "IX_CourseMark_CourseID",
                table: "CourseMark",
                column: "CourseID");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherRating_ApplicationUserId",
                table: "TeacherRating",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Course_LessonTime_DurationID",
                table: "Course",
                column: "DurationID",
                principalTable: "LessonTime",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Course_CourseTeacherInfo_TeacherInfoID",
                table: "Course",
                column: "TeacherInfoID",
                principalTable: "CourseTeacherInfo",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Course_LessonTime_DurationID",
                table: "Course");

            migrationBuilder.DropForeignKey(
                name: "FK_Course_CourseTeacherInfo_TeacherInfoID",
                table: "Course");

            migrationBuilder.DropTable(
                name: "CDayOfWeak");

            migrationBuilder.DropTable(
                name: "CourseLesson");

            migrationBuilder.DropTable(
                name: "CourseMark");

            migrationBuilder.DropTable(
                name: "CourseTeacherInfo");

            migrationBuilder.DropTable(
                name: "LessonTime");

            migrationBuilder.DropTable(
                name: "TeacherRating");

            migrationBuilder.DropIndex(
                name: "IX_Course_DurationID",
                table: "Course");

            migrationBuilder.DropIndex(
                name: "IX_Course_TeacherInfoID",
                table: "Course");

            migrationBuilder.DropColumn(
                name: "Category",
                table: "Course");

            migrationBuilder.DropColumn(
                name: "DurationID",
                table: "Course");

            migrationBuilder.DropColumn(
                name: "Subject",
                table: "Course");

            migrationBuilder.DropColumn(
                name: "TeacherInfoID",
                table: "Course");

            migrationBuilder.DropColumn(
                name: "Biography",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Skype",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "Course",
                maxLength: 30,
                nullable: false,
                defaultValue: "");
        }
    }
}
