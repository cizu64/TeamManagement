using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class teamMembercountry : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_TeamMember_CountryId",
                table: "TeamMember",
                column: "CountryId");

            migrationBuilder.AddForeignKey(
                name: "FK_TeamMember_Country_CountryId",
                table: "TeamMember",
                column: "CountryId",
                principalTable: "Country",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TeamMember_Country_CountryId",
                table: "TeamMember");

            migrationBuilder.DropIndex(
                name: "IX_TeamMember_CountryId",
                table: "TeamMember");
        }
    }
}
