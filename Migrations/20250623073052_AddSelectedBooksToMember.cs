using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibManage.Migrations
{
    /// <inheritdoc />
    public partial class AddSelectedBooksToMember : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BookMember",
                columns: table => new
                {
                    MemberId = table.Column<int>(type: "int", nullable: false),
                    SelectedBooksId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookMember", x => new { x.MemberId, x.SelectedBooksId });
                    table.ForeignKey(
                        name: "FK_BookMember_Books_SelectedBooksId",
                        column: x => x.SelectedBooksId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookMember_Members_MemberId",
                        column: x => x.MemberId,
                        principalTable: "Members",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BookMember_SelectedBooksId",
                table: "BookMember",
                column: "SelectedBooksId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookMember");
        }
    }
}
