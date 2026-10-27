using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace para.Migrations;

public partial class LayeredArchitecture : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<int>(name: "StockQuantity", table: "Products", type: "int", nullable: false, defaultValue: 0);
        migrationBuilder.AddColumn<byte[]>(name: "RowVersion", table: "Products", type: "rowversion", rowVersion: true, nullable: false);
        migrationBuilder.CreateTable(
            name: "ProductReservations",
            columns: table => new
            {
                ProductReservationID = table.Column<int>(type: "int", nullable: false).Annotation("SqlServer:Identity", "1, 1"),
                ProductID = table.Column<int>(type: "int", nullable: false),
                UserID = table.Column<int>(type: "int", nullable: false),
                Quantity = table.Column<int>(type: "int", nullable: false),
                CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                CancelledAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_ProductReservations", x => x.ProductReservationID);
                table.CheckConstraint("CK_ProductReservations_Quantity", "[Quantity] > 0");
                table.ForeignKey("FK_ProductReservations_Products_ProductID", x => x.ProductID, "Products", "ProductID", onDelete: ReferentialAction.Restrict);
                table.ForeignKey("FK_ProductReservations_Users_UserID", x => x.UserID, "Users", "UserID", onDelete: ReferentialAction.Restrict);
            });
        migrationBuilder.CreateIndex(name: "IX_ProductReservations_ProductID", table: "ProductReservations", column: "ProductID");
        migrationBuilder.CreateIndex(name: "IX_ProductReservations_UserID", table: "ProductReservations", column: "UserID");
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(name: "ProductReservations");
        migrationBuilder.DropColumn(name: "StockQuantity", table: "Products");
        migrationBuilder.DropColumn(name: "RowVersion", table: "Products");
    }
}
