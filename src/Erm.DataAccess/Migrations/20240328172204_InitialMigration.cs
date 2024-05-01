﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Erm.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "business_process",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "VARCHAR(50)", nullable: false),
                    domain = table.Column<string>(type: "VARCHAR(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_business_process", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "risk",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    type = table.Column<string>(type: "VARCHAR(10)", nullable: false),
                    description = table.Column<string>(type: "VARCHAR(500)", nullable: false),
                    probability = table.Column<int>(type: "INTEGER", nullable: false),
                    business_impact = table.Column<int>(type: "INTEGER", nullable: false),
                    occurence_data = table.Column<DateTime>(type: "DATE", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_risk", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "risk_profile",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    business_process_id = table.Column<int>(type: "integer", nullable: false),
                    risk_name = table.Column<string>(type: "VARCHAR(50)", nullable: false),
                    description = table.Column<string>(type: "VARCHAR(500)", nullable: false),
                    occurrence_probability = table.Column<int>(type: "INTEGER", nullable: false),
                    potential_business_impact = table.Column<int>(type: "INTEGER", nullable: false),
                    potential_solution = table.Column<string>(type: "VARCHAR(100)", nullable: true),
                    RiskId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_risk_profile", x => x.id);
                    table.ForeignKey(
                        name: "FK_risk_profile_business_process_business_process_id",
                        column: x => x.business_process_id,
                        principalTable: "business_process",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_risk_profile_risk_RiskId",
                        column: x => x.RiskId,
                        principalTable: "risk",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_risk_profile_business_process_id",
                table: "risk_profile",
                column: "business_process_id");

            migrationBuilder.CreateIndex(
                name: "IX_risk_profile_RiskId",
                table: "risk_profile",
                column: "RiskId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "risk_profile");

            migrationBuilder.DropTable(
                name: "business_process");

            migrationBuilder.DropTable(
                name: "risk");
        }
    }
}
