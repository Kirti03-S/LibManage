using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibManage.Migrations
{
    /// <inheritdoc />
    public partial class MembersAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LendingRecords_Member_MemberId",
                table: "LendingRecords");

            migrationBuilder.DropForeignKey(
                name: "FK_Member_MembershipPlan_MembershipPlanId",
                table: "Member");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Member_MemberId",
                table: "Reviews");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Member",
                table: "Member");

            migrationBuilder.RenameTable(
                name: "Member",
                newName: "Members");

            migrationBuilder.RenameIndex(
                name: "IX_Member_MembershipPlanId",
                table: "Members",
                newName: "IX_Members_MembershipPlanId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Members",
                table: "Members",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_LendingRecords_Members_MemberId",
                table: "LendingRecords",
                column: "MemberId",
                principalTable: "Members",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Members_MembershipPlan_MembershipPlanId",
                table: "Members",
                column: "MembershipPlanId",
                principalTable: "MembershipPlan",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Members_MemberId",
                table: "Reviews",
                column: "MemberId",
                principalTable: "Members",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LendingRecords_Members_MemberId",
                table: "LendingRecords");

            migrationBuilder.DropForeignKey(
                name: "FK_Members_MembershipPlan_MembershipPlanId",
                table: "Members");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Members_MemberId",
                table: "Reviews");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Members",
                table: "Members");

            migrationBuilder.RenameTable(
                name: "Members",
                newName: "Member");

            migrationBuilder.RenameIndex(
                name: "IX_Members_MembershipPlanId",
                table: "Member",
                newName: "IX_Member_MembershipPlanId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Member",
                table: "Member",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_LendingRecords_Member_MemberId",
                table: "LendingRecords",
                column: "MemberId",
                principalTable: "Member",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Member_MembershipPlan_MembershipPlanId",
                table: "Member",
                column: "MembershipPlanId",
                principalTable: "MembershipPlan",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Member_MemberId",
                table: "Reviews",
                column: "MemberId",
                principalTable: "Member",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
