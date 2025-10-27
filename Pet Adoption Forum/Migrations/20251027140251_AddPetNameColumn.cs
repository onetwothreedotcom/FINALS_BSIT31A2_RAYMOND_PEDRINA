using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pet_Adoption_Forum.Migrations
{
    /// <inheritdoc />
    public partial class AddPetNameColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PetName",
                table: "AdoptionRequests",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "RequestDate",
                table: "AdoptionRequests",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "AdoptionRequests",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PetName",
                table: "AdoptionRequests");

            migrationBuilder.DropColumn(
                name: "RequestDate",
                table: "AdoptionRequests");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "AdoptionRequests");
        }
    }
}
