﻿// <auto-generated />
using System;
using Eros.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Eros.Persistence.Migrations
{
    [DbContext(typeof(ErosDbContext))]
    [Migration("20240416064600_AddRoleIdToInvitation")]
    partial class AddRoleIdToInvitation
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Eros.Domain.Aggregates.Apartments.Apartment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("ApartmentTypeId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("BuildingId")
                        .HasColumnType("uuid");

                    b.Property<string>("Description")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<int>("NumberOfRooms")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ApartmentTypeId");

                    b.HasIndex("BuildingId");

                    b.HasIndex("Name", "BuildingId")
                        .IsUnique();

                    b.ToTable("Apartments", (string)null);
                });

            modelBuilder.Entity("Eros.Domain.Aggregates.Apartments.ApartmentType", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Description")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("ApartmentTypes", (string)null);
                });

            modelBuilder.Entity("Eros.Domain.Aggregates.Buildings.Building", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<Guid>("BuildingTypeId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<Guid>("EstateId")
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("BuildingTypeId");

                    b.HasIndex("EstateId");

                    b.ToTable("Buildings", (string)null);
                });

            modelBuilder.Entity("Eros.Domain.Aggregates.Buildings.BuildingType", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("BuildingTypes", (string)null);
                });

            modelBuilder.Entity("Eros.Domain.Aggregates.Estates.Estate", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("CreatedBy")
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("LatLng")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<Guid>("OwnerId")
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Estates", (string)null);
                });

            modelBuilder.Entity("Eros.Domain.Aggregates.Estates.EstateRole", b =>
                {
                    b.Property<Guid>("EstateId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uuid");

                    b.HasKey("EstateId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("EstateRole");
                });

            modelBuilder.Entity("Eros.Domain.Aggregates.Estates.EstateUser", b =>
                {
                    b.Property<Guid>("EstateId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("ApartmentId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("CreatedBy")
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("EstateUserType")
                        .HasColumnType("integer");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("EstateId", "UserId");

                    b.HasIndex("RoleId");

                    b.HasIndex("UserId");

                    b.ToTable("EstateUser");
                });

            modelBuilder.Entity("Eros.Domain.Aggregates.Invitations.Invitation", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("CreatedBy")
                        .HasColumnType("uuid");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)");

                    b.Property<Guid>("EstateId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("Expiration")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uuid");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.ToTable("Invitations", (string)null);
                });

            modelBuilder.Entity("Eros.Domain.Aggregates.Roles.Permission", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("Id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("Name");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Permissions", (string)null);

                    b.HasData(
                        new
                        {
                            Id = new Guid("8cfdbbb4-cb87-4d78-9806-c20a63b87530"),
                            Name = "Estate.Create"
                        },
                        new
                        {
                            Id = new Guid("d542cd68-6d1a-4588-92c1-b35f550a2a1b"),
                            Name = "Estate.Update"
                        },
                        new
                        {
                            Id = new Guid("32e40dda-f579-4fd8-aeee-d157cedeb062"),
                            Name = "Estate.Delete"
                        },
                        new
                        {
                            Id = new Guid("e45f233e-6dbd-4228-8b18-a23a66c1b18b"),
                            Name = "Estate.View"
                        },
                        new
                        {
                            Id = new Guid("b80d1e12-44bb-4457-a1ce-e6fbbbd74cec"),
                            Name = "Building.Create"
                        },
                        new
                        {
                            Id = new Guid("2a84ccda-2adf-4215-8e2e-5bbd72d5216c"),
                            Name = "Building.Update"
                        },
                        new
                        {
                            Id = new Guid("bd848626-40c8-499a-b9f8-2886cf57d8c6"),
                            Name = "Building.Delete"
                        },
                        new
                        {
                            Id = new Guid("af23db24-f6c0-4448-9f43-67288c4f5328"),
                            Name = "Building.View"
                        },
                        new
                        {
                            Id = new Guid("e300d634-8686-40b2-b396-6e0ac5cb0d09"),
                            Name = "Building.List"
                        },
                        new
                        {
                            Id = new Guid("9765a47d-7e67-4d24-aa86-af372748ec7a"),
                            Name = "Apartment.Create"
                        },
                        new
                        {
                            Id = new Guid("61e201f7-7859-40b9-bbe8-715f29204291"),
                            Name = "Apartment.Update"
                        },
                        new
                        {
                            Id = new Guid("1c8a89a0-b6fe-4efc-b0e3-12db170433fe"),
                            Name = "Apartment.Delete"
                        },
                        new
                        {
                            Id = new Guid("8b7a4964-46b3-4a03-a19e-86c35cb5b3cd"),
                            Name = "Apartment.View"
                        },
                        new
                        {
                            Id = new Guid("2c67bdb4-7d9e-419a-816d-be29da2e837a"),
                            Name = "Apartment.List"
                        },
                        new
                        {
                            Id = new Guid("98d6ab3d-8a40-44bc-85d6-96c739813f9d"),
                            Name = "Role.Create"
                        },
                        new
                        {
                            Id = new Guid("1a4b7beb-488b-4666-86d4-991895e0872b"),
                            Name = "Role.Update"
                        },
                        new
                        {
                            Id = new Guid("4c6e1ac5-b00e-41f5-89fa-c801578a9818"),
                            Name = "Role.Delete"
                        },
                        new
                        {
                            Id = new Guid("ed64a073-1821-407c-add6-2e88f5d045e1"),
                            Name = "Role.View"
                        },
                        new
                        {
                            Id = new Guid("e0a2cb5c-28e2-4ee7-8fb9-a00a3e761e5e"),
                            Name = "Role.List"
                        });
                });

            modelBuilder.Entity("Eros.Domain.Aggregates.Roles.Role", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("Id");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("CreatedAt");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("Description");

                    b.Property<bool>("IsShared")
                        .HasColumnType("boolean")
                        .HasColumnName("IsShared");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("Name");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("UpdatedAt");

                    b.HasKey("Id");

                    b.ToTable("Roles", (string)null);
                });

            modelBuilder.Entity("Eros.Domain.Aggregates.Roles.RolePermission", b =>
                {
                    b.Property<Guid>("PermissionId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uuid");

                    b.HasKey("PermissionId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("RolePermission");
                });

            modelBuilder.Entity("Eros.Domain.Aggregates.Users.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("integer");

                    b.Property<string>("Avatar")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("boolean");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<bool>("IsAdmin")
                        .HasColumnType("boolean");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("boolean");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("boolean");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("text");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("boolean");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("Users", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("text");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("text");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("text");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Value")
                        .HasColumnType("text");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("Eros.Domain.Aggregates.Apartments.Apartment", b =>
                {
                    b.HasOne("Eros.Domain.Aggregates.Apartments.ApartmentType", "ApartmentType")
                        .WithMany()
                        .HasForeignKey("ApartmentTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Eros.Domain.Aggregates.Buildings.Building", "Building")
                        .WithMany("Apartments")
                        .HasForeignKey("BuildingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ApartmentType");

                    b.Navigation("Building");
                });

            modelBuilder.Entity("Eros.Domain.Aggregates.Buildings.Building", b =>
                {
                    b.HasOne("Eros.Domain.Aggregates.Buildings.BuildingType", "BuildingType")
                        .WithMany()
                        .HasForeignKey("BuildingTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Eros.Domain.Aggregates.Estates.Estate", "Estate")
                        .WithMany("Buildings")
                        .HasForeignKey("EstateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BuildingType");

                    b.Navigation("Estate");
                });

            modelBuilder.Entity("Eros.Domain.Aggregates.Estates.EstateRole", b =>
                {
                    b.HasOne("Eros.Domain.Aggregates.Estates.Estate", null)
                        .WithMany("EstateRoles")
                        .HasForeignKey("EstateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Eros.Domain.Aggregates.Roles.Role", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Eros.Domain.Aggregates.Estates.EstateUser", b =>
                {
                    b.HasOne("Eros.Domain.Aggregates.Estates.Estate", "Estate")
                        .WithMany()
                        .HasForeignKey("EstateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Eros.Domain.Aggregates.Roles.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Eros.Domain.Aggregates.Users.User", "User")
                        .WithMany("EstateUsers")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Estate");

                    b.Navigation("Role");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Eros.Domain.Aggregates.Roles.RolePermission", b =>
                {
                    b.HasOne("Eros.Domain.Aggregates.Roles.Permission", null)
                        .WithMany()
                        .HasForeignKey("PermissionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Eros.Domain.Aggregates.Roles.Role", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.HasOne("Eros.Domain.Aggregates.Users.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.HasOne("Eros.Domain.Aggregates.Users.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.HasOne("Eros.Domain.Aggregates.Users.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Eros.Domain.Aggregates.Buildings.Building", b =>
                {
                    b.Navigation("Apartments");
                });

            modelBuilder.Entity("Eros.Domain.Aggregates.Estates.Estate", b =>
                {
                    b.Navigation("Buildings");

                    b.Navigation("EstateRoles");
                });

            modelBuilder.Entity("Eros.Domain.Aggregates.Users.User", b =>
                {
                    b.Navigation("EstateUsers");
                });
#pragma warning restore 612, 618
        }
    }
}
