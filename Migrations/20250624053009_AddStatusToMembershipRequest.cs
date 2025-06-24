using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibManage.Migrations
{
    /// <inheritdoc />
    public partial class AddStatusToMembershipRequest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsApproved",
                table: "MembershipRequests");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "MembershipRequests",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "MembershipRequests");

            migrationBuilder.AddColumn<bool>(
                name: "IsApproved",
                table: "MembershipRequests",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
