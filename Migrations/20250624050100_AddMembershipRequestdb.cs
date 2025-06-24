using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibManage.Migrations
{
    /// <inheritdoc />
    public partial class AddMembershipRequestdb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MembershipRequestId",
                table: "Books",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "MembershipRequests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MembershipPlanId = table.Column<int>(type: "int", nullable: false),
                    RequestedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsApproved = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MembershipRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MembershipRequests_MembershipPlans_MembershipPlanId",
                        column: x => x.MembershipPlanId,
                        principalTable: "MembershipPlans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 1,
                column: "MembershipRequestId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 2,
                column: "MembershipRequestId",
                value: null);

            migrationBuilder.CreateIndex(
                name: "IX_Books_MembershipRequestId",
                table: "Books",
                column: "MembershipRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_MembershipRequests_MembershipPlanId",
                table: "MembershipRequests",
                column: "MembershipPlanId");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_MembershipRequests_MembershipRequestId",
                table: "Books",
                column: "MembershipRequestId",
                principalTable: "MembershipRequests",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_MembershipRequests_MembershipRequestId",
                table: "Books");

            migrationBuilder.DropTable(
                name: "MembershipRequests");

            migrationBuilder.DropIndex(
                name: "IX_Books_MembershipRequestId",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "MembershipRequestId",
                table: "Books");
        }
    }
}
