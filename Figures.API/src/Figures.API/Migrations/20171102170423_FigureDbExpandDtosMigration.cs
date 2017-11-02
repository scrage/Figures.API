using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Figures.API.Migrations
{
    public partial class FigureDbExpandDtosMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Aka",
                table: "Figures");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Figures",
                maxLength: 25,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "Figures",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "Figures",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Figures",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Alias",
                table: "Figures",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FigureType",
                table: "Figures",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsLastNameFirst",
                table: "Figures",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "MiddleName",
                table: "Figures",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UniquelyDisplayedFullName",
                table: "Figures",
                maxLength: 152,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Alias",
                table: "Figures");

            migrationBuilder.DropColumn(
                name: "FigureType",
                table: "Figures");

            migrationBuilder.DropColumn(
                name: "IsLastNameFirst",
                table: "Figures");

            migrationBuilder.DropColumn(
                name: "MiddleName",
                table: "Figures");

            migrationBuilder.DropColumn(
                name: "UniquelyDisplayedFullName",
                table: "Figures");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Figures",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 25,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "Figures",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "Figures",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Figures",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<string>(
                name: "Aka",
                table: "Figures",
                nullable: true);
        }
    }
}
