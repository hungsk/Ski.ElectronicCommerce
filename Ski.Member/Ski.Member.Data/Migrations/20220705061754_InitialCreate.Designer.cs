﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Ski.Member.Data;

#nullable disable

namespace Ski.Member.Data.Migrations
{
    [DbContext(typeof(ShinkongDbContext))]
    [Migration("20220705061754_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.6");

            modelBuilder.Entity("Ski.Member.Domain.Entities.MemberModel", b =>
                {
                    b.Property<int>("me_sn")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("LstLoginTime")
                        .HasColumnType("TEXT");

                    b.Property<string>("RememberCC")
                        .HasColumnType("TEXT");

                    b.Property<string>("RememberEmail")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("adddate")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("appLoginTime")
                        .HasColumnType("TEXT");

                    b.Property<string>("authcode")
                        .HasColumnType("TEXT");

                    b.Property<string>("authflag")
                        .HasColumnType("TEXT");

                    b.Property<string>("bChangePw")
                        .HasColumnType("TEXT");

                    b.Property<string>("bLock")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("dOTP")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("dQply")
                        .HasColumnType("TEXT");

                    b.Property<string>("iChangeMobile")
                        .HasColumnType("TEXT");

                    b.Property<string>("iCreate")
                        .HasColumnType("TEXT");

                    b.Property<int?>("ilock")
                        .HasColumnType("INTEGER");

                    b.Property<string>("m_RegType")
                        .HasColumnType("TEXT");

                    b.Property<string>("me_add1")
                        .HasColumnType("TEXT");

                    b.Property<string>("me_add1_area")
                        .HasColumnType("TEXT");

                    b.Property<string>("me_add1_city")
                        .HasColumnType("TEXT");

                    b.Property<string>("me_add1_post")
                        .HasColumnType("TEXT");

                    b.Property<string>("me_add2")
                        .HasColumnType("TEXT");

                    b.Property<string>("me_add2_area")
                        .HasColumnType("TEXT");

                    b.Property<string>("me_add2_city")
                        .HasColumnType("TEXT");

                    b.Property<string>("me_add2_post")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("me_birth")
                        .HasColumnType("TEXT");

                    b.Property<string>("me_email")
                        .HasColumnType("TEXT");

                    b.Property<string>("me_fax")
                        .HasColumnType("TEXT");

                    b.Property<string>("me_gender")
                        .HasColumnType("TEXT");

                    b.Property<string>("me_id")
                        .HasColumnType("TEXT");

                    b.Property<string>("me_marital")
                        .HasColumnType("TEXT");

                    b.Property<string>("me_mobile")
                        .HasColumnType("TEXT");

                    b.Property<string>("me_name")
                        .HasColumnType("TEXT");

                    b.Property<string>("me_nameE")
                        .HasColumnType("TEXT");

                    b.Property<string>("me_national")
                        .HasColumnType("TEXT");

                    b.Property<string>("me_pid")
                        .HasColumnType("TEXT");

                    b.Property<string>("me_pw")
                        .HasColumnType("TEXT");

                    b.Property<string>("me_pwE")
                        .HasColumnType("TEXT");

                    b.Property<string>("me_receiver")
                        .HasColumnType("TEXT");

                    b.Property<string>("me_tel")
                        .HasColumnType("TEXT");

                    b.Property<string>("me_tel_ext")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("moddate")
                        .HasColumnType("TEXT");

                    b.Property<string>("otpchk")
                        .HasColumnType("TEXT");

                    b.Property<string>("sQply")
                        .HasColumnType("TEXT");

                    b.HasKey("me_sn");

                    b.ToTable("Member");
                });
#pragma warning restore 612, 618
        }
    }
}
