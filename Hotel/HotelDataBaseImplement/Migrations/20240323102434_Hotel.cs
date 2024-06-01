using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelDataBaseImplement.Migrations
{
    /// <inheritdoc />
    public partial class Hotel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Headwaiters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HeadwaiterSurname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HeadwaiterName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HeadwaiterPatronymic = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HeadwaiterLogin = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HeadwaiterPassword = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HeadwaiterEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HeadwaiterPhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Headwaiters", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Organisers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrganiserSurname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OrganiserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OrganiserPatronymic = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OrganiserLogin = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OrganiserPassword = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OrganiserEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OrganiserPhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Organisers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Lunches",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HeadwaiterId = table.Column<int>(type: "int", nullable: false),
                    LunchName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LunchPrice = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lunches", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Lunches_Headwaiters_HeadwaiterId",
                        column: x => x.HeadwaiterId,
                        principalTable: "Headwaiters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Conferences",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ConferenceName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OrganiserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Conferences", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Conferences_Organisers_OrganiserId",
                        column: x => x.OrganiserId,
                        principalTable: "Organisers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MealPlans",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MealPlanName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MealPlanPrice = table.Column<double>(type: "float", nullable: false),
                    OrganiserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MealPlans", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MealPlans_Organisers_OrganiserId",
                        column: x => x.OrganiserId,
                        principalTable: "Organisers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Members",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MemberSurname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MemberName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MemberPatronymic = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MemberPhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OrganiserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Members", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Members_Organisers_OrganiserId",
                        column: x => x.OrganiserId,
                        principalTable: "Organisers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ConferenceBookings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ConferenceId = table.Column<int>(type: "int", nullable: false),
                    HeadwaiterId = table.Column<int>(type: "int", nullable: false),
                    NameHall = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BookingDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConferenceBookings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ConferenceBookings_Conferences_ConferenceId",
                        column: x => x.ConferenceId,
                        principalTable: "Conferences",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ConferenceBookings_Headwaiters_HeadwaiterId",
                        column: x => x.HeadwaiterId,
                        principalTable: "Headwaiters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Rooms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoomName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RoomFrame = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RoomPrice = table.Column<double>(type: "float", nullable: false),
                    MealPlanId = table.Column<int>(type: "int", nullable: false),
                    HeadwaiterId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rooms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rooms_Headwaiters_HeadwaiterId",
                        column: x => x.HeadwaiterId,
                        principalTable: "Headwaiters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Rooms_MealPlans_MealPlanId",
                        column: x => x.MealPlanId,
                        principalTable: "MealPlans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ConferenceMembers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MemberId = table.Column<int>(type: "int", nullable: false),
                    ConferenceId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConferenceMembers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ConferenceMembers_Conferences_ConferenceId",
                        column: x => x.ConferenceId,
                        principalTable: "Conferences",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ConferenceMembers_Members_MemberId",
                        column: x => x.MemberId,
                        principalTable: "Members",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MealPlanMembers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MemberId = table.Column<int>(type: "int", nullable: false),
                    MealPlanId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MealPlanMembers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MealPlanMembers_MealPlans_MealPlanId",
                        column: x => x.MealPlanId,
                        principalTable: "MealPlans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MealPlanMembers_Members_MemberId",
                        column: x => x.MemberId,
                        principalTable: "Members",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ConferenceBookingLunches",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ConferenceBookingId = table.Column<int>(type: "int", nullable: false),
                    LunchId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConferenceBookingLunches", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ConferenceBookingLunches_ConferenceBookings_ConferenceBookingId",
                        column: x => x.ConferenceBookingId,
                        principalTable: "ConferenceBookings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ConferenceBookingLunches_Lunches_LunchId",
                        column: x => x.LunchId,
                        principalTable: "Lunches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RoomLunches",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoomId = table.Column<int>(type: "int", nullable: false),
                    LunchId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomLunches", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoomLunches_Lunches_LunchId",
                        column: x => x.LunchId,
                        principalTable: "Lunches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RoomLunches_Rooms_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Rooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ConferenceBookingLunches_ConferenceBookingId",
                table: "ConferenceBookingLunches",
                column: "ConferenceBookingId");

            migrationBuilder.CreateIndex(
                name: "IX_ConferenceBookingLunches_LunchId",
                table: "ConferenceBookingLunches",
                column: "LunchId");

            migrationBuilder.CreateIndex(
                name: "IX_ConferenceBookings_ConferenceId",
                table: "ConferenceBookings",
                column: "ConferenceId");

            migrationBuilder.CreateIndex(
                name: "IX_ConferenceBookings_HeadwaiterId",
                table: "ConferenceBookings",
                column: "HeadwaiterId");

            migrationBuilder.CreateIndex(
                name: "IX_ConferenceMembers_ConferenceId",
                table: "ConferenceMembers",
                column: "ConferenceId");

            migrationBuilder.CreateIndex(
                name: "IX_ConferenceMembers_MemberId",
                table: "ConferenceMembers",
                column: "MemberId");

            migrationBuilder.CreateIndex(
                name: "IX_Conferences_OrganiserId",
                table: "Conferences",
                column: "OrganiserId");

            migrationBuilder.CreateIndex(
                name: "IX_Lunches_HeadwaiterId",
                table: "Lunches",
                column: "HeadwaiterId");

            migrationBuilder.CreateIndex(
                name: "IX_MealPlanMembers_MealPlanId",
                table: "MealPlanMembers",
                column: "MealPlanId");

            migrationBuilder.CreateIndex(
                name: "IX_MealPlanMembers_MemberId",
                table: "MealPlanMembers",
                column: "MemberId");

            migrationBuilder.CreateIndex(
                name: "IX_MealPlans_OrganiserId",
                table: "MealPlans",
                column: "OrganiserId");

            migrationBuilder.CreateIndex(
                name: "IX_Members_OrganiserId",
                table: "Members",
                column: "OrganiserId");

            migrationBuilder.CreateIndex(
                name: "IX_RoomLunches_LunchId",
                table: "RoomLunches",
                column: "LunchId");

            migrationBuilder.CreateIndex(
                name: "IX_RoomLunches_RoomId",
                table: "RoomLunches",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_HeadwaiterId",
                table: "Rooms",
                column: "HeadwaiterId");

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_MealPlanId",
                table: "Rooms",
                column: "MealPlanId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ConferenceBookingLunches");

            migrationBuilder.DropTable(
                name: "ConferenceMembers");

            migrationBuilder.DropTable(
                name: "MealPlanMembers");

            migrationBuilder.DropTable(
                name: "RoomLunches");

            migrationBuilder.DropTable(
                name: "ConferenceBookings");

            migrationBuilder.DropTable(
                name: "Members");

            migrationBuilder.DropTable(
                name: "Lunches");

            migrationBuilder.DropTable(
                name: "Rooms");

            migrationBuilder.DropTable(
                name: "Conferences");

            migrationBuilder.DropTable(
                name: "Headwaiters");

            migrationBuilder.DropTable(
                name: "MealPlans");

            migrationBuilder.DropTable(
                name: "Organisers");
        }
    }
}
