﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;
using TeachMe.Data;
using TeachMe.Models.CourseModels;

namespace TeachMe.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20180601200825_IsAtiveCOurse")]
    partial class IsAtiveCOurse
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("TeachMe.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("Biography")
                        .HasMaxLength(300);

                    b.Property<string>("City")
                        .IsRequired();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<double>("FinalRating");

                    b.Property<string>("FirstName")
                        .IsRequired();

                    b.Property<string>("ImageName");

                    b.Property<bool>("IsTeacher");

                    b.Property<string>("LastName")
                        .IsRequired();

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<string>("Skype")
                        .IsRequired();

                    b.Property<int>("SummaryStudentsNumber");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("TeachMe.Models.CourseModels.CDayOfWeak", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("CourseID");

                    b.Property<int>("EndTime");

                    b.Property<bool>("IsWorkDay");

                    b.Property<int>("StartTime");

                    b.Property<int>("WeekDay");

                    b.HasKey("ID");

                    b.HasIndex("CourseID");

                    b.ToTable("CDayOfWeak");
                });

            modelBuilder.Entity("TeachMe.Models.CourseModels.Course", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Category");

                    b.Property<string>("Description")
                        .HasMaxLength(600);

                    b.Property<int?>("DurationID");

                    b.Property<double>("FinalRating");

                    b.Property<bool>("IsActive");

                    b.Property<byte>("LessonsNumber");

                    b.Property<decimal>("Price");

                    b.Property<DateTime>("ReleaseDate");

                    b.Property<string>("Subject")
                        .HasMaxLength(60);

                    b.Property<int>("SummaryStudentsNumber");

                    b.Property<string>("TeacherID");

                    b.Property<int?>("TeacherInfoID");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(60);

                    b.HasKey("ID");

                    b.HasIndex("DurationID");

                    b.HasIndex("TeacherInfoID");

                    b.ToTable("Course");
                });

            modelBuilder.Entity("TeachMe.Models.CourseModels.CourseLesson", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("BusyExpireDate");

                    b.Property<int?>("CourseID");

                    b.Property<DateTime>("EndLessonTime");

                    b.Property<DateTime>("StartLessonTime");

                    b.Property<int>("WeekDay");

                    b.Property<bool>("isBusy");

                    b.HasKey("ID");

                    b.HasIndex("CourseID");

                    b.ToTable("CourseLesson");
                });

            modelBuilder.Entity("TeachMe.Models.CourseModels.CourseMark", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("CourseID");

                    b.Property<double>("Mark");

                    b.Property<string>("RaterId");

                    b.HasKey("ID");

                    b.HasIndex("CourseID");

                    b.ToTable("CourseMark");
                });

            modelBuilder.Entity("TeachMe.Models.CourseModels.CourseTeacherInfo", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Email");

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.Property<string>("Skype");

                    b.HasKey("ID");

                    b.ToTable("CourseTeacherInfo");
                });

            modelBuilder.Entity("TeachMe.Models.CourseModels.LessonTime", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Hours");

                    b.Property<int>("Minutes");

                    b.HasKey("ID");

                    b.ToTable("LessonTime");
                });

            modelBuilder.Entity("TeachMe.Models.CourseModels.UserCourse", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ApplicationUserId");

                    b.Property<string>("CourseTittle");

                    b.Property<DateTime>("EndLessonTime");

                    b.Property<DateTime>("ExpireDate");

                    b.Property<DateTime>("StartLessonTime");

                    b.Property<string>("StudentId");

                    b.Property<string>("TeacherId");

                    b.Property<string>("UserShortInfoId");

                    b.Property<int>("WeekDay");

                    b.HasKey("ID");

                    b.HasIndex("ApplicationUserId");

                    b.HasIndex("UserShortInfoId");

                    b.ToTable("UserCourse");
                });

            modelBuilder.Entity("TeachMe.Models.TeacherRating", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ApplicationUserId");

                    b.Property<double>("Mark");

                    b.Property<string>("RaterId");

                    b.HasKey("ID");

                    b.HasIndex("ApplicationUserId");

                    b.ToTable("TeacherRating");
                });

            modelBuilder.Entity("TeachMe.Models.UserShortInfo", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Email");

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.HasKey("Id");

                    b.ToTable("UserShortInfo");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("TeachMe.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("TeachMe.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("TeachMe.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("TeachMe.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TeachMe.Models.CourseModels.CDayOfWeak", b =>
                {
                    b.HasOne("TeachMe.Models.CourseModels.Course")
                        .WithMany("WeekPlans")
                        .HasForeignKey("CourseID");
                });

            modelBuilder.Entity("TeachMe.Models.CourseModels.Course", b =>
                {
                    b.HasOne("TeachMe.Models.CourseModels.LessonTime", "Duration")
                        .WithMany()
                        .HasForeignKey("DurationID");

                    b.HasOne("TeachMe.Models.CourseModels.CourseTeacherInfo", "TeacherInfo")
                        .WithMany()
                        .HasForeignKey("TeacherInfoID");
                });

            modelBuilder.Entity("TeachMe.Models.CourseModels.CourseLesson", b =>
                {
                    b.HasOne("TeachMe.Models.CourseModels.Course")
                        .WithMany("LessonSchedule")
                        .HasForeignKey("CourseID");
                });

            modelBuilder.Entity("TeachMe.Models.CourseModels.CourseMark", b =>
                {
                    b.HasOne("TeachMe.Models.CourseModels.Course")
                        .WithMany("Marks")
                        .HasForeignKey("CourseID");
                });

            modelBuilder.Entity("TeachMe.Models.CourseModels.UserCourse", b =>
                {
                    b.HasOne("TeachMe.Models.ApplicationUser")
                        .WithMany("LessonsList")
                        .HasForeignKey("ApplicationUserId");

                    b.HasOne("TeachMe.Models.UserShortInfo", "UserShortInfo")
                        .WithMany()
                        .HasForeignKey("UserShortInfoId");
                });

            modelBuilder.Entity("TeachMe.Models.TeacherRating", b =>
                {
                    b.HasOne("TeachMe.Models.ApplicationUser")
                        .WithMany("Marks")
                        .HasForeignKey("ApplicationUserId");
                });
#pragma warning restore 612, 618
        }
    }
}
