using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace fakeLook_dal.Migrations
{
    public partial class Migration2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Date", "X_Position", "Y_Position", "Z_Position" },
                values: new object[] { new DateTime(2022, 3, 14, 20, 21, 50, 179, DateTimeKind.Local).AddTicks(1076), 44.297386426473267, 30.826800297074993, 23.983233308427575 });

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Date", "X_Position", "Y_Position", "Z_Position" },
                values: new object[] { new DateTime(2022, 3, 14, 20, 21, 50, 179, DateTimeKind.Local).AddTicks(1247), 43.869408513238561, 41.181428286321719, 32.098816385038646 });

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Date", "X_Position", "Y_Position", "Z_Position" },
                values: new object[] { new DateTime(2022, 3, 14, 20, 21, 50, 179, DateTimeKind.Local).AddTicks(1250), 33.674769223610028, 23.438622825981703, 42.212867561212278 });

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Date", "X_Position", "Y_Position", "Z_Position" },
                values: new object[] { new DateTime(2022, 3, 14, 20, 21, 50, 179, DateTimeKind.Local).AddTicks(1252), 10.051693576019611, 49.668069819637289, 29.710728513442746 });

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Date", "X_Position", "Y_Position", "Z_Position" },
                values: new object[] { new DateTime(2022, 3, 14, 20, 21, 50, 179, DateTimeKind.Local).AddTicks(1254), 26.863160729928044, 24.317390624914463, 25.721390320699673 });

            migrationBuilder.UpdateData(
                table: "UserTaggedComments",
                keyColumn: "Id",
                keyValue: 1,
                column: "UserId",
                value: 4);

            migrationBuilder.UpdateData(
                table: "UserTaggedComments",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CommentId", "UserId" },
                values: new object[] { 7, 5 });

            migrationBuilder.UpdateData(
                table: "UserTaggedComments",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CommentId", "UserId" },
                values: new object[] { 11, 4 });

            migrationBuilder.UpdateData(
                table: "UserTaggedComments",
                keyColumn: "Id",
                keyValue: 4,
                column: "CommentId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "UserTaggedComments",
                keyColumn: "Id",
                keyValue: 5,
                column: "CommentId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "UserTaggedComments",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CommentId", "UserId" },
                values: new object[] { 1, 1 });

            migrationBuilder.UpdateData(
                table: "UserTaggedComments",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CommentId", "UserId" },
                values: new object[] { 11, 3 });

            migrationBuilder.UpdateData(
                table: "UserTaggedComments",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CommentId", "UserId" },
                values: new object[] { 22, 4 });

            migrationBuilder.UpdateData(
                table: "UserTaggedComments",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CommentId", "UserId" },
                values: new object[] { 22, 2 });

            migrationBuilder.UpdateData(
                table: "UserTaggedComments",
                keyColumn: "Id",
                keyValue: 10,
                column: "CommentId",
                value: 20);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "Password",
                value: "452710133");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "Password",
                value: "452710133");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                column: "Password",
                value: "452710133");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4,
                column: "Password",
                value: "452710133");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 5,
                column: "Password",
                value: "452710133");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserName",
                table: "Users");

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Date", "X_Position", "Y_Position", "Z_Position" },
                values: new object[] { new DateTime(2022, 3, 14, 11, 16, 25, 574, DateTimeKind.Local).AddTicks(8925), 35.894483619845161, 1.4793927861086742, 1.2152104013205323 });

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Date", "X_Position", "Y_Position", "Z_Position" },
                values: new object[] { new DateTime(2022, 3, 14, 11, 16, 25, 574, DateTimeKind.Local).AddTicks(8972), 30.41734441016246, 19.932767388819666, 17.807658911658031 });

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Date", "X_Position", "Y_Position", "Z_Position" },
                values: new object[] { new DateTime(2022, 3, 14, 11, 16, 25, 574, DateTimeKind.Local).AddTicks(8976), 44.859550246668739, 14.642026576833016, 1.2797874173405666 });

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Date", "X_Position", "Y_Position", "Z_Position" },
                values: new object[] { new DateTime(2022, 3, 14, 11, 16, 25, 574, DateTimeKind.Local).AddTicks(8978), 18.404362015812204, 17.687425762989502, 28.902186250685798 });

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Date", "X_Position", "Y_Position", "Z_Position" },
                values: new object[] { new DateTime(2022, 3, 14, 11, 16, 25, 574, DateTimeKind.Local).AddTicks(8981), 16.187076251426223, 20.621380521350137, 45.67429424575127 });

            migrationBuilder.UpdateData(
                table: "UserTaggedComments",
                keyColumn: "Id",
                keyValue: 1,
                column: "UserId",
                value: 5);

            migrationBuilder.UpdateData(
                table: "UserTaggedComments",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CommentId", "UserId" },
                values: new object[] { 6, 4 });

            migrationBuilder.UpdateData(
                table: "UserTaggedComments",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CommentId", "UserId" },
                values: new object[] { 13, 3 });

            migrationBuilder.UpdateData(
                table: "UserTaggedComments",
                keyColumn: "Id",
                keyValue: 4,
                column: "CommentId",
                value: 18);

            migrationBuilder.UpdateData(
                table: "UserTaggedComments",
                keyColumn: "Id",
                keyValue: 5,
                column: "CommentId",
                value: 8);

            migrationBuilder.UpdateData(
                table: "UserTaggedComments",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CommentId", "UserId" },
                values: new object[] { 9, 3 });

            migrationBuilder.UpdateData(
                table: "UserTaggedComments",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CommentId", "UserId" },
                values: new object[] { 15, 2 });

            migrationBuilder.UpdateData(
                table: "UserTaggedComments",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CommentId", "UserId" },
                values: new object[] { 15, 1 });

            migrationBuilder.UpdateData(
                table: "UserTaggedComments",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CommentId", "UserId" },
                values: new object[] { 7, 4 });

            migrationBuilder.UpdateData(
                table: "UserTaggedComments",
                keyColumn: "Id",
                keyValue: 10,
                column: "CommentId",
                value: 3);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "Password",
                value: "-81759896");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "Password",
                value: "-81759896");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                column: "Password",
                value: "-81759896");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4,
                column: "Password",
                value: "-81759896");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 5,
                column: "Password",
                value: "-81759896");
        }
    }
}
