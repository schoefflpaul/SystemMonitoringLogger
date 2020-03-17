using Microsoft.EntityFrameworkCore.Migrations;

namespace SystemMonitoringLogger.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cpu",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Baseclock = table.Column<int>(nullable: false),
                    Currentclock = table.Column<int>(nullable: false),
                    Utilisation = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cpu", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ram",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Used = table.Column<double>(nullable: false),
                    Max = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ram", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SystemInfo",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    CpuId = table.Column<int>(nullable: true),
                    RamId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemInfo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SystemInfo_Cpu_CpuId",
                        column: x => x.CpuId,
                        principalTable: "Cpu",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SystemInfo_Ram_RamId",
                        column: x => x.RamId,
                        principalTable: "Ram",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SystemInfo_CpuId",
                table: "SystemInfo",
                column: "CpuId");

            migrationBuilder.CreateIndex(
                name: "IX_SystemInfo_RamId",
                table: "SystemInfo",
                column: "RamId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SystemInfo");

            migrationBuilder.DropTable(
                name: "Cpu");

            migrationBuilder.DropTable(
                name: "Ram");
        }
    }
}
