using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Data.Migrations
{
    public partial class OrderEntityModified : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_Orders1_Address1_ShipToAddressId",
            //    table: "Orders1");

            //migrationBuilder.DropTable(
            //    name: "Address1");

            //migrationBuilder.DropIndex(
            //    name: "IX_Orders1_ShipToAddressId",
            //    table: "Orders1");

            //migrationBuilder.DropColumn(
            //    name: "ShipToAddressId",
            //    table: "Orders1");

            migrationBuilder.AddColumn<string>(
                name: "ShipToAddress_Company",
                table: "Orders",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ShipToAddress_Country",
                table: "Orders",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ShipToAddress_MiddleName",
                table: "Orders",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ShipToAddress_Company",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ShipToAddress_Country",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ShipToAddress_MiddleName",
                table: "Orders");

            //migrationBuilder.AddColumn<string>(
            //    name: "ShipToAddressId",
            //    table: "Orders1",
            //    type: "TEXT",
            //    nullable: true);

            //migrationBuilder.CreateTable(
            //    name: "Address1",
            //    columns: table => new
            //    {
            //        Id = table.Column<string>(type: "TEXT", nullable: false),
            //        City = table.Column<string>(type: "TEXT", nullable: true),
            //        Company = table.Column<string>(type: "TEXT", nullable: true),
            //        Country = table.Column<string>(type: "TEXT", nullable: true),
            //        Email = table.Column<string>(type: "TEXT", nullable: true),
            //        FirstName = table.Column<string>(type: "TEXT", nullable: true),
            //        LastName = table.Column<string>(type: "TEXT", nullable: true),
            //        MiddleName = table.Column<string>(type: "TEXT", nullable: true),
            //        Phone = table.Column<string>(type: "TEXT", nullable: true),
            //        State = table.Column<string>(type: "TEXT", nullable: true),
            //        Street = table.Column<string>(type: "TEXT", nullable: true),
            //        Zipcode = table.Column<string>(type: "TEXT", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Address1", x => x.Id);
            //    });

            //migrationBuilder.CreateIndex(
            //    name: "IX_Orders1_ShipToAddressId",
            //    table: "Orders1",
            //    column: "ShipToAddressId");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_Orders1_Address1_ShipToAddressId",
            //    table: "Orders1",
            //    column: "ShipToAddressId",
            //    principalTable: "Address1",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Restrict);
        }
    }
}
