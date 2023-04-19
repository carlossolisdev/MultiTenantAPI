using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MultiTenantAPI.Persistance.Tenant.Migrations
{
    public partial class Add_Table_StoresInfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StoreInfo",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Identifier = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConnectionString = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoreInfo", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StoreInfo_Identifier",
                table: "StoreInfo",
                column: "Identifier",
                unique: true,
                filter: "[Identifier] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StoreInfo");
        }
    }
}
