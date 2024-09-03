using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IdentityApi.Migrations
{
    /// <inheritdoc />
    public partial class AddBaseEntityFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Stores",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Logo = table.Column<string>(type: "text", nullable: false),
                    Code = table.Column<int>(type: "integer", nullable: false),
                    AlternateCode = table.Column<int>(type: "integer", nullable: false),
                    Type = table.Column<string>(type: "text", nullable: false),
                    Url = table.Column<string>(type: "text", nullable: false),
                    GSTIN = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    EmailID = table.Column<string>(type: "text", nullable: false),
                    Address1 = table.Column<string>(type: "text", nullable: false),
                    Address2 = table.Column<string>(type: "text", nullable: false),
                    Latitude = table.Column<int>(type: "integer", nullable: false),
                    Longitude = table.Column<int>(type: "integer", nullable: false),
                    Country = table.Column<string>(type: "text", nullable: false),
                    State = table.Column<string>(type: "text", nullable: false),
                    City = table.Column<string>(type: "text", nullable: false),
                    Zone = table.Column<string>(type: "text", nullable: false),
                    Region = table.Column<string>(type: "text", nullable: false),
                    Pincode = table.Column<int>(type: "integer", nullable: false),
                    Timing = table.Column<int>(type: "integer", nullable: false),
                    CIN = table.Column<string>(type: "character varying(21)", maxLength: 21, nullable: false),
                    PAN = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stores", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Stores");
        }
    }
}
