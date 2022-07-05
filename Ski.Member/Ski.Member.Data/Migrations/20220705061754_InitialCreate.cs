using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ski.Member.Data.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Member",
                columns: table => new
                {
                    me_sn = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    me_name = table.Column<string>(type: "TEXT", nullable: true),
                    me_id = table.Column<string>(type: "TEXT", nullable: true),
                    me_pw = table.Column<string>(type: "TEXT", nullable: true),
                    me_gender = table.Column<string>(type: "TEXT", nullable: true),
                    me_pid = table.Column<string>(type: "TEXT", nullable: true),
                    me_birth = table.Column<DateTime>(type: "TEXT", nullable: true),
                    me_tel = table.Column<string>(type: "TEXT", nullable: true),
                    me_tel_ext = table.Column<string>(type: "TEXT", nullable: true),
                    me_fax = table.Column<string>(type: "TEXT", nullable: true),
                    me_mobile = table.Column<string>(type: "TEXT", nullable: true),
                    me_add1_post = table.Column<string>(type: "TEXT", nullable: true),
                    me_add1_city = table.Column<string>(type: "TEXT", nullable: true),
                    me_add1_area = table.Column<string>(type: "TEXT", nullable: true),
                    me_add1 = table.Column<string>(type: "TEXT", nullable: true),
                    me_marital = table.Column<string>(type: "TEXT", nullable: true),
                    me_national = table.Column<string>(type: "TEXT", nullable: true),
                    me_email = table.Column<string>(type: "TEXT", nullable: true),
                    me_add2_post = table.Column<string>(type: "TEXT", nullable: true),
                    me_add2_city = table.Column<string>(type: "TEXT", nullable: true),
                    me_add2_area = table.Column<string>(type: "TEXT", nullable: true),
                    me_add2 = table.Column<string>(type: "TEXT", nullable: true),
                    me_receiver = table.Column<string>(type: "TEXT", nullable: true),
                    adddate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    moddate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    otpchk = table.Column<string>(type: "TEXT", nullable: true),
                    authcode = table.Column<string>(type: "TEXT", nullable: true),
                    authflag = table.Column<string>(type: "TEXT", nullable: true),
                    dOTP = table.Column<DateTime>(type: "TEXT", nullable: true),
                    m_RegType = table.Column<string>(type: "TEXT", nullable: true),
                    bChangePw = table.Column<string>(type: "TEXT", nullable: true),
                    iCreate = table.Column<string>(type: "TEXT", nullable: true),
                    RememberCC = table.Column<string>(type: "TEXT", nullable: true),
                    RememberEmail = table.Column<string>(type: "TEXT", nullable: true),
                    iChangeMobile = table.Column<string>(type: "TEXT", nullable: true),
                    LstLoginTime = table.Column<DateTime>(type: "TEXT", nullable: true),
                    appLoginTime = table.Column<DateTime>(type: "TEXT", nullable: true),
                    me_pwE = table.Column<string>(type: "TEXT", nullable: true),
                    me_nameE = table.Column<string>(type: "TEXT", nullable: true),
                    bLock = table.Column<string>(type: "TEXT", nullable: true),
                    ilock = table.Column<int>(type: "INTEGER", nullable: true),
                    sQply = table.Column<string>(type: "TEXT", nullable: true),
                    dQply = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Member", x => x.me_sn);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Member");
        }
    }
}
