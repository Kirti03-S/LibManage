using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibManage.Migrations
{
    /// <inheritdoc />
    public partial class AddMembershipPlanToDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Members_MembershipPlan_MembershipPlanId",
                table: "Members");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MembershipPlan",
                table: "MembershipPlan");

            migrationBuilder.RenameTable(
                name: "MembershipPlan",
                newName: "MembershipPlans");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MembershipPlans",
                table: "MembershipPlans",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Members_MembershipPlans_MembershipPlanId",
                table: "Members",
                column: "MembershipPlanId",
                principalTable: "MembershipPlans",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Members_MembershipPlans_MembershipPlanId",
                table: "Members");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MembershipPlans",
                table: "MembershipPlans");

            migrationBuilder.RenameTable(
                name: "MembershipPlans",
                newName: "MembershipPlan");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MembershipPlan",
                table: "MembershipPlan",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Members_MembershipPlan_MembershipPlanId",
                table: "Members",
                column: "MembershipPlanId",
                principalTable: "MembershipPlan",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
