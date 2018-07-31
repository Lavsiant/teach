using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace TeachMe.Data.Migrations
{
    public partial class Stream : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StreamID",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "StreamInfo",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    StreamLink = table.Column<string>(nullable: true),
                    StreamTIttle = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StreamInfo", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_StreamID",
                table: "AspNetUsers",
                column: "StreamID");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_StreamInfo_StreamID",
                table: "AspNetUsers",
                column: "StreamID",
                principalTable: "StreamInfo",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_StreamInfo_StreamID",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "StreamInfo");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_StreamID",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "StreamID",
                table: "AspNetUsers");
        }
    }
}
