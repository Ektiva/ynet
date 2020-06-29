using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Data.Migrations
{
    public partial class OrderEntity1Added : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Orders1",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    BuyerEmail = table.Column<string>(nullable: true),
                    OrderDate = table.Column<long>(nullable: false),
                    ShipToAddress_FirstName = table.Column<string>(nullable: true),
                    ShipToAddress_LastName = table.Column<string>(nullable: true),
                    ShipToAddress_MiddleName = table.Column<string>(nullable: true),
                    ShipToAddress_Company = table.Column<string>(nullable: true),
                    ShipToAddress_Country = table.Column<string>(nullable: true),
                    ShipToAddress_Email = table.Column<string>(nullable: true),
                    ShipToAddress_Phone = table.Column<string>(nullable: true),
                    ShipToAddress_Street = table.Column<string>(nullable: true),
                    ShipToAddress_City = table.Column<string>(nullable: true),
                    ShipToAddress_State = table.Column<string>(nullable: true),
                    ShipToAddress_Zipcode = table.Column<string>(nullable: true),
                    DeliveryMethodId = table.Column<int>(nullable: true),
                    Subtotal = table.Column<double>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    PaymentIntentId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders1", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders1_DeliveryMethods_DeliveryMethodId",
                        column: x => x.DeliveryMethodId,
                        principalTable: "DeliveryMethods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OrderItems1",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ItemOrdered_ProductItemId = table.Column<int>(nullable: true),
                    ItemOrdered_ProductName = table.Column<string>(nullable: true),
                    ItemOrdered_PictureUrl = table.Column<string>(nullable: true),
                    Price = table.Column<double>(nullable: false),
                    Quantity = table.Column<int>(nullable: false),
                    Order1Id = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItems1", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderItems1_Orders1_Order1Id",
                        column: x => x.Order1Id,
                        principalTable: "Orders1",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems1_Order1Id",
                table: "OrderItems1",
                column: "Order1Id");

            migrationBuilder.CreateIndex(
                name: "IX_Orders1_DeliveryMethodId",
                table: "Orders1",
                column: "DeliveryMethodId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderItems1");

            migrationBuilder.DropTable(
                name: "Orders1");
        }
    }
}
