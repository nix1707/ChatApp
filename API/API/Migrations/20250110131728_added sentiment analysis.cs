using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class addedsentimentanalysis : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Analysis_Score",
                table: "Messages",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Analysis_Sentiment",
                table: "Messages",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Analysis_Score",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "Analysis_Sentiment",
                table: "Messages");
        }
    }
}
