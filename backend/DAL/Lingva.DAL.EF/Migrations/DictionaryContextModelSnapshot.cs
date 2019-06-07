﻿// <auto-generated />
using System;
using Lingva.DAL.EF.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Lingva.DAL.EF.Migrations
{
    [DbContext(typeof(DictionaryContext))]
    partial class DictionaryContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Lingva.DAL.Entities.Group", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("CreateDate");

                    b.Property<DateTime>("Date");

                    b.Property<string>("Description");

                    b.Property<int>("LanguageId");

                    b.Property<DateTime?>("ModifyDate");

                    b.Property<string>("Name");

                    b.Property<string>("Picture");

                    b.HasKey("Id");

                    b.HasIndex("LanguageId");

                    b.ToTable("Groups");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreateDate = new DateTime(2019, 6, 6, 17, 28, 46, 845, DateTimeKind.Local).AddTicks(8063),
                            Date = new DateTime(2019, 6, 6, 17, 28, 46, 845, DateTimeKind.Local).AddTicks(8073),
                            Description = "Good movie",
                            LanguageId = 1,
                            ModifyDate = new DateTime(2019, 6, 6, 17, 28, 46, 845, DateTimeKind.Local).AddTicks(8070),
                            Name = "Harry Potter",
                            Picture = "1"
                        },
                        new
                        {
                            Id = 2,
                            CreateDate = new DateTime(2019, 6, 6, 17, 28, 46, 845, DateTimeKind.Local).AddTicks(9351),
                            Date = new DateTime(2019, 6, 6, 17, 28, 46, 845, DateTimeKind.Local).AddTicks(9362),
                            Description = "Eq",
                            LanguageId = 1,
                            ModifyDate = new DateTime(2019, 6, 6, 17, 28, 46, 845, DateTimeKind.Local).AddTicks(9359),
                            Name = "Librium",
                            Picture = "2"
                        },
                        new
                        {
                            Id = 3,
                            CreateDate = new DateTime(2019, 6, 6, 17, 28, 46, 845, DateTimeKind.Local).AddTicks(9384),
                            Date = new DateTime(2019, 6, 6, 17, 28, 46, 845, DateTimeKind.Local).AddTicks(9389),
                            Description = "stuff",
                            LanguageId = 2,
                            ModifyDate = new DateTime(2019, 6, 6, 17, 28, 46, 845, DateTimeKind.Local).AddTicks(9386),
                            Name = "2Guns",
                            Picture = "3"
                        });
                });

            modelBuilder.Entity("Lingva.DAL.Entities.GroupUser", b =>
                {
                    b.Property<int>("GroupId");

                    b.Property<int>("UserId");

                    b.Property<DateTime?>("CreateDate");

                    b.Property<int>("Id");

                    b.Property<DateTime?>("ModifyDate");

                    b.HasKey("GroupId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("GroupUser");

                    b.HasData(
                        new
                        {
                            GroupId = 1,
                            UserId = 1,
                            CreateDate = new DateTime(2019, 6, 6, 17, 28, 46, 846, DateTimeKind.Local).AddTicks(940),
                            Id = 1,
                            ModifyDate = new DateTime(2019, 6, 6, 17, 28, 46, 846, DateTimeKind.Local).AddTicks(943)
                        },
                        new
                        {
                            GroupId = 1,
                            UserId = 2,
                            CreateDate = new DateTime(2019, 6, 6, 17, 28, 46, 846, DateTimeKind.Local).AddTicks(1825),
                            Id = 2,
                            ModifyDate = new DateTime(2019, 6, 6, 17, 28, 46, 846, DateTimeKind.Local).AddTicks(1832)
                        });
                });

            modelBuilder.Entity("Lingva.DAL.Entities.Language", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("CreateDate");

                    b.Property<DateTime?>("ModifyDate");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Languages");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreateDate = new DateTime(2019, 6, 6, 17, 28, 46, 841, DateTimeKind.Local).AddTicks(2218),
                            ModifyDate = new DateTime(2019, 6, 6, 17, 28, 46, 845, DateTimeKind.Local).AddTicks(4620),
                            Name = "en"
                        },
                        new
                        {
                            Id = 2,
                            CreateDate = new DateTime(2019, 6, 6, 17, 28, 46, 845, DateTimeKind.Local).AddTicks(6435),
                            ModifyDate = new DateTime(2019, 6, 6, 17, 28, 46, 845, DateTimeKind.Local).AddTicks(6444),
                            Name = "ru"
                        });
                });

            modelBuilder.Entity("Lingva.DAL.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("CreateDate");

                    b.Property<string>("Email");

                    b.Property<DateTime?>("ModifyDate");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreateDate = new DateTime(2019, 6, 6, 17, 28, 46, 845, DateTimeKind.Local).AddTicks(9657),
                            Email = "veloceraptor89@gmail.com",
                            ModifyDate = new DateTime(2019, 6, 6, 17, 28, 46, 845, DateTimeKind.Local).AddTicks(9663),
                            Name = "Serhii"
                        },
                        new
                        {
                            Id = 2,
                            CreateDate = new DateTime(2019, 6, 6, 17, 28, 46, 846, DateTimeKind.Local).AddTicks(779),
                            Email = "tucker_serega@mail.ru",
                            ModifyDate = new DateTime(2019, 6, 6, 17, 28, 46, 846, DateTimeKind.Local).AddTicks(788),
                            Name = "Old"
                        });
                });

            modelBuilder.Entity("SenderService.Email.EF.DAL.Entities.EmailSendingOption", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Host");

                    b.Property<string>("Password");

                    b.Property<int>("Port");

                    b.Property<bool>("UseSsl");

                    b.Property<string>("UserName");

                    b.HasKey("Id");

                    b.ToTable("EmailSendingOption");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Host = "smtp.gmail.com",
                            Password = "worksoftserve_90",
                            Port = 587,
                            UseSsl = false,
                            UserName = "worksoftserve@gmail.com"
                        });
                });

            modelBuilder.Entity("SenderService.Email.EF.DAL.Entities.EmailTemplate", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ParametersString");

                    b.Property<string>("Text");

                    b.HasKey("Id");

                    b.ToTable("EmailTemplate");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            ParametersString = "GroupName; GroupDate",
                            Text = "You will have meeting {{GroupName}} at {{GroupDate}}"
                        });
                });

            modelBuilder.Entity("Lingva.DAL.Entities.Group", b =>
                {
                    b.HasOne("Lingva.DAL.Entities.Language", "Language")
                        .WithMany("Groups")
                        .HasForeignKey("LanguageId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Lingva.DAL.Entities.GroupUser", b =>
                {
                    b.HasOne("Lingva.DAL.Entities.Group", "Group")
                        .WithMany("GroupUsers")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Lingva.DAL.Entities.User", "User")
                        .WithMany("GroupUsers")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
