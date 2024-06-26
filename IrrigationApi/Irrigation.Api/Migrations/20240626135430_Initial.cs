using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Irrigation.Api.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_area",
                columns: table => new
                {
                    area_id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    description = table.Column<string>(type: "TEXT", nullable: true),
                    location = table.Column<string>(type: "TEXT", nullable: true),
                    area_size = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_area", x => x.area_id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_role",
                columns: table => new
                {
                    role_id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    name = table.Column<string>(type: "VARCHAR", maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_role", x => x.role_id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_schedule",
                columns: table => new
                {
                    schedule_id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    start_time = table.Column<DateTime>(type: "TEXT", nullable: false),
                    end_time = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_schedule", x => x.schedule_id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_user",
                columns: table => new
                {
                    user_id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    name = table.Column<string>(type: "NVARCHAR", maxLength: 100, nullable: false),
                    email = table.Column<string>(type: "NVARCHAR", maxLength: 50, nullable: false),
                    password_hash = table.Column<string>(type: "NVARCHAR", maxLength: 255, nullable: false),
                    active = table.Column<bool>(type: "BIT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_user", x => x.user_id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_sensor",
                columns: table => new
                {
                    sensor_id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    type = table.Column<string>(type: "TEXT", nullable: true),
                    location = table.Column<string>(type: "TEXT", nullable: true),
                    area_id = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_sensor", x => x.sensor_id);
                    table.ForeignKey(
                        name: "FK_Area_Sensor",
                        column: x => x.area_id,
                        principalTable: "tbl_area",
                        principalColumn: "area_id");
                });

            migrationBuilder.CreateTable(
                name: "tbl_user_role",
                columns: table => new
                {
                    role_id = table.Column<int>(type: "INTEGER", nullable: false),
                    user_id = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_user_role", x => new { x.role_id, x.user_id });
                    table.ForeignKey(
                        name: "FK_UserRole_RoleId",
                        column: x => x.role_id,
                        principalTable: "tbl_role",
                        principalColumn: "role_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRole_UserId",
                        column: x => x.user_id,
                        principalTable: "tbl_user",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_notification",
                columns: table => new
                {
                    notification_id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    description = table.Column<string>(type: "TEXT", nullable: true),
                    timestamp = table.Column<DateTime>(type: "TEXT", nullable: false),
                    sensor_id = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_notification", x => x.notification_id);
                    table.ForeignKey(
                        name: "FK_Notification_Sensor",
                        column: x => x.sensor_id,
                        principalTable: "tbl_sensor",
                        principalColumn: "sensor_id");
                });

            migrationBuilder.CreateTable(
                name: "tbl_weather",
                columns: table => new
                {
                    weather_id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    timestamp = table.Column<DateTime>(type: "TEXT", nullable: false),
                    temperature = table.Column<int>(type: "INTEGER", nullable: false),
                    humidity = table.Column<int>(type: "INTEGER", nullable: false),
                    description = table.Column<string>(type: "TEXT", nullable: true),
                    sensor_id = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_weather", x => x.weather_id);
                    table.ForeignKey(
                        name: "FK_Weather_Sensor",
                        column: x => x.sensor_id,
                        principalTable: "tbl_sensor",
                        principalColumn: "sensor_id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbl_notification_sensor_id",
                table: "tbl_notification",
                column: "sensor_id");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_sensor_area_id",
                table: "tbl_sensor",
                column: "area_id");

            migrationBuilder.CreateIndex(
                name: "IX_User_Email",
                table: "tbl_user",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tbl_user_role_user_id",
                table: "tbl_user_role",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_weather_sensor_id",
                table: "tbl_weather",
                column: "sensor_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_notification");

            migrationBuilder.DropTable(
                name: "tbl_schedule");

            migrationBuilder.DropTable(
                name: "tbl_user_role");

            migrationBuilder.DropTable(
                name: "tbl_weather");

            migrationBuilder.DropTable(
                name: "tbl_role");

            migrationBuilder.DropTable(
                name: "tbl_user");

            migrationBuilder.DropTable(
                name: "tbl_sensor");

            migrationBuilder.DropTable(
                name: "tbl_area");
        }
    }
}
