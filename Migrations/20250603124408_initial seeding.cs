using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LibManage.Migrations
{
    /// <inheritdoc />
    public partial class initialseeding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LendingRecords_Members_MemberId",
                table: "LendingRecords");

            migrationBuilder.DropForeignKey(
                name: "FK_Members_MembershipPlans_MembershipPlanId",
                table: "Members");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Members_MemberId",
                table: "Reviews");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MembershipPlans",
                table: "MembershipPlans");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Members",
                table: "Members");

            migrationBuilder.RenameTable(
                name: "MembershipPlans",
                newName: "MembershipPlan");

            migrationBuilder.RenameTable(
                name: "Members",
                newName: "Member");

            migrationBuilder.RenameIndex(
                name: "IX_Members_MembershipPlanId",
                table: "Member",
                newName: "IX_Member_MembershipPlanId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MembershipPlan",
                table: "MembershipPlan",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Member",
                table: "Member",
                column: "Id");

            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "Id", "Biography", "FirstName", "LastName" },
                values: new object[,]
                {
                    { 1, "", "Isaac", "Asimov" },
                    { 2, "", "J.K.", "Rowling" }
                });

            migrationBuilder.InsertData(
                table: "Genres",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Science Fiction" },
                    { 2, "Fantasy" }
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "AuthorId", "GenreId", "ISBN", "PublishedDate", "Stock", "Title" },
                values: new object[,]
                {
                    { 1, 1, 1, "978-0-553-80371-0", new DateTime(1951, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, "Foundation" },
                    { 2, 2, 2, "978-0-7475-3269-9", new DateTime(1997, 6, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), 10, "Harry Potter and the Sorcerer's Stone" }
                });

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                name: "PK_MembershipPlan",
                table: "MembershipPlan");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Member",
                table: "Member");

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.RenameTable(
                name: "MembershipPlan",
                newName: "MembershipPlans");

            migrationBuilder.RenameTable(
                name: "Member",
                newName: "Members");

            migrationBuilder.RenameIndex(
                name: "IX_Member_MembershipPlanId",
                table: "Members",
                newName: "IX_Members_MembershipPlanId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MembershipPlans",
                table: "MembershipPlans",
                column: "Id");

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
                name: "FK_Members_MembershipPlans_MembershipPlanId",
                table: "Members",
                column: "MembershipPlanId",
                principalTable: "MembershipPlans",
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
    }
}
