using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Data.Migrations
{
    public partial class ReviewAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AdditionalInformation",
                table: "Items",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PictureUrl",
                table: "Items",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TechnicalDescription",
                table: "Items",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ReviewerName = table.Column<string>(nullable: true),
                    ReviewerEmail = table.Column<string>(nullable: true),
                    ReviewerPhoto = table.Column<string>(nullable: true),
                    ReviewMessage = table.Column<string>(nullable: true),
                    ReviewDate = table.Column<long>(nullable: false),
                    rate = table.Column<int>(nullable: false),
                    ItemId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reviews_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_ItemId",
                table: "Reviews",
                column: "ItemId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropColumn(
                name: "AdditionalInformation",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "PictureUrl",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "TechnicalDescription",
                table: "Items");
        }
    }
}
