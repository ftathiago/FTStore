using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FTStore.Web.Migrations.FTStoreAuth
{
    public partial class InitializeAuthDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "user",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Email = table.Column<string>(maxLength: 100, nullable: false),
                    Hash = table.Column<byte[]>(nullable: false),
                    Salt = table.Column<byte[]>(nullable: false),
                    IsAdmin = table.Column<bool>(nullable: false),
                    CustomerId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "user",
                columns: new[] { "Id", "CustomerId", "Email", "Hash", "IsAdmin", "Salt" },
                values: new object[] { 1, null, "admin@admin.com", new byte[] { 41, 190, 66, 31, 72, 247, 149, 193, 69, 195, 76, 229, 53, 20, 73, 248, 173, 157, 239, 150, 79, 132, 178, 176, 218, 176, 188, 72, 118, 223, 46, 104, 229, 87, 114, 152, 208, 174, 243, 151, 167, 27, 100, 58, 79, 195, 203, 1, 2, 25, 251, 104, 41, 182, 196, 126, 213, 68, 177, 199, 74, 3, 189, 61 }, true, new byte[] { 71, 53, 232, 225, 25, 31, 8, 91, 250, 249, 123, 8, 170, 255, 58, 154, 12, 107, 3, 34, 252, 253, 254, 17, 17, 195, 84, 247, 154, 34, 245, 18, 197, 169, 60, 225, 61, 31, 34, 199, 152, 112, 116, 173, 102, 25, 61, 16, 164, 155, 113, 94, 217, 199, 94, 232, 215, 233, 160, 45, 70, 205, 190, 219, 154, 0, 232, 230, 24, 10, 100, 74, 166, 212, 32, 245, 88, 10, 53, 217, 146, 238, 235, 34, 197, 73, 157, 27, 59, 110, 27, 64, 1, 216, 7, 72, 221, 171, 204, 116, 232, 119, 116, 229, 217, 7, 144, 48, 42, 247, 170, 17, 236, 218, 150, 68, 225, 230, 44, 57, 126, 14, 79, 144, 140, 251, 69, 145 } });

            migrationBuilder.CreateIndex(
                name: "IX_user_Email",
                table: "user",
                column: "Email",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "user");
        }
    }
}
