using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BackgroundJobs.Migrations
{
    public partial class Model_Nodes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ModelFlow",
                columns: table => new
                {
                    Id_Model = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModelFlow", x => x.Id_Model);
                });

            migrationBuilder.CreateTable(
                name: "NodeFlow",
                columns: table => new
                {
                    Id_Node = table.Column<Guid>(nullable: false),
                    Id_Parent = table.Column<Guid>(nullable: true),
                    Id_Model = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Operation = table.Column<int>(type: "int", nullable: false),
                    Input = table.Column<long>(nullable: false),
                    Output = table.Column<long>(nullable: true),
                    ModelId1 = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NodeFlow", x => x.Id_Node);
                    table.ForeignKey(
                        name: "FK_NodeFlow_ModelFlow_Id_Model",
                        column: x => x.Id_Model,
                        principalTable: "ModelFlow",
                        principalColumn: "Id_Model",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_NodeFlow_ModelFlow_ModelId1",
                        column: x => x.ModelId1,
                        principalTable: "ModelFlow",
                        principalColumn: "Id_Model",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_NodeFlow_NodeFlow_Id_Parent",
                        column: x => x.Id_Parent,
                        principalTable: "NodeFlow",
                        principalColumn: "Id_Node",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NodeFlow_Id_Model",
                table: "NodeFlow",
                column: "Id_Model");

            migrationBuilder.CreateIndex(
                name: "IX_NodeFlow_ModelId1",
                table: "NodeFlow",
                column: "ModelId1",
                unique: true,
                filter: "[ModelId1] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_NodeFlow_Id_Parent",
                table: "NodeFlow",
                column: "Id_Parent");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NodeFlow");

            migrationBuilder.DropTable(
                name: "ModelFlow");
        }
    }
}
