using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace TrackYourExpensesApp.Data.Migrations
{
    public partial class modifyFixedExpeModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ByDate",
                table: "Expenses",
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "ByDate",
                table: "Expenses",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
