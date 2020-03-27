using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FTStore.Infra.Migrations
{
    public partial class InitializeDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "customer",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    Surname = table.Column<string>(maxLength: 250, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_customer", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "paymentmethod",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Description = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_paymentmethod", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "product",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 200, nullable: false),
                    Details = table.Column<string>(maxLength: 1000, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(19,4)", nullable: false),
                    ImageFileName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_product", x => x.Id);
                });

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
                    table.ForeignKey(
                        name: "FK_user_customer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "customer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "order",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    OrderDate = table.Column<DateTime>(nullable: false),
                    CustomerId = table.Column<int>(nullable: false),
                    DeliveryForecast = table.Column<DateTime>(nullable: false),
                    Street = table.Column<string>(maxLength: 200, nullable: false),
                    AddressNumber = table.Column<string>(maxLength: 4, nullable: false),
                    Neighborhood = table.Column<string>(maxLength: 100, nullable: false),
                    City = table.Column<string>(maxLength: 100, nullable: false),
                    State = table.Column<string>(maxLength: 100, nullable: false),
                    ZIPCode = table.Column<string>(maxLength: 10, nullable: false),
                    PaymentMethodId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_order", x => x.Id);
                    table.ForeignKey(
                        name: "FK_order_customer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "customer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_order_paymentmethod_PaymentMethodId",
                        column: x => x.PaymentMethodId,
                        principalTable: "paymentmethod",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "orderitem",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ProductId = table.Column<int>(nullable: false),
                    Title = table.Column<string>(maxLength: 200, nullable: true),
                    Price = table.Column<decimal>(nullable: false),
                    Quantity = table.Column<decimal>(nullable: false),
                    Discount = table.Column<decimal>(nullable: false),
                    OrderModelId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_orderitem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_orderitem_order_OrderModelId",
                        column: x => x.OrderModelId,
                        principalTable: "order",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "paymentmethod",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "Forma de pagamento Boleto", "Boleto" },
                    { 2, "Forma de pagamento Cartão de Crédito", "Cartão de Crédito" },
                    { 3, "Forma de pagamento Depósito", "Depósito" }
                });

            migrationBuilder.InsertData(
                table: "user",
                columns: new[] { "Id", "CustomerId", "Email", "Hash", "IsAdmin", "Salt" },
                values: new object[] { 1, null, "admin@admin.com", new byte[] { 97, 115, 100, 102 }, true, new byte[] { 97, 115, 100, 102 } });

            migrationBuilder.CreateIndex(
                name: "IX_order_CustomerId",
                table: "order",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_order_PaymentMethodId",
                table: "order",
                column: "PaymentMethodId");

            migrationBuilder.CreateIndex(
                name: "IX_orderitem_OrderModelId",
                table: "orderitem",
                column: "OrderModelId");

            migrationBuilder.CreateIndex(
                name: "IX_user_CustomerId",
                table: "user",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_user_Email",
                table: "user",
                column: "Email",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "orderitem");

            migrationBuilder.DropTable(
                name: "product");

            migrationBuilder.DropTable(
                name: "user");

            migrationBuilder.DropTable(
                name: "order");

            migrationBuilder.DropTable(
                name: "customer");

            migrationBuilder.DropTable(
                name: "paymentmethod");
        }
    }
}
