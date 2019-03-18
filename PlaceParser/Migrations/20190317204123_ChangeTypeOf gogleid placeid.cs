using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PlaceParser.Migrations
{
    public partial class ChangeTypeOfgogleidplaceid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PlaceId",
                table: "Places",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AlterColumn<string>(
                name: "GoogleId",
                table: "Places",
                nullable: true,
                oldClrType: typeof(Guid));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "PlaceId",
                table: "Places",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "GoogleId",
                table: "Places",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
