using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class teamLeadcountry : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_TeamLead_CountryId",
                table: "TeamLead",
                column: "CountryId");

            migrationBuilder.AddForeignKey(
                name: "FK_TeamLead_Country_CountryId",
                table: "TeamLead",
                column: "CountryId",
                principalTable: "Country",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

          
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TeamLead_Country_CountryId",
                table: "TeamLead");

            migrationBuilder.DropIndex(
                name: "IX_TeamLead_CountryId",
                table: "TeamLead");
        }
    }
}
