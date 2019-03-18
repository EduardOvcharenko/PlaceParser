using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PlaceParser.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Places",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    GoogleId = table.Column<Guid>(nullable: false),
                    PlaceId = table.Column<Guid>(nullable: false),
                    PlaceType = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Rating = table.Column<string>(nullable: true),
                    Adress = table.Column<string>(nullable: true),
                    Location = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Places", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PlaceTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlaceTypes", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Places");

            migrationBuilder.DropTable(
                name: "PlaceTypes");
        }
    }
}
